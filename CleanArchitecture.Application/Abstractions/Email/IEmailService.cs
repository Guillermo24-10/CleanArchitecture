using Correo = CleanArchitecture.Domain.Users.Email;

namespace CleanArchitecture.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendAsync(Correo recipient, string subject, string body);
    }
}
