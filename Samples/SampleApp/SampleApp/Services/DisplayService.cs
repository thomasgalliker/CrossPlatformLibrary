using System;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public class DisplayService : IDisplayService
    {
        private readonly Func<string, string, Task> alertAction;

        public DisplayService(Func<string, string, Task> alertAction)
        {
            this.alertAction = alertAction;
        }
        public async Task DisplayAlert(string title, string message)
        {
            await this.alertAction(title, message);
        }
    }
}