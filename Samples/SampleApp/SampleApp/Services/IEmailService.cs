using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface IEmailService
    {
        Task SendEmail(string subject, string body, List<string> recipients);
    }
}