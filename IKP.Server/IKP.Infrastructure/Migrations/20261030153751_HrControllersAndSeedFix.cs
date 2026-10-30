using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IKP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HrControllersAndSeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompetencyMatrixItems_IsDeleted_SectionId",
                schema: "app",
                table: "CompetencyMatrixItems");

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                keyColumn: "Id",
                keyValue: new Guid("400a6b76-3848-4ed8-9f87-66781eb5ad08"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                keyColumn: "Id",
                keyValue: new Guid("5e8ae364-20b4-4d87-a1dd-129933dc3f98"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                keyColumn: "Id",
                keyValue: new Guid("7d13cc7d-a26d-46f5-bc97-99cd1ef970a3"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("135d60c7-8db3-44c5-ba38-40941c38e79f"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("3107999a-8171-4603-9920-1049d28406d4"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("48e20b26-4a1e-400c-bc4e-765ddfc67f5d"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("664eecb2-ad07-4758-9374-840edead6e6c"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("97694f85-3780-4fac-8f0f-f7d146482794"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                keyColumn: "Id",
                keyValue: new Guid("9f8e4471-55a6-4c35-9784-339775fb76ed"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                keyColumn: "Id",
                keyValue: new Guid("ec327914-2553-417e-9293-7f1a257538e9"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "CompetencySectionType",
                keyColumn: "Id",
                keyValue: new Guid("483a5019-76e0-444d-8b3d-07414af3aeab"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "CompetencySectionType",
                keyColumn: "Id",
                keyValue: new Guid("b294a9f3-5ade-4065-a651-7204b9c1d09d"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                keyColumn: "Id",
                keyValue: new Guid("9a4a148b-020d-46ae-ab9a-ae25c0f74556"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                keyColumn: "Id",
                keyValue: new Guid("a8006951-8d75-4510-bacf-967d48779aa3"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                keyColumn: "Id",
                keyValue: new Guid("d315ce3c-2cd7-44d7-8bbe-7290307b85d2"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                keyColumn: "Id",
                keyValue: new Guid("daa84e6a-5c60-4d07-b92f-2dc7db57ac22"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("016e6c89-ad0e-4969-a004-38713a329c61"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("033d6925-6d4e-41d3-bdca-92d8c96b952f"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("08434097-599e-4676-8b88-5d083e79f5b1"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("3a9f9698-88b6-4834-a055-ec3935f607fe"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("65400c7b-662d-46de-bd2f-e0f035f50887"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("81e0c037-fe5b-4b05-aaf6-f05ef72769af"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("947f3616-84c8-4e9e-acb7-bcaae61bf220"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("b1200c92-895a-4fb3-aa6f-4e2b9047d17b"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("b9ff6c64-6c01-4f25-beba-806d8c1e252d"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("fecafe0d-cc2d-400d-a388-c9dadcd21809"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("03ea6d8c-fb9c-45dc-b589-126e076ab4e8"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("16f677ab-0314-40d1-ab6f-90099483d770"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("1f07ecab-585f-4f33-9b0d-c6560e16a058"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("262c7d6b-05cf-4c7f-a264-18fbf57d7fb6"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("278c967d-3d23-4c09-8377-6c10d34a7215"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("2efde89f-66fb-4e8d-beb2-944a1a542fa4"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("30568ab9-31ec-414a-8e30-980d764fd988"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("4ad17a34-26dd-4216-8b3c-c2a01f1c5ae5"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("591d1482-7c34-4377-9572-e4773ba70425"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("5e1593bf-e290-49c6-91c3-21216ca154b2"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("72111a66-6892-43a1-9273-c2bd394d747a"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("730629af-4007-4423-b604-a1f048806896"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("7e4a82da-13ae-4a12-93a6-de7f39588286"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("8f85a850-09ab-49e0-aa75-0cd060004e8f"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("8fc2deb1-3258-402f-a744-645a34fa67b0"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("9b5a97f6-0e59-4ecb-9545-043d382c0fb8"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("a53f0d6c-221e-4618-beab-fe17d1e0809c"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("a6afe381-7e69-40d2-afb3-783eef10cbfc"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("ae8188b3-5c67-4d7b-9854-fbde5a6aa84d"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("bc103f5f-edfb-4c3e-a826-7069733e1dac"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("dc0df98f-c941-4587-9626-1f228720fca9"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                keyColumn: "Id",
                keyValue: new Guid("e18bc645-2acc-4d07-87ec-159529ded17e"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                keyColumn: "Id",
                keyValue: new Guid("f43f5cdf-836b-467e-891b-48112a10f029"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("12e1cff1-6139-4631-af8e-702604e65666"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("23024474-050b-4782-91e8-2140db526a7d"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("3b207b2c-4b3a-4e09-b57a-0fc2f8c6c702"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("5c34ff2a-2f44-4b01-9579-c190402e2111"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("6c333bc7-6980-40ca-85e7-87472cebac1e"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("7a230f27-fe49-4e77-bcfc-58f9325f0ae9"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("7d604675-7b70-4546-93b4-7a9d6357c2ef"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("87139e74-2b35-407d-ab66-39c6f1d32567"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("9dd93eb6-22d3-4177-bf39-98e74d9520b7"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("d75f86f8-f249-4943-afc2-9e4a71b3ef71"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("df002f57-595f-4ea0-93eb-ba4a6ce4b1f9"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("f3756199-c246-48b5-a784-ff9459739034"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("f98a9344-4194-4c4c-ad5b-3a162aa0bd79"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("3f934caa-efa2-4e6c-b42e-e9ac8f6efa0a"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("706969aa-486e-4ee8-ac18-d05f34c70a4b"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("a416a68d-c269-406e-bd44-7a73775ef195"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("cd1483a3-4554-4108-b9bb-0e7e65819309"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("cda2e2e3-944d-4817-aea8-373547b7cb62"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("28709016-2293-478c-8287-0f06aef33f84"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("5c3c4c10-ad63-486e-85e7-23da76960ede"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("79778ad8-be41-44ec-bcf5-25a9b42a8303"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("7c4232bd-5233-48b3-863a-b8c079035fd5"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("8d44deac-24e5-4ff8-b0bd-117b3bbef882"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("9b576a00-6a70-4f0a-b996-817f016ce388"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("c2a4f4fd-4328-4c2f-81b0-a2b92a7ce067"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("db1f0ed6-9d84-4536-ba46-1db321f0fd93"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("f63bbd0b-081f-4a98-ab82-b888f09fbb4b"));

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Вопрос для ответа на который нужно выбрать только один ответ.", null, false, "Один ответ", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Вопрос для ответа на который нужно выбрать только один или несколько ответов.", null, false, "Множественный выбор", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Задание на практическое выполнение.", null, false, "Практика", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Вопрос с самостоятельным письменным или развернутым ответом.", null, false, "Открытый ответ", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "ForPromotion", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Тестовые вопросы", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Практические вопросы", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Дополнительные вопросы по регламентам", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Вопросы по развитию личностных навыков", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Практическая часть или Проектная работа", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Оценка руководителя", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Оценка сотрудника", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "CompetencySectionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Професиональные компетенции сотруника", null, true, false, "Профессиональные компетенции", 1, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Личностные компетенции сотруника", null, true, false, "Личностные компетенции", 2, null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Проведение ТО-2 ТПА", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа, выполнение действий на оборудовании", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа, выполнение действий на машине", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsControl", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Самостоятельное изучение", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Практика", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Лекции от технологов", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Лекции от сотрудников СС", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Самостоятельное изучение материала, лекции от Менеджера по СМК", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Списание инструмента с карточки сотрудника", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Заполнени отчета после испытаний совместно с технологами", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Подбор комплектующих для оборудования", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Выполнение ремонта на машине совместно с сотрудниками СС", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000010"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Навешивание ПФ совместно с сотрудниками СС", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "TypeId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Охрана труда", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Локально нормативные акты", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Технология производства", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Ремонтные работы при экстплаутаци ТПА", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Наладка и обслуживание пресс-форм", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Общие-технические знания", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Работа с технической документацией", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Отчетность", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Работа с ручным и электро инструментом", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000010"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Документы СМК", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Технологические регламенты", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Пищевая безопасность", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Организация и планирование", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000014"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Обучение и мотивация на развитие", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000015"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Управленческие навыки", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000016"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Презентация проекта", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Поиск источников", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000018"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Актуализация", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Создание таблицы", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000020"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Предоставление данных сотрудникам", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000021"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Презентация выполненного проекта", new Guid("10000000-0000-0000-0000-000000000003"), null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Подтверждение должности", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Повышение должности", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EducationType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Воспитание и обучение детей в детских садах", null, false, "Дошкольное", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "1–4 классы школы (базовые навыки чтения, письма, счета)", null, false, "Начальное общее", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "5–9 классы школы (обязательный уровень)", null, false, "Основное общее", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "10–11 классы школы (завершение общего образования)", null, false, "Среднее общее образование", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Обучение в колледжах или техникумах после 9 или 11 класса", null, false, "Среднее профессиональное образование (СПО)", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Бакалавриат", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Специалитет", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Магистратура", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Аспирантура", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000010"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Ординатура", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Ассистентура-стажировка", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Кружки, секции, творческие студии", null, false, "Дополнительное образование детей и взрослых", null, null },
                    { new Guid("10000000-0000-0000-0000-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Повышение квалификации и профессиональная переподготовка", null, false, "Дополнительное профессиональное образование (ДПО)", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Адресс электронной почты", null, true, false, "Email", 1, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Личный или рабочимй номер телефон сотрудника", null, true, false, "Телефон", 2, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Внутрений рабочимй номер телефона сотрудника", null, true, false, "Внутрений телефон", 3, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Ссылка на контакт в мессенджере WhatsApp", null, true, false, "WhatsApp", 4, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Ссылка на контакт в мессенджере Telegram", null, true, false, "Telegram", 5, null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Стажировка", 1, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Нанят", 2, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Переведен", 3, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Повышен", 4, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Обучение", 5, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Уволен", 7, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен на прежнюю должность", 8, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен с понижением", 9, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен с повышением", 10, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixItems_IsDeleted_SectionId_ParentItemId_Sort~",
                schema: "app",
                table: "CompetencyMatrixItems",
                columns: new[] { "IsDeleted", "SectionId", "ParentItemId", "SortOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompetencyMatrixItems_IsDeleted_SectionId_ParentItemId_Sort~",
                schema: "app",
                table: "CompetencyMatrixItems");

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "CompetencySectionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "CompetencySectionType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EducationType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "AttestationQuestionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("400a6b76-3848-4ed8-9f87-66781eb5ad08"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Вопрос для ответа на который нужно выбрать только один ответ.", null, false, "Один ответ", null, null },
                    { new Guid("5e8ae364-20b4-4d87-a1dd-129933dc3f98"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Задание на практическое выполнение.", null, false, "Практика", null, null },
                    { new Guid("7d13cc7d-a26d-46f5-bc97-99cd1ef970a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Вопрос для ответа на который нужно выбрать только один или несколько ответов.", null, false, "Множественный выбор", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "AttestationSectionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "ForPromotion", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("135d60c7-8db3-44c5-ba38-40941c38e79f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Дополнительные вопросы по регламентам", null, null },
                    { new Guid("3107999a-8171-4603-9920-1049d28406d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Практические вопросы", null, null },
                    { new Guid("48e20b26-4a1e-400c-bc4e-765ddfc67f5d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, false, false, "Тестовые вопросы", null, null },
                    { new Guid("664eecb2-ad07-4758-9374-840edead6e6c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Практическая часть или Проектная работа", null, null },
                    { new Guid("97694f85-3780-4fac-8f0f-f7d146482794"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Вопросы по развитию личностных навыков", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "CompetencyAssessmentType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("9f8e4471-55a6-4c35-9784-339775fb76ed"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Оценка руководителя", null, null },
                    { new Guid("ec327914-2553-417e-9293-7f1a257538e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Оценка сотрудника", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "CompetencySectionType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("483a5019-76e0-444d-8b3d-07414af3aeab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Професиональные компетенции сотруника", null, true, false, "Профессиональные компетенции", 1, null, null },
                    { new Guid("b294a9f3-5ade-4065-a651-7204b9c1d09d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Личностные компетенции сотруника", null, true, false, "Личностные компетенции", 2, null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentControlMethods",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("9a4a148b-020d-46ae-ab9a-ae25c0f74556"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа, выполнение действий на машине", null, null },
                    { new Guid("a8006951-8d75-4510-bacf-967d48779aa3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа, выполнение действий на оборудовании", null, null },
                    { new Guid("d315ce3c-2cd7-44d7-8bbe-7290307b85d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Беседа", null, null },
                    { new Guid("daa84e6a-5c60-4d07-b92f-2dc7db57ac22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Проведение ТО-2 ТПА", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentMethods",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsControl", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("016e6c89-ad0e-4969-a004-38713a329c61"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Выполнение ремонта на машине совместно с сотрудниками СС", null, null },
                    { new Guid("033d6925-6d4e-41d3-bdca-92d8c96b952f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Лекции от технологов", null, null },
                    { new Guid("08434097-599e-4676-8b88-5d083e79f5b1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Самостоятельное изучение", null, null },
                    { new Guid("3a9f9698-88b6-4834-a055-ec3935f607fe"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Практика", null, null },
                    { new Guid("65400c7b-662d-46de-bd2f-e0f035f50887"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Списание инструмента с карточки сотрудника", null, null },
                    { new Guid("81e0c037-fe5b-4b05-aaf6-f05ef72769af"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Лекции от сотрудников СС", null, null },
                    { new Guid("947f3616-84c8-4e9e-acb7-bcaae61bf220"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Заполнени отчета после испытаний совместно с технологами", null, null },
                    { new Guid("b1200c92-895a-4fb3-aa6f-4e2b9047d17b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Подбор комплектующих для оборудования", null, null },
                    { new Guid("b9ff6c64-6c01-4f25-beba-806d8c1e252d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, "Самостоятельное изучение материала, лекции от Менеджера по СМК", null, null },
                    { new Guid("fecafe0d-cc2d-400d-a388-c9dadcd21809"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, "Навешивание ПФ совместно с сотрудниками СС", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentNeeds",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "TypeId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("03ea6d8c-fb9c-45dc-b589-126e076ab4e8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Презентация проекта", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("16f677ab-0314-40d1-ab6f-90099483d770"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Работа с ручным и электро инструментом", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("1f07ecab-585f-4f33-9b0d-c6560e16a058"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Организация и планирование", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("262c7d6b-05cf-4c7f-a264-18fbf57d7fb6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Пищевая безопасность", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("278c967d-3d23-4c09-8377-6c10d34a7215"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Технология производства", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("2efde89f-66fb-4e8d-beb2-944a1a542fa4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Технологические регламенты", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("30568ab9-31ec-414a-8e30-980d764fd988"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Предоставление данных сотрудникам", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("4ad17a34-26dd-4216-8b3c-c2a01f1c5ae5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Документы СМК", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("591d1482-7c34-4377-9572-e4773ba70425"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Презентация выполненного проекта", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("5e1593bf-e290-49c6-91c3-21216ca154b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Работа с технической документацией", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("72111a66-6892-43a1-9273-c2bd394d747a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Актуализация", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("730629af-4007-4423-b604-a1f048806896"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Локально нормативные акты", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("7e4a82da-13ae-4a12-93a6-de7f39588286"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Обучение и мотивация на развитие", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("8f85a850-09ab-49e0-aa75-0cd060004e8f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Создание таблицы", new Guid("10000000-0000-0000-0000-000000000003"), null, null },
                    { new Guid("8fc2deb1-3258-402f-a744-645a34fa67b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Управленческие навыки", new Guid("10000000-0000-0000-0000-000000000002"), null, null },
                    { new Guid("9b5a97f6-0e59-4ecb-9545-043d382c0fb8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Общие-технические знания", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("a53f0d6c-221e-4618-beab-fe17d1e0809c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Охрана труда", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("a6afe381-7e69-40d2-afb3-783eef10cbfc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Наладка и обслуживание пресс-форм", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("ae8188b3-5c67-4d7b-9854-fbde5a6aa84d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Отчетность", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("bc103f5f-edfb-4c3e-a826-7069733e1dac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Ремонтные работы при экстплаутаци ТПА", new Guid("10000000-0000-0000-0000-000000000001"), null, null },
                    { new Guid("dc0df98f-c941-4587-9626-1f228720fca9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, "Поиск источников", new Guid("10000000-0000-0000-0000-000000000003"), null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "DevelopmentPlanType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("e18bc645-2acc-4d07-87ec-159529ded17e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Повышение должности", null, null },
                    { new Guid("f43f5cdf-836b-467e-891b-48112a10f029"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, "Подтверждение должности", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EducationType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("12e1cff1-6139-4631-af8e-702604e65666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "1–4 классы школы (базовые навыки чтения, письма, счета)", null, false, "Начальное общее", null, null },
                    { new Guid("23024474-050b-4782-91e8-2140db526a7d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Повышение квалификации и профессиональная переподготовка", null, false, "Дополнительное профессиональное образование (ДПО)", null, null },
                    { new Guid("3b207b2c-4b3a-4e09-b57a-0fc2f8c6c702"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Ординатура", null, null },
                    { new Guid("5c34ff2a-2f44-4b01-9579-c190402e2111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Магистратура", null, null },
                    { new Guid("6c333bc7-6980-40ca-85e7-87472cebac1e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "10–11 классы школы (завершение общего образования)", null, false, "Среднее общее образование", null, null },
                    { new Guid("7a230f27-fe49-4e77-bcfc-58f9325f0ae9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Аспирантура", null, null },
                    { new Guid("7d604675-7b70-4546-93b4-7a9d6357c2ef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Воспитание и обучение детей в детских садах", null, false, "Дошкольное", null, null },
                    { new Guid("87139e74-2b35-407d-ab66-39c6f1d32567"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка кадров высшей квалификации", null, false, "Ассистентура-стажировка", null, null },
                    { new Guid("9dd93eb6-22d3-4177-bf39-98e74d9520b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Обучение в колледжах или техникумах после 9 или 11 класса", null, false, "Среднее профессиональное образование (СПО)", null, null },
                    { new Guid("d75f86f8-f249-4943-afc2-9e4a71b3ef71"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Кружки, секции, творческие студии", null, false, "Дополнительное образование детей и взрослых", null, null },
                    { new Guid("df002f57-595f-4ea0-93eb-ba4a6ce4b1f9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Специалитет", null, null },
                    { new Guid("f3756199-c246-48b5-a784-ff9459739034"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "5–9 классы школы (обязательный уровень)", null, false, "Основное общее", null, null },
                    { new Guid("f98a9344-4194-4c4c-ad5b-3a162aa0bd79"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Высшее образование — подготовка высококвалифицированных кадров", null, false, "Бакалавриат", null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EmployeeContactType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("3f934caa-efa2-4e6c-b42e-e9ac8f6efa0a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Внутрений рабочимй номер телефона сотрудника", null, true, false, "Внутрений телефон", 3, null, null },
                    { new Guid("706969aa-486e-4ee8-ac18-d05f34c70a4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Ссылка на контакт в мессенджере WhatsApp", null, true, false, "WhatsApp", 4, null, null },
                    { new Guid("a416a68d-c269-406e-bd44-7a73775ef195"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Адресс электронной почты", null, true, false, "Email", 1, null, null },
                    { new Guid("cd1483a3-4554-4108-b9bb-0e7e65819309"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Личный или рабочимй номер телефон сотрудника", null, true, false, "Телефон", 2, null, null },
                    { new Guid("cda2e2e3-944d-4817-aea8-373547b7cb62"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Ссылка на контакт в мессенджере Telegram", null, true, false, "Telegram", 5, null, null }
                });

            migrationBuilder.InsertData(
                schema: "app_lookup",
                table: "EmploymentRecordType",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "FileId", "IsActive", "IsDeleted", "Name", "SortOrder", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("28709016-2293-478c-8287-0f06aef33f84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен с повышением", 10, null, null },
                    { new Guid("5c3c4c10-ad63-486e-85e7-23da76960ede"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Повышен", 4, null, null },
                    { new Guid("79778ad8-be41-44ec-bcf5-25a9b42a8303"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Уволен", 7, null, null },
                    { new Guid("7c4232bd-5233-48b3-863a-b8c079035fd5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Нанят", 2, null, null },
                    { new Guid("8d44deac-24e5-4ff8-b0bd-117b3bbef882"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Обучение", 5, null, null },
                    { new Guid("9b576a00-6a70-4f0a-b996-817f016ce388"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен с понижением", 9, null, null },
                    { new Guid("c2a4f4fd-4328-4c2f-81b0-a2b92a7ce067"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Востановлен на прежнюю должность", 8, null, null },
                    { new Guid("db1f0ed6-9d84-4536-ba46-1db321f0fd93"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Стажировка", 1, null, null },
                    { new Guid("f63bbd0b-081f-4a98-ab82-b888f09fbb4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, true, false, "Переведен", 3, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyMatrixItems_IsDeleted_SectionId",
                schema: "app",
                table: "CompetencyMatrixItems",
                columns: new[] { "IsDeleted", "SectionId" },
                unique: true);
        }
    }
}
