using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.Diagnostics.Statistics
{
    /// <summary>
    /// Defines a processing state of a diagnostic statistics work item.
    /// </summary>
    public class DiagnosticStatisticsWorkItemStatus : FullLookupLocalized
    {
        /// <summary>
        /// Gets work items that use this status.
        /// </summary>
        public ICollection<DiagnosticStatisticsWorkItem> WorkItems { get; set; } = new List<DiagnosticStatisticsWorkItem>();
    }
}
