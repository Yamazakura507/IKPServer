using IKP.Domain.Entities.App.HR.Lookup;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds.Seeds
{
    public static class AppHRSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            uint baseIndex = 1;
            Guid ProffesionalDevItem = baseIndex.GetGuidForLevel(0, 1);
            Guid PersonalDevItem = baseIndex.GetGuidForLevel(0, 2);
            Guid ProjectDevItem = baseIndex.GetGuidForLevel(0, 3);

            modelBuilder.Entity<AttestationQuestionType>().HasData(
                new[]
                {
                    new AttestationQuestionType() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Один ответ", Description = "Вопрос для ответа на который нужно выбрать только один ответ." },
                    new AttestationQuestionType() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Множественный выбор", Description = "Вопрос для ответа на который нужно выбрать только один или несколько ответов." },
                    new AttestationQuestionType() { Id = baseIndex.GetGuidForLevel(0, 3), Name = "Практика", Description = "Задание на практическое выполнение." },
                    new AttestationQuestionType() { Id = baseIndex.GetGuidForLevel(0, 4), Name = "Открытый ответ", Description = "Вопрос с самостоятельным письменным или развернутым ответом." }
                });
            modelBuilder.Entity<AttestationSectionType>().HasData(
                new[]
                {
                    new AttestationSectionType() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Тестовые вопросы" },
                    new AttestationSectionType() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Практические вопросы" },
                    new AttestationSectionType() { Id = baseIndex.GetGuidForLevel(0, 3), Name = "Дополнительные вопросы по регламентам" },
                    new AttestationSectionType() { Id = baseIndex.GetGuidForLevel(0, 4), Name = "Вопросы по развитию личностных навыков", ForPromotion = true },
                    new AttestationSectionType() { Id = baseIndex.GetGuidForLevel(0, 5), Name = "Практическая часть или Проектная работа", ForPromotion = true }
                });
            modelBuilder.Entity<CompetencyAssessmentType>().HasData(
                new[]
                {
                    new CompetencyAssessmentType() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Оценка руководителя" },
                    new CompetencyAssessmentType() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Оценка сотрудника" }
                });
            modelBuilder.Entity<CompetencySectionType>().HasData(
                new[]
                {
                    new CompetencySectionType() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Профессиональные компетенции", Description = "Професиональные компетенции сотруника", IsActive = true, SortOrder = 1 },
                    new CompetencySectionType() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Личностные компетенции", Description = "Личностные компетенции сотруника", IsActive = true, SortOrder = 2 }
                });
            modelBuilder.Entity<DevelopmentPlanType>().HasData(
                new[]
                {
                    new DevelopmentPlanType() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Подтверждение должности" },
                    new DevelopmentPlanType() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Повышение должности" }
                });
            modelBuilder.Entity<DevelopmentItemType>().HasData(
                new[]
                {
                    new DevelopmentItemType() { Id = ProffesionalDevItem, Name = "Профессиональные компетенции" },
                    new DevelopmentItemType() { Id = PersonalDevItem, Name = "Личностные компетенции" },
                    new DevelopmentItemType() { Id = ProjectDevItem, Name = "Проект" }
                });
            modelBuilder.Entity<EducationType>().HasData(
                new[]
                {
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Дошкольное", Description = "Воспитание и обучение детей в детских садах" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Начальное общее", Description = "1–4 классы школы (базовые навыки чтения, письма, счета)" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 3), Name = "Основное общее", Description = "5–9 классы школы (обязательный уровень)" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 4), Name = "Среднее общее образование", Description = "10–11 классы школы (завершение общего образования)" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 5), Name = "Среднее профессиональное образование (СПО)", Description = "Обучение в колледжах или техникумах после 9 или 11 класса" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 6), Name = "Бакалавриат", Description = "Высшее образование — подготовка высококвалифицированных кадров" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 7), Name = "Специалитет", Description = "Высшее образование — подготовка высококвалифицированных кадров" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 8), Name = "Магистратура", Description = "Высшее образование — подготовка высококвалифицированных кадров" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 9), Name = "Аспирантура", Description = "Высшее образование — подготовка кадров высшей квалификации" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 10), Name = "Ординатура", Description = "Высшее образование — подготовка кадров высшей квалификации" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 11), Name = "Ассистентура-стажировка", Description = "Высшее образование — подготовка кадров высшей квалификации" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 12), Name = "Дополнительное образование детей и взрослых", Description = "Кружки, секции, творческие студии" },
                    new EducationType() { Id = baseIndex.GetGuidForLevel(0, 13), Name = "Дополнительное профессиональное образование (ДПО)", Description = "Повышение квалификации и профессиональная переподготовка" }
                });
            modelBuilder.Entity<EmployeeContactType>().HasData(
                new[]
                {
                    new EmployeeContactType() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Email", Description = "Адресс электронной почты", IsActive = true, SortOrder = 1 },
                    new EmployeeContactType() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Телефон", Description = "Личный или рабочимй номер телефон сотрудника", IsActive = true, SortOrder = 2 },
                    new EmployeeContactType() { Id = baseIndex.GetGuidForLevel(0, 3), Name = "Внутрений телефон", Description = "Внутрений рабочимй номер телефона сотрудника", IsActive = true, SortOrder = 3 },
                    new EmployeeContactType() { Id = baseIndex.GetGuidForLevel(0, 4), Name = "WhatsApp", Description = "Ссылка на контакт в мессенджере WhatsApp", IsActive = true, SortOrder = 4 },
                    new EmployeeContactType() { Id = baseIndex.GetGuidForLevel(0, 5), Name = "Telegram", Description = "Ссылка на контакт в мессенджере Telegram", IsActive = true, SortOrder = 5 }
                });
            modelBuilder.Entity<EmploymentRecordType>().HasData(
                new[]
                {
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Стажировка", IsActive = true, SortOrder = 1 },
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Нанят", IsActive = true, SortOrder = 2 },
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 3), Name = "Переведен", IsActive = true, SortOrder = 3 },
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 4), Name = "Повышен", IsActive = true, SortOrder = 4 },
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 5), Name = "Обучение", IsActive = true, SortOrder = 5 },
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 6), Name = "Уволен", IsActive = true, SortOrder = 7 },
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 7), Name = "Востановлен на прежнюю должность", IsActive = true, SortOrder = 8 },
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 8), Name = "Востановлен с понижением", IsActive = true, SortOrder = 9 },
                    new EmploymentRecordType() { Id = baseIndex.GetGuidForLevel(0, 9), Name = "Востановлен с повышением", IsActive = true, SortOrder = 10 }
                });
            modelBuilder.Entity<DevelopmentControlMethods>().HasData(
                new[]
                {
                    new DevelopmentControlMethods() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Беседа" },
                    new DevelopmentControlMethods() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Проведение ТО-2 ТПА" },
                    new DevelopmentControlMethods() { Id = baseIndex.GetGuidForLevel(0, 3), Name = "Беседа, выполнение действий на оборудовании" },
                    new DevelopmentControlMethods() { Id = baseIndex.GetGuidForLevel(0, 4), Name = "Беседа, выполнение действий на машине" }
                });
            modelBuilder.Entity<DevelopmentMethods>().HasData(
                new[]
                {
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Самостоятельное изучение" },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Практика" },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 3), Name = "Лекции от технологов" },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 4), Name = "Лекции от сотрудников СС", IsControl = true },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 5), Name = "Самостоятельное изучение материала, лекции от Менеджера по СМК" },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 6), Name = "Списание инструмента с карточки сотрудника" },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 7), Name = "Заполнени отчета после испытаний совместно с технологами" },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 8), Name = "Подбор комплектующих для оборудования" },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 9), Name = "Выполнение ремонта на машине совместно с сотрудниками СС", IsControl = true },
                    new DevelopmentMethods() { Id = baseIndex.GetGuidForLevel(0, 10), Name = "Навешивание ПФ совместно с сотрудниками СС", IsControl = true }
                });
            modelBuilder.Entity<DevelopmentNeeds>().HasData(
                new[]
                {
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 1), Name = "Охрана труда", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 2), Name = "Локально нормативные акты", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 3), Name = "Технология производства", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 4),  Name = "Ремонтные работы при экстплаутаци ТПА", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 5), Name = "Наладка и обслуживание пресс-форм", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 6), Name = "Общие-технические знания", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 7), Name = "Работа с технической документацией", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 8), Name = "Отчетность", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 9), Name = "Работа с ручным и электро инструментом", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 10), Name = "Документы СМК", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 11), Name = "Технологические регламенты", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 12), Name = "Пищевая безопасность", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 13), Name = "Организация и планирование", TypeId = PersonalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 14), Name = "Обучение и мотивация на развитие", TypeId = PersonalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 15), Name = "Управленческие навыки", TypeId = PersonalDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 16), Name = "Презентация проекта", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 17), Name = "Поиск источников", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 18), Name = "Актуализация", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 19), Name = "Создание таблицы", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 20), Name = "Предоставление данных сотрудникам", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Id = baseIndex.GetGuidForLevel(0, 21), Name = "Презентация выполненного проекта", TypeId = ProjectDevItem }
                });
        }
    }
}
