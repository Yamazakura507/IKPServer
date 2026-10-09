using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Errors;

namespace IKP.Domain.Entities.Diagnostics.Statistics
{
    public class ErrorStatistics : Entity
    {
        public Guid ErrorGroupId { get; set; }

        public ErrorGroup ErrorGroup { get; set; } = null!;

        public long TotalOccurrences { get; set; }

        public long UniqueUsers { get; set; }

        public decimal AffectedUserRatio { get; set; }

        public long OccurrencesLastHour { get; set; }

        public long OccurrencesLastDay { get; set; }

        public long OccurrencesLast7Days { get; set; }

        public long UsersLastHour { get; set; }

        public long UsersLastDay { get; set; }

        public long UsersLast7Days { get; set; }

        public decimal GrowthRate { get; set; }

        public DateTime CalculatedAt { get; set; }
    }
}
