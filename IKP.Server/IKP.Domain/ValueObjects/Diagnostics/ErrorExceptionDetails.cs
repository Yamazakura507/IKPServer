namespace IKP.Domain.ValueObjects.Diagnostics
{
    public class ErrorExceptionDetails
    {
        public string? ExceptionType { get; set; }

        public string? Message { get; set; }

        public string? StackTrace { get; set; }

        public string? SourceFile { get; set; }

        public string? SourceMethod { get; set; }

        public int? SourceLineNumber { get; set; }
    }
}
