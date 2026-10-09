namespace IKP.Domain.ValueObjects.Diagnostics
{
    public class ErrorApplicationContext
    {
        public string? ApplicationId { get; set; }

        public string? ApplicationVersion { get; set; }

        public string? Platform { get; set; }

        public string? MachineId { get; set; }

        public string? ClientInstanceId { get; set; }
    }
}
