using IKP.Application.Diagnostics.Exceptions.Interfaces;
using IKP.Domain.ValueObjects.Diagnostics;
using System.Diagnostics;
using System.Reflection;

namespace IKP.Application.Diagnostics.Exceptions
{
    /// <summary>
    /// Extracts structured diagnostic information from exceptions.
    /// </summary>
    public class ExceptionDetailsExtractor : IExceptionDetailsExtractor
    {
        /// <inheritdoc />
        public ErrorExceptionDetails Extract(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            Exception rootException = GetRootException(exception);
            (string? sourceFile, string? sourceMethod, int? sourceLineNumber) = ExtractSource(rootException);

            return new ErrorExceptionDetails
            {
                ExceptionType = rootException.GetType().FullName ?? rootException.GetType().Name,
                Message = rootException.Message,
                StackTrace = exception.StackTrace ?? rootException.StackTrace,
                SourceFile = sourceFile,
                SourceMethod = sourceMethod,
                SourceLineNumber = sourceLineNumber
            };
        }

        /// <summary>
        /// Gets the deepest inner exception.
        /// </summary>
        /// <param name="exception">Exception.</param>
        /// <returns>The deepest inner exception.</returns>
        private static Exception GetRootException(Exception exception)
        {
            Exception current = exception;

            while (current.InnerException is not null)
            {
                current = current.InnerException;
            }

            return current;
        }

        private static (string? SourceFile, string? SourceMethod, int? SourceLineNumber) ExtractSource(Exception exception)
        {
            StackTrace stackTrace = new(exception, true);
            StackFrame[] frames = stackTrace.GetFrames() ?? [];

            foreach (StackFrame frame in frames)
            {
                MethodBase? method = frame.GetMethod();
                string? sourceFile = frame.GetFileName();
                int sourceLineNumber = frame.GetFileLineNumber();

                string? sourceMethod = method is null ? null : $"{method.DeclaringType?.FullName ?? method.ReflectedType?.FullName}.{method.Name}";

                if (!string.IsNullOrWhiteSpace(sourceFile) || !string.IsNullOrWhiteSpace(sourceMethod))
                {
                    return (sourceFile, sourceMethod, sourceLineNumber > 0 ? sourceLineNumber : null);
                }
            }

            return (null, null, null);
        }
    }
}
