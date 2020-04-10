using System.Threading.Tasks;

namespace ModuleApp.Module.Core.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);
    }
}