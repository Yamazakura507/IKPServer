using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Resolvers.Interfaces;
using IKP.Application.Diagnostics.Resolvers.Models;
using IKP.Domain.Common.Codes.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Areas;
using System.Text.RegularExpressions;

namespace IKP.Application.Diagnostics.Resolvers
{
    /// <summary>
    /// Resolves diagnostic areas using configured resolution mappings.
    /// </summary>
    public class DiagnosticAreaResolver : IDiagnosticAreaResolver
    {
        private const int ResolutionVersion = 2;
        private const decimal MinimumConfidence = 0m;
        private const decimal MaximumConfidence = 100m;
        private const decimal PriorityMinimumFactor = 0.5m;
        private const decimal PriorityMaximum = 100m;
        private const int RegexTimeoutMilliseconds = 100;

        private readonly IDiagnosticRepository diagnosticRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticAreaResolver"/> class.
        /// </summary>
        /// <param name="diagnosticRepository">Diagnostic repository.</param>
        public DiagnosticAreaResolver(IDiagnosticRepository diagnosticRepository)
        {
            this.diagnosticRepository = diagnosticRepository;
        }

        /// <inheritdoc />
        public async ValueTask<DiagnosticAreaResolution?> ResolveAsync(DiagnosticAreaResolutionInputModel input, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(input);

            IReadOnlyList<DiagnosticAreaResolutionMappingModel> mappings = await diagnosticRepository.GetActiveDiagnosticAreaResolutionMappingsAsync(cancellationToken);
            Dictionary<Guid, List<DiagnosticAreaResolutionEvidenceModel>> evidencesByArea = new();

            foreach (DiagnosticAreaResolutionMappingModel mapping in mappings)
            {
                string? value = GetRuleValue(mapping.RuleCode, input);

                if (string.IsNullOrWhiteSpace(value) || !IsMatch(mapping.Pattern, value)) continue;

                DiagnosticAreaResolutionEvidenceModel evidence = CreateEvidence(mapping, value);

                if (!evidencesByArea.TryGetValue(mapping.DiagnosticAreaId, out List<DiagnosticAreaResolutionEvidenceModel>? evidences))
                {
                    evidences = new List<DiagnosticAreaResolutionEvidenceModel>();
                    evidencesByArea.Add(mapping.DiagnosticAreaId, evidences);
                }

                evidences.Add(evidence);
            }

            List<DiagnosticAreaResolutionCandidateModel> candidates = CreateCandidates(evidencesByArea);
            DiagnosticAreaResolutionCandidateModel? winner = candidates
                .OrderByDescending(x => x.Confidence)
                .ThenByDescending(x => x.Evidences.Sum(y => y.Priority))
                .ThenBy(x => x.DiagnosticAreaId)
                .FirstOrDefault();

            if (winner is null) return null;

            return await diagnosticRepository.CreateAutomaticDiagnosticAreaResolutionAsync(
                winner.DiagnosticAreaId,
                winner.PrimaryRuleId,
                winner.Confidence,
                ResolutionVersion,
                winner.Evidences,
                cancellationToken);
        }

        /// <summary>
        /// Creates candidate areas from matched rule evidence.
        /// </summary>
        /// <param name="evidencesByArea">Matched evidence grouped by area.</param>
        /// <returns>Diagnostic area candidates.</returns>
        private static List<DiagnosticAreaResolutionCandidateModel> CreateCandidates(Dictionary<Guid, List<DiagnosticAreaResolutionEvidenceModel>> evidencesByArea)
        {
            List<DiagnosticAreaResolutionCandidateModel> candidates = new();

            foreach (KeyValuePair<Guid, List<DiagnosticAreaResolutionEvidenceModel>> pair in evidencesByArea)
            {
                List<DiagnosticAreaResolutionEvidenceModel> distinctRuleEvidence = pair.Value
                    .GroupBy(x => x.RuleId)
                    .Select(x => x.OrderByDescending(y => y.Contribution).ThenByDescending(y => y.Priority).First())
                    .OrderByDescending(x => x.Contribution)
                    .ToList();

                decimal remainingProbability = 1m;

                foreach (DiagnosticAreaResolutionEvidenceModel evidence in distinctRuleEvidence)
                {
                    remainingProbability *= 1m - evidence.Contribution / MaximumConfidence;
                }

                decimal aggregateConfidence = Math.Clamp((1m - remainingProbability) * MaximumConfidence, MinimumConfidence, MaximumConfidence);
                DiagnosticAreaResolutionEvidenceModel primaryEvidence = distinctRuleEvidence[0];

                candidates.Add(new DiagnosticAreaResolutionCandidateModel
                {
                    DiagnosticAreaId = pair.Key,
                    Confidence = Math.Round(aggregateConfidence, 2, MidpointRounding.AwayFromZero),
                    PrimaryRuleId = primaryEvidence.RuleId,
                    Evidences = distinctRuleEvidence
                });
            }

            return candidates;
        }

        /// <summary>
        /// Creates an evidence record from a matched mapping.
        /// </summary>
        /// <param name="mapping">Matched mapping.</param>
        /// <param name="matchedValue">Value matched by the mapping.</param>
        /// <returns>Evidence record.</returns>
        private static DiagnosticAreaResolutionEvidenceModel CreateEvidence(DiagnosticAreaResolutionMappingModel mapping, string matchedValue)
        {
            decimal priorityFactor = PriorityMinimumFactor + mapping.Priority / PriorityMaximum * (1m - PriorityMinimumFactor);
            decimal contribution = Math.Clamp(mapping.Confidence * priorityFactor, MinimumConfidence, MaximumConfidence);

            return new DiagnosticAreaResolutionEvidenceModel
            {
                MappingId = mapping.Id,
                RuleId = mapping.RuleId,
                Pattern = mapping.Pattern,
                MatchedValue = matchedValue.Length <= 4000 ? matchedValue : matchedValue[..4000],
                Priority = mapping.Priority,
                Confidence = mapping.Confidence,
                Contribution = Math.Round(contribution, 2, MidpointRounding.AwayFromZero)
            };
        }

        /// <summary>
        /// Gets the value associated with a resolution rule.
        /// </summary>
        /// <param name="ruleCode">Resolution rule code.</param>
        /// <param name="input">Resolution input.</param>
        /// <returns>The value to test or null.</returns>
        private static string? GetRuleValue(string ruleCode, DiagnosticAreaResolutionInputModel input)
        {
            return ruleCode switch
            {
                DiagnosticAreaResolutionRuleCodes.ExceptionType => input.ExceptionType,
                DiagnosticAreaResolutionRuleCodes.Namespace => input.Namespace,
                DiagnosticAreaResolutionRuleCodes.Endpoint => input.Endpoint,
                DiagnosticAreaResolutionRuleCodes.HttpPipeline => CreateHttpPipelineValue(input),
                _ => null
            };
        }

        /// <summary>
        /// Creates the normalized value used by HTTP pipeline mappings.
        /// </summary>
        /// <param name="input">Resolution input.</param>
        /// <returns>HTTP pipeline value.</returns>
        private static string? CreateHttpPipelineValue(DiagnosticAreaResolutionInputModel input)
        {
            string value = string.Join(" ", new[] { input.HttpMethod, input.Route, input.Endpoint }.Where(x => !string.IsNullOrWhiteSpace(x)));
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        /// <summary>
        /// Tests a configured regular expression without allowing a pathological expression to block error processing.
        /// </summary>
        /// <param name="pattern">Regular expression pattern.</param>
        /// <param name="value">Value to match.</param>
        /// <returns>True when the pattern matches.</returns>
        private static bool IsMatch(string pattern, string value)
        {
            try
            {
                return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(RegexTimeoutMilliseconds));
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
