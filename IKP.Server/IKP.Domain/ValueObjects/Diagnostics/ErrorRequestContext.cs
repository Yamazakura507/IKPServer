namespace IKP.Domain.ValueObjects.Diagnostics
{
    public class ErrorRequestContext
    {
        public string? RequestId { get; set; }

        public string? CorrelationId { get; set; }

        public string? Endpoint { get; set; }

        public string? HttpMethod { get; set; }

        public string? Route { get; set; }
    }
}
