using IKP.Application.Diagnostics.Criticality.Models;
using IKP.Domain.Common.Codes.Diagnostics;

namespace IKP.Application.Diagnostics.Criticality.Services
{
    /// <summary>
    /// Calculates diagnostic criticality from persisted operational metrics.
    /// </summary>
    public class DiagnosticCriticalityCalculator
    {
        private const decimal OccurrenceRateWeight = 0.20m;
        private const decimal AffectedUsersWeight = 0.15m;
        private const decimal AffectedUserRatioWeight = 0.15m;
        private const decimal FeatureImportanceWeight = 0.20m;
        private const decimal TrendWeight = 0.10m;
        private const decimal FailureRateWeight = 0.05m;
        private const decimal RegressionWeight = 0.05m;
        private const decimal BusinessImpactWeight = 0.04m;
        private const decimal SupportImpactWeight = 0.03m;
        private const decimal DataLossRiskWeight = 0.03m;

        /// <summary>
        /// Calculates automatic criticality and its factor breakdown.
        /// </summary>
        /// <param name="input">Persisted diagnostic metrics.</param>
        /// <returns>Calculated criticality.</returns>
        public DiagnosticCriticalityCalculationResult Calculate(DiagnosticCriticalityCalculationInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            List<DiagnosticCriticalityFactorResult> factors = new()
            {
                CreateAvailable(CriticalityFactorCodes.OccurrenceRate, ScoreOccurrences(input.OccurrencesLastHour), OccurrenceRateWeight, input.OccurrencesLastHour),
                CreateAvailable(CriticalityFactorCodes.AffectedUsers, ScoreUsers(input.UsersLastHour), AffectedUsersWeight, input.UsersLastHour),
                CreateAvailable(CriticalityFactorCodes.AffectedUserRatio, Clamp(input.AffectedUserRatio), AffectedUserRatioWeight, input.AffectedUserRatio),
                CreateOptional(CriticalityFactorCodes.FeatureImportance, input.FeatureImportance, FeatureImportanceWeight),
                CreateOptional(CriticalityFactorCodes.Trend, input.GrowthRate.HasValue ? ScoreTrend(input.GrowthRate) : null, TrendWeight, input.GrowthRate),
                CreateUnavailable(CriticalityFactorCodes.FailureRate, FailureRateWeight),
                CreateUnavailable(CriticalityFactorCodes.Regression, RegressionWeight),
                CreateUnavailable(CriticalityFactorCodes.BusinessImpact, BusinessImpactWeight),
                CreateUnavailable(CriticalityFactorCodes.SupportImpact, SupportImpactWeight),
                CreateUnavailable(CriticalityFactorCodes.DataLossRisk, DataLossRiskWeight)
            };

            decimal totalWeight = factors.Where(x => x.IsAvailable).Sum(x => x.Weight);
            decimal automaticScore = totalWeight <= 0 ? 0 : Math.Round(factors.Where(x => x.IsAvailable).Sum(x => x.Score * x.Weight) / totalWeight, 2);

            return new DiagnosticCriticalityCalculationResult
            {
                AutomaticScore = Clamp(automaticScore),
                Factors = factors
            };
        }

        private static DiagnosticCriticalityFactorResult CreateAvailable(string code, decimal score, decimal weight, decimal value) => new()
        {
            Code = code,
            Score = Clamp(score),
            Weight = weight,
            Value = value,
            IsAvailable = true
        };

        private static DiagnosticCriticalityFactorResult CreateOptional(string code, decimal? value, decimal weight, decimal? rawValue = null) => new()
        {
            Code = code,
            Score = value.HasValue ? Clamp(value.Value) : 0,
            Weight = weight,
            Value = rawValue ?? value,
            IsAvailable = value.HasValue
        };

        private static DiagnosticCriticalityFactorResult CreateUnavailable(string code, decimal weight) => new()
        {
            Code = code,
            Score = 0,
            Weight = weight,
            Value = null,
            IsAvailable = false
        };

        private static decimal ScoreOccurrences(long value)
        {
            if (value <= 0) return 0;
            if (value == 1) return 10;
            if (value <= 5) return 25;
            if (value <= 20) return 50;
            if (value <= 50) return 70;
            if (value <= 100) return 85;
            return 100;
        }

        private static decimal ScoreUsers(long value)
        {
            if (value <= 0) return 0;
            if (value == 1) return 15;
            if (value <= 5) return 30;
            if (value <= 20) return 55;
            if (value <= 50) return 75;
            if (value <= 100) return 90;
            return 100;
        }

        private static decimal ScoreTrend(decimal? growthRate)
        {
            if (!growthRate.HasValue || growthRate.Value <= -50) return 0;
            if (growthRate.Value <= 0) return 25;
            if (growthRate.Value <= 25) return 50;
            if (growthRate.Value <= 50) return 70;
            if (growthRate.Value <= 100) return 85;
            return 100;
        }

        private static decimal Clamp(decimal value) => Math.Clamp(value, 0, 100);
    }
}
