using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeDiagnosticGrowthRateNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GrowthRate",
                schema: "diagnostics_statistics",
                table: "ErrorStatistics",
                type: "numeric(9,2)",
                precision: 9,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(9,2)",
                oldPrecision: 9,
                oldScale: 2);

            migrationBuilder.Sql(
                """
                CREATE OR REPLACE FUNCTION diagnostics_statistics."RecalculateErrorStatistics"(
                    p_error_group_id uuid,
                    p_calculated_at timestamp with time zone
                )
                RETURNS void
                LANGUAGE plpgsql
                AS $function$
                DECLARE
                    v_application_id text;
                    v_latest_occurrence_at timestamp with time zone;

                    -- Переменные для основной статистики
                    v_total_occurrences bigint;
                    v_unique_users bigint;
                    v_affected_user_ratio numeric(5,2);
                    v_occurrences_last_hour bigint;
                    v_occurrences_last_day bigint;
                    v_occurrences_last_week bigint;
                    v_users_last_hour bigint;
                    v_users_last_day bigint;
                    v_users_last_week bigint;
                    v_previous_hour_occurrences bigint;
                    v_growth_rate numeric(9,2);
                    v_active_employees bigint;
                BEGIN
                    -- 1. Проверка существования группы ошибок
                    SELECT "ApplicationId" INTO v_application_id
                    FROM diagnostics_core."ErrorGroups"
                    WHERE "Id" = p_error_group_id;

                    -- 2. Единый запрос для сбора всей базовой статистики
                    SELECT
                        COUNT(*),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL),
                        COUNT(*) FILTER (WHERE "OccurredAt" >= p_calculated_at - INTERVAL '1 hour' AND "OccurredAt" < p_calculated_at),
                        COUNT(*) FILTER (WHERE "OccurredAt" >= p_calculated_at - INTERVAL '1 day' AND "OccurredAt" < p_calculated_at),
                        COUNT(*) FILTER (WHERE "OccurredAt" >= p_calculated_at - INTERVAL '7 days' AND "OccurredAt" < p_calculated_at),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL AND "OccurredAt" >= p_calculated_at - INTERVAL '1 hour' AND "OccurredAt" < p_calculated_at),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL AND "OccurredAt" >= p_calculated_at - INTERVAL '1 day' AND "OccurredAt" < p_calculated_at),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL AND "OccurredAt" >= p_calculated_at - INTERVAL '7 days' AND "OccurredAt" < p_calculated_at),
                        COUNT(*) FILTER (WHERE "OccurredAt" >= p_calculated_at - INTERVAL '2 hours' AND "OccurredAt" < p_calculated_at - INTERVAL '1 hour'),
                        MAX("OccurredAt")
                    INTO
                        v_total_occurrences,
                        v_unique_users,
                        v_occurrences_last_hour,
                        v_occurrences_last_day,
                        v_occurrences_last_week,
                        v_users_last_hour,
                        v_users_last_day,
                        v_users_last_week,
                        v_previous_hour_occurrences,
                        v_latest_occurrence_at
                    FROM diagnostics_core."ErrorOccurrences"
                    WHERE "ErrorGroupId" = p_error_group_id;

                    -- 3. Расчет соотношения затронутых пользователей
                    SELECT COUNT(*) INTO v_active_employees
                    FROM app."Employees"
                    WHERE "IsDeleted" = FALSE;

                    v_affected_user_ratio := CASE
                        WHEN v_active_employees = 0 THEN 0
                        ELSE LEAST(100, ROUND(v_users_last_week * 100.0 / v_active_employees, 2))
                    END;

                    -- 4. Расчет темпа роста (Growth Rate)
                    v_growth_rate := CASE
                        WHEN v_previous_hour_occurrences = 0 AND v_occurrences_last_hour = 0 THEN 0
                        WHEN v_previous_hour_occurrences = 0 THEN NULL
                        ELSE ROUND(((v_occurrences_last_hour - v_previous_hour_occurrences) * 100.0) / v_previous_hour_occurrences, 2)
                    END;

                    -- 5. Обновление основной статистики (UPSERT)
                    INSERT INTO diagnostics_statistics."ErrorStatistics" (
                        "Id", "ErrorGroupId", "TotalOccurrences", "UniqueUsers", "AffectedUserRatio",
                        "OccurrencesLastHour", "OccurrencesLastDay", "OccurrencesLastWeek",
                        "UsersLastHour", "UsersLastDay", "UsersLastWeek", "GrowthRate", "CalculatedAt"
                    ) VALUES (
                        gen_random_uuid(), p_error_group_id, v_total_occurrences, v_unique_users, v_affected_user_ratio,
                        v_occurrences_last_hour, v_occurrences_last_day, v_occurrences_last_week,
                        v_users_last_hour, v_users_last_day, v_users_last_week, v_growth_rate, p_calculated_at
                    )
                    ON CONFLICT ("ErrorGroupId") DO UPDATE SET
                        "TotalOccurrences" = EXCLUDED."TotalOccurrences",
                        "UniqueUsers" = EXCLUDED."UniqueUsers",
                        "AffectedUserRatio" = EXCLUDED."AffectedUserRatio",
                        "OccurrencesLastHour" = EXCLUDED."OccurrencesLastHour",
                        "OccurrencesLastDay" = EXCLUDED."OccurrencesLastDay",
                        "OccurrencesLastWeek" = EXCLUDED."OccurrencesLastWeek",
                        "UsersLastHour" = EXCLUDED."UsersLastHour",
                        "UsersLastDay" = EXCLUDED."UsersLastDay",
                        "UsersLastWeek" = EXCLUDED."UsersLastWeek",
                        "GrowthRate" = EXCLUDED."GrowthRate",
                        "CalculatedAt" = EXCLUDED."CalculatedAt";

                    -- 6. Ранний выход, если не было инцидентов
                    IF v_latest_occurrence_at IS NULL THEN
                        RETURN;
                    END IF;

                    -- 7. Set-based upsert текущих временных бакетов.
                    --
                    -- Границы бакетов вычисляются через Unix epoch, поэтому они
                    -- не зависят от timezone PostgreSQL session.
                    WITH BucketParams AS (
                        SELECT
                            d."Id" AS bucket_type_id,
                            CASE d."Code"
                                WHEN 'MINUTE' THEN 1
                                WHEN 'HOUR' THEN 60
                                WHEN 'DAY' THEN 1440
                            END AS duration_mins
                        FROM diagnostics_lookup."DiagnosticDefinitions" d
                        INNER JOIN diagnostics_lookup."DiagnosticDefinitionGroups" g
                            ON g."Id" = d."GroupId"
                        WHERE g."Code" = 'METRIC_BUCKET_TYPE'
                          AND d."Code" IN ('MINUTE', 'HOUR', 'DAY')
                          AND g."IsActive" = TRUE
                          AND d."IsActive" = TRUE
                    ),
                    BucketBounds AS (
                        SELECT
                            bp.bucket_type_id,
                            bp.duration_mins,
                            to_timestamp(
                                floor(
                                    extract(epoch FROM v_latest_occurrence_at)
                                    / (bp.duration_mins * 60)
                                ) * bp.duration_mins * 60
                            ) AS bucket_start
                        FROM BucketParams bp
                    )
                    INSERT INTO diagnostics_statistics."ErrorMetricBuckets" (
                        "Id",
                        "ErrorGroupId",
                        "BucketStart",
                        "BucketDurationMinutes",
                        "BucketTypeId",
                        "Occurrences",
                        "UniqueUsers",
                        "FirstOccurrenceAt",
                        "LastOccurrenceAt",
                        "CalculatedAt"
                    )
                    SELECT
                        gen_random_uuid(),
                        p_error_group_id,
                        bb.bucket_start,
                        bb.duration_mins,
                        bb.bucket_type_id,
                        COUNT(eo."OccurredAt"),
                        COUNT(DISTINCT eo."UserId") FILTER (WHERE eo."UserId" IS NOT NULL),
                        MIN(eo."OccurredAt"),
                        MAX(eo."OccurredAt"),
                        p_calculated_at
                    FROM BucketBounds bb
                    LEFT JOIN diagnostics_core."ErrorOccurrences" eo
                        ON eo."ErrorGroupId" = p_error_group_id
                        AND eo."OccurredAt" >= bb.bucket_start
                        AND eo."OccurredAt" < bb.bucket_start + make_interval(mins => bb.duration_mins)
                    GROUP BY
                        bb.bucket_type_id,
                        bb.duration_mins,
                        bb.bucket_start
                    ON CONFLICT ("ErrorGroupId", "BucketTypeId", "BucketStart", "BucketDurationMinutes")
                    DO UPDATE SET
                        "Occurrences" = EXCLUDED."Occurrences",
                        "UniqueUsers" = EXCLUDED."UniqueUsers",
                        "FirstOccurrenceAt" = EXCLUDED."FirstOccurrenceAt",
                        "LastOccurrenceAt" = EXCLUDED."LastOccurrenceAt",
                        "CalculatedAt" = EXCLUDED."CalculatedAt";
                END;
                $function$;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GrowthRate",
                schema: "diagnostics_statistics",
                table: "ErrorStatistics",
                type: "numeric(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(9,2)",
                oldPrecision: 9,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.Sql(
                """
                DROP FUNCTION IF EXISTS "diagnostics_statistics"."RecalculateErrorStatistics"(uuid, timestamp with time zone);
                """);
        }
    }
}
