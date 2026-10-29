using System.Net;
using System.Net.Mail;
using IKP.Application.Common.Email;
using IKP.Application.Common.Interfaces;
using Microsoft.Extensions.Options;

namespace IKP.Infrastructure.Services.Email;

public sealed class SmtpEmailSender : IEmailSender
{
    private readonly EmailOptions options;

    public SmtpEmailSender(IOptions<EmailOptions> options) => this.options = options.Value;

    public async ValueTask SendAsync(string recipient, string subject, string body, bool isHtml = false, CancellationToken cancellationToken = default)
    {
        if (!options.Enabled) throw new InvalidOperationException("Email sending is disabled.");
        if (string.IsNullOrWhiteSpace(options.Host)) throw new InvalidOperationException("Email:Host is not configured.");
        if (string.IsNullOrWhiteSpace(options.FromAddress)) throw new InvalidOperationException("Email:FromAddress is not configured.");

        using MailMessage message = new ()
        {
            From = new MailAddress(options.FromAddress, options.FromDisplayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        message.To.Add(recipient);

        using SmtpClient client = new (options.Host, options.Port)
        {
            EnableSsl = options.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        if (!string.IsNullOrWhiteSpace(options.UserName))
        {
            client.Credentials = new NetworkCredential(options.UserName, options.Password);
        }
        else
        {
            client.UseDefaultCredentials = false;
        }

        cancellationToken.ThrowIfCancellationRequested();
        await client.SendMailAsync(message, cancellationToken);
    }
}
