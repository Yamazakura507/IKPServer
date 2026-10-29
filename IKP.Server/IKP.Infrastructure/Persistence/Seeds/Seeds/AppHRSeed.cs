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
                    new AttestationQuestionType() { Name = "Один ответ", Description = "Вопрос для ответа на который нужно выбрать только один ответ." },
                    new AttestationQuestionType() { Name = "Множественный выбор", Description = "Вопрос для ответа на который нужно выбрать только один или несколько ответов." },
                    new AttestationQuestionType() { Name = "Практика", Description = "Задание на практическое выполнение." }
                });
            modelBuilder.Entity<AttestationSectionType>().HasData(
                new[]
                {
                    new AttestationSectionType() { Name = "Тестовые вопросы" },
                    new AttestationSectionType() { Name = "Практические вопросы" },
                    new AttestationSectionType() { Name = "Дополнительные вопросы по регламентам" },
                    new AttestationSectionType() { Name = "Вопросы по развитию личностных навыков", ForPromotion = true },
                    new AttestationSectionType() { Name = "Практическая часть или Проектная работа", ForPromotion = true }
                });
            modelBuilder.Entity<CompetencyAssessmentType>().HasData(
                new[]
                {
                    new CompetencyAssessmentType() { Name = "Оценка руководителя" },
                    new CompetencyAssessmentType() { Name = "Оценка сотрудника" }
                });
            modelBuilder.Entity<CompetencySectionType>().HasData(
                new[]
                {
                    new CompetencySectionType() { Name = "Профессиональные компетенции", Description = "Професиональные компетенции сотруника", IsActive = true, SortOrder = 1 },
                    new CompetencySectionType() { Name = "Личностные компетенции", Description = "Личностные компетенции сотруника", IsActive = true, SortOrder = 2 }
                });
            modelBuilder.Entity<DevelopmentPlanType>().HasData(
                new[]
                {
                    new DevelopmentPlanType() { Name = "Подтверждение должности" },
                    new DevelopmentPlanType() { Name = "Повышение должности" }
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
                    new EducationType() { Name = "Дошкольное", Description = "Воспитание и обучение детей в детских садах" },
                    new EducationType() { Name = "Начальное общее", Description = "1–4 классы школы (базовые навыки чтения, письма, счета)" },
                    new EducationType() { Name = "Основное общее", Description = "5–9 классы школы (обязательный уровень)" },
                    new EducationType() { Name = "Среднее общее образование", Description = "10–11 классы школы (завершение общего образования)" },
                    new EducationType() { Name = "Среднее профессиональное образование (СПО)", Description = "Обучение в колледжах или техникумах после 9 или 11 класса" },
                    new EducationType() { Name = "Бакалавриат", Description = "Высшее образование — подготовка высококвалифицированных кадров" },
                    new EducationType() { Name = "Специалитет", Description = "Высшее образование — подготовка высококвалифицированных кадров" },
                    new EducationType() { Name = "Магистратура", Description = "Высшее образование — подготовка высококвалифицированных кадров" },
                    new EducationType() { Name = "Аспирантура", Description = "Высшее образование — подготовка кадров высшей квалификации" },
                    new EducationType() { Name = "Ординатура", Description = "Высшее образование — подготовка кадров высшей квалификации" },
                    new EducationType() { Name = "Ассистентура-стажировка", Description = "Высшее образование — подготовка кадров высшей квалификации" },
                    new EducationType() { Name = "Дополнительное образование детей и взрослых", Description = "Кружки, секции, творческие студии" },
                    new EducationType() { Name = "Дополнительное профессиональное образование (ДПО)", Description = "Повышение квалификации и профессиональная переподготовка" }
                });
            modelBuilder.Entity<EmployeeContactType>().HasData(
                new[]
                {
                    new EmployeeContactType() { Name = "Email", Description = "Адресс электронной почты", IsActive = true, SortOrder = 1 },
                    new EmployeeContactType() { Name = "Телефон", Description = "Личный или рабочимй номер телефон сотрудника", IsActive = true, SortOrder = 2 },
                    new EmployeeContactType() { Name = "Внутрений телефон", Description = "Внутрений рабочимй номер телефона сотрудника", IsActive = true, SortOrder = 3 },
                    new EmployeeContactType() { Name = "WhatsApp", Description = "Ссылка на контакт в мессенджере WhatsApp", IsActive = true, SortOrder = 4 },
                    new EmployeeContactType() { Name = "Telegram", Description = "Ссылка на контакт в мессенджере Telegram", IsActive = true, SortOrder = 5 }
                });
            modelBuilder.Entity<EmploymentRecordType>().HasData(
                new[]
                {
                    new EmploymentRecordType() { Name = "Стажировка", IsActive = true, SortOrder = 1 },
                    new EmploymentRecordType() { Name = "Нанят", IsActive = true, SortOrder = 2 },
                    new EmploymentRecordType() { Name = "Переведен", IsActive = true, SortOrder = 3 },
                    new EmploymentRecordType() { Name = "Повышен", IsActive = true, SortOrder = 4 },
                    new EmploymentRecordType() { Name = "Обучение", IsActive = true, SortOrder = 5 },
                    new EmploymentRecordType() { Name = "Уволен", IsActive = true, SortOrder = 7 },
                    new EmploymentRecordType() { Name = "Востановлен на прежнюю должность", IsActive = true, SortOrder = 8 },
                    new EmploymentRecordType() { Name = "Востановлен с понижением", IsActive = true, SortOrder = 9 },
                    new EmploymentRecordType() { Name = "Востановлен с повышением", IsActive = true, SortOrder = 10 }
                });
            modelBuilder.Entity<DevelopmentControlMethods>().HasData(
                new[]
                {
                    new DevelopmentControlMethods() { Name = "Беседа" },
                    new DevelopmentControlMethods() { Name = "Проведение ТО-2 ТПА" },
                    new DevelopmentControlMethods() { Name = "Беседа, выполнение действий на оборудовании" },
                    new DevelopmentControlMethods() { Name = "Беседа, выполнение действий на машине" }
                });
            modelBuilder.Entity<DevelopmentMethods>().HasData(
                new[]
                {
                    new DevelopmentMethods() { Name = "Самостоятельное изучение" },
                    new DevelopmentMethods() { Name = "Практика" },
                    new DevelopmentMethods() { Name = "Лекции от технологов" },
                    new DevelopmentMethods() { Name = "Лекции от сотрудников СС", IsControl = true },
                    new DevelopmentMethods() { Name = "Самостоятельное изучение материала, лекции от Менеджера по СМК" },
                    new DevelopmentMethods() { Name = "Списание инструмента с карточки сотрудника" },
                    new DevelopmentMethods() { Name = "Заполнени отчета после испытаний совместно с технологами" },
                    new DevelopmentMethods() { Name = "Подбор комплектующих для оборудования" },
                    new DevelopmentMethods() { Name = "Выполнение ремонта на машине совместно с сотрудниками СС", IsControl = true },
                    new DevelopmentMethods() { Name = "Навешивание ПФ совместно с сотрудниками СС", IsControl = true }
                });
            modelBuilder.Entity<DevelopmentNeeds>().HasData(
                new[]
                {
                    new DevelopmentNeeds() { Name = "Охрана труда", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Локально нормативные акты", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Технология производства", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Ремонтные работы при экстплаутаци ТПА", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Наладка и обслуживание пресс-форм", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Общие-технические знания", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Работа с технической документацией", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Отчетность", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Работа с ручным и электро инструментом", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Документы СМК", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Технологические регламенты", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Пищевая безопасность", TypeId = ProffesionalDevItem },
                    new DevelopmentNeeds() { Name = "Организация и планирование", TypeId = PersonalDevItem },
                    new DevelopmentNeeds() { Name = "Обучение и мотивация на развитие", TypeId = PersonalDevItem },
                    new DevelopmentNeeds() { Name = "Управленческие навыки", TypeId = PersonalDevItem },
                    new DevelopmentNeeds() { Name = "Презентация проекта", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Name = "Поиск источников", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Name = "Актуализация", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Name = "Создание таблицы", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Name = "Предоставление данных сотрудникам", TypeId = ProjectDevItem },
                    new DevelopmentNeeds() { Name = "Презентация выполненного проекта", TypeId = ProjectDevItem }
                });
        }
    }
}
