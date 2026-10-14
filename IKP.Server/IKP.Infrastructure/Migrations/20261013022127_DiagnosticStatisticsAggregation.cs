using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiagnosticStatisticsAggregation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE OR REPLACE FUNCTION "diagnostics_statistics"."RecalculateErrorStatistics"(
                    p_error_group_id uuid,
                    p_calculated_at timestamp with time zone
                )
                RETURNS void
                LANGUAGE plpgsql
                AS $function$
                DECLARE
                    v_statistics_id uuid;
                    v_application_id text;
                    v_total_occurrences bigint;
                    v_unique_users bigint;
                    v_group_users_last_week bigint;
                    v_application_users_last_week bigint;
                    v_affected_user_ratio numeric(5,2);
                    v_occurrences_last_hour bigint;
                    v_occurrences_last_day bigint;
                    v_occurrences_last_week bigint;
                    v_users_last_hour bigint;
                    v_users_last_day bigint;
                    v_users_last_week bigint;
                    v_previous_hour_occurrences bigint;
                    v_growth_rate numeric(9,2);
                    v_bucket_type_minute uuid;
                    v_bucket_type_hour uuid;
                    v_bucket_type_day uuid;
                BEGIN
                    SELECT "ApplicationId"
                    INTO v_application_id
                    FROM "diagnostics_core"."ErrorGroups"
                    WHERE "Id" = p_error_group_id;

                    IF v_application_id IS NULL THEN
                        RAISE EXCEPTION 'Error group % was not found or has no application id.', p_error_group_id;
                    END IF;

                    SELECT
                        COUNT(*),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL)
                    INTO v_total_occurrences, v_unique_users
                    FROM "diagnostics_core"."ErrorOccurrences"
                    WHERE "ErrorGroupId" = p_error_group_id;

                    SELECT
                        COUNT(DISTINCT occurrence."UserId") FILTER (WHERE occurrence."ErrorGroupId" = p_error_group_id),
                        COUNT(DISTINCT occurrence."UserId")
                    INTO v_group_users_last_week, v_application_users_last_week
                    FROM "diagnostics_core"."ErrorOccurrences" occurrence
                    INNER JOIN "diagnostics_core"."ErrorGroups" occurrence_group
                        ON occurrence_group."Id" = occurrence."ErrorGroupId"
                    WHERE occurrence_group."ApplicationId" = v_application_id
                      AND occurrence."OccurredAt" >= p_calculated_at - INTERVAL '7 days'
                      AND occurrence."OccurredAt" <= p_calculated_at
                      AND occurrence."UserId" IS NOT NULL;

                    IF v_application_users_last_week = 0 THEN
                        v_affected_user_ratio := 0;
                    ELSE
                        v_affected_user_ratio := ROUND((v_group_users_last_week::numeric / v_application_users_last_week::numeric) * 100, 2);
                    END IF;

                    SELECT
                        COUNT(*) FILTER (WHERE "OccurredAt" >= p_calculated_at - INTERVAL '1 hour' AND "OccurredAt" <= p_calculated_at),
                        COUNT(*) FILTER (WHERE "OccurredAt" >= p_calculated_at - INTERVAL '1 day' AND "OccurredAt" <= p_calculated_at),
                        COUNT(*) FILTER (WHERE "OccurredAt" >= p_calculated_at - INTERVAL '7 days' AND "OccurredAt" <= p_calculated_at),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL AND "OccurredAt" >= p_calculated_at - INTERVAL '1 hour' AND "OccurredAt" <= p_calculated_at),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL AND "OccurredAt" >= p_calculated_at - INTERVAL '1 day' AND "OccurredAt" <= p_calculated_at),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL AND "OccurredAt" >= p_calculated_at - INTERVAL '7 days' AND "OccurredAt" <= p_calculated_at)
                    INTO
                        v_occurrences_last_hour,
                        v_occurrences_last_day,
                        v_occurrences_last_week,
                        v_users_last_hour,
                        v_users_last_day,
                        v_users_last_week
                    FROM "diagnostics_core"."ErrorOccurrences"
                    WHERE "ErrorGroupId" = p_error_group_id;

                    SELECT COUNT(*)
                    INTO v_previous_hour_occurrences
                    FROM "diagnostics_core"."ErrorOccurrences"
                    WHERE "ErrorGroupId" = p_error_group_id
                      AND "OccurredAt" >= p_calculated_at - INTERVAL '2 hours'
                      AND "OccurredAt" < p_calculated_at - INTERVAL '1 hour';

                    IF v_previous_hour_occurrences = 0 THEN
                        IF v_occurrences_last_hour = 0 THEN
                            v_growth_rate := 0;
                        ELSE
                            v_growth_rate := 100;
                        END IF;
                    ELSE
                        v_growth_rate := ROUND(((v_occurrences_last_hour - v_previous_hour_occurrences)::numeric / v_previous_hour_occurrences::numeric) * 100, 2);
                    END IF;

                    SELECT "Id"
                    INTO v_statistics_id
                    FROM "diagnostics_statistics"."ErrorStatistics"
                    WHERE "ErrorGroupId" = p_error_group_id;

                    IF v_statistics_id IS NULL THEN
                        v_statistics_id := gen_random_uuid();
                    END IF;

                    INSERT INTO "diagnostics_statistics"."ErrorStatistics" (
                        "Id",
                        "ErrorGroupId",
                        "TotalOccurrences",
                        "UniqueUsers",
                        "AffectedUserRatio",
                        "OccurrencesLastHour",
                        "OccurrencesLastDay",
                        "OccurrencesLastWeek",
                        "UsersLastHour",
                        "UsersLastDay",
                        "UsersLastWeek",
                        "GrowthRate",
                        "CalculatedAt"
                    )
                    VALUES (
                        v_statistics_id,
                        p_error_group_id,
                        v_total_occurrences,
                        v_unique_users,
                        v_affected_user_ratio,
                        v_occurrences_last_hour,
                        v_occurrences_last_day,
                        v_occurrences_last_week,
                        v_users_last_hour,
                        v_users_last_day,
                        v_users_last_week,
                        v_growth_rate,
                        p_calculated_at
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

                    SELECT "Id" INTO v_bucket_type_minute
                    FROM "diagnostics_lookup"."DiagnosticDefinitions"
                    WHERE "Code" = 'MINUTE' AND "IsActive"
                    LIMIT 1;

                    SELECT "Id" INTO v_bucket_type_hour
                    FROM "diagnostics_lookup"."DiagnosticDefinitions"
                    WHERE "Code" = 'HOUR' AND "IsActive"
                    LIMIT 1;

                    SELECT "Id" INTO v_bucket_type_day
                    FROM "diagnostics_lookup"."DiagnosticDefinitions"
                    WHERE "Code" = 'DAY' AND "IsActive"
                    LIMIT 1;

                    IF v_bucket_type_minute IS NULL OR v_bucket_type_hour IS NULL OR v_bucket_type_day IS NULL THEN
                        RAISE EXCEPTION 'Metric bucket type definitions MINUTE, HOUR and DAY are required.';
                    END IF;

                    INSERT INTO "diagnostics_statistics"."ErrorMetricBuckets" (
                        "Id", "ErrorGroupId", "BucketStart", "BucketDurationMinutes", "BucketTypeId",
                        "Occurrences", "UniqueUsers", "FirstOccurrenceAt", "LastOccurrenceAt", "CalculatedAt"
                    )
                    SELECT
                        gen_random_uuid(),
                        p_error_group_id,
                        date_trunc('minute', p_calculated_at),
                        1,
                        v_bucket_type_minute,
                        COUNT(*),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL),
                        MIN("OccurredAt"),
                        MAX("OccurredAt"),
                        p_calculated_at
                    FROM "diagnostics_core"."ErrorOccurrences"
                    WHERE "ErrorGroupId" = p_error_group_id
                      AND "OccurredAt" >= date_trunc('minute', p_calculated_at)
                      AND "OccurredAt" < date_trunc('minute', p_calculated_at) + INTERVAL '1 minute'
                    ON CONFLICT ("ErrorGroupId", "BucketTypeId", "BucketStart", "BucketDurationMinutes") DO UPDATE SET
                        "Occurrences" = EXCLUDED."Occurrences",
                        "UniqueUsers" = EXCLUDED."UniqueUsers",
                        "FirstOccurrenceAt" = EXCLUDED."FirstOccurrenceAt",
                        "LastOccurrenceAt" = EXCLUDED."LastOccurrenceAt",
                        "CalculatedAt" = EXCLUDED."CalculatedAt";

                    INSERT INTO "diagnostics_statistics"."ErrorMetricBuckets" (
                        "Id", "ErrorGroupId", "BucketStart", "BucketDurationMinutes", "BucketTypeId",
                        "Occurrences", "UniqueUsers", "FirstOccurrenceAt", "LastOccurrenceAt", "CalculatedAt"
                    )
                    SELECT
                        gen_random_uuid(),
                        p_error_group_id,
                        date_trunc('hour', p_calculated_at),
                        60,
                        v_bucket_type_hour,
                        COUNT(*),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL),
                        MIN("OccurredAt"),
                        MAX("OccurredAt"),
                        p_calculated_at
                    FROM "diagnostics_core"."ErrorOccurrences"
                    WHERE "ErrorGroupId" = p_error_group_id
                      AND "OccurredAt" >= date_trunc('hour', p_calculated_at)
                      AND "OccurredAt" < date_trunc('hour', p_calculated_at) + INTERVAL '1 hour'
                    ON CONFLICT ("ErrorGroupId", "BucketTypeId", "BucketStart", "BucketDurationMinutes") DO UPDATE SET
                        "Occurrences" = EXCLUDED."Occurrences",
                        "UniqueUsers" = EXCLUDED."UniqueUsers",
                        "FirstOccurrenceAt" = EXCLUDED."FirstOccurrenceAt",
                        "LastOccurrenceAt" = EXCLUDED."LastOccurrenceAt",
                        "CalculatedAt" = EXCLUDED."CalculatedAt";

                    INSERT INTO "diagnostics_statistics"."ErrorMetricBuckets" (
                        "Id", "ErrorGroupId", "BucketStart", "BucketDurationMinutes", "BucketTypeId",
                        "Occurrences", "UniqueUsers", "FirstOccurrenceAt", "LastOccurrenceAt", "CalculatedAt"
                    )
                    SELECT
                        gen_random_uuid(),
                        p_error_group_id,
                        date_trunc('day', p_calculated_at),
                        1440,
                        v_bucket_type_day,
                        COUNT(*),
                        COUNT(DISTINCT "UserId") FILTER (WHERE "UserId" IS NOT NULL),
                        MIN("OccurredAt"),
                        MAX("OccurredAt"),
                        p_calculated_at
                    FROM "diagnostics_core"."ErrorOccurrences"
                    WHERE "ErrorGroupId" = p_error_group_id
                      AND "OccurredAt" >= date_trunc('day', p_calculated_at)
                      AND "OccurredAt" < date_trunc('day', p_calculated_at) + INTERVAL '1 day'
                    ON CONFLICT ("ErrorGroupId", "BucketTypeId", "BucketStart", "BucketDurationMinutes") DO UPDATE SET
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
            migrationBuilder.Sql(
                """
                DROP FUNCTION IF EXISTS "diagnostics_statistics"."RecalculateErrorStatistics"(uuid, timestamp with time zone);
                """);
        }
    }
}
