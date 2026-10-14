using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Domain.ValueObjects.Diagnostics;
using System.Text.Json;

namespace IKP.Domain.Entities.Diagnostics.Errors
{
    public class ErrorOccurrence : Entity
    {
        public Guid ErrorGroupId { get; set; }

        public Guid? DiagnosticAreaResolutionId { get; set; }

        public DateTime OccurredAt { get; set; }

        public JsonDocument? Context { get; set; }

        public ErrorActor Actor { get; set; } = new();

        public ErrorApplicationContext Application { get; set; } = new();

        public ErrorRequestContext Request { get; set; } = new();

        public ErrorExceptionDetails Exception { get; set; } = new();


        public ErrorGroup ErrorGroup { get; set; } = null!;

        public DiagnosticAreaResolution? DiagnosticAreaResolution { get; set; }
    }
}
