using IKP.Domain.Common.Base;
using IKP.Domain.ValueObjects.Diagnostics;
using System.Text.Json;

namespace IKP.Domain.Entities.Diagnostics.Errors
{
    public class ErrorOccurrence : Entity
    {
        public Guid ErrorGroupId { get; set; }

        public ErrorGroup ErrorGroup { get; set; } = null!;

        public DateTime OccurredAt { get; set; }

        public ErrorActor Actor { get; set; } = new();

        public ErrorApplicationContext Application { get; set; } = new();

        public ErrorRequestContext Request { get; set; } = new();

        public ErrorExceptionDetails Exception { get; set; } = new();

        public JsonDocument? Context { get; set; }
    }
}
