using IKP.Domain.ValueObjects.Diagnostics;

namespace IKP.Application.Diagnostics.Exceptions.Interfaces
{
    /// <summary>
    /// Extracts structured diagnostic information from exceptions.
    /// </summary>
    public interface IExceptionDetailsExtractor
    {
        /// <summary>
        /// Extracts diagnostic details from the specified exception.
        /// </summary>
        /// <param name="exception">Exception to analyze.</param>
        /// <returns>Structured exception details.</returns>
        ErrorExceptionDetails Extract(Exception exception);
    }
}
