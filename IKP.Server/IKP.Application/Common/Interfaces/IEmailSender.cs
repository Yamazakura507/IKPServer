namespace IKP.Application.Common.Interfaces;

public interface IEmailSender
{
    ValueTask SendAsync(string recipient, string subject, string body, bool isHtml = false, CancellationToken cancellationToken = default);
}
