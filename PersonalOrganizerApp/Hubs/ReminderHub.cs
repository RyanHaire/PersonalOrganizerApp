using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PersonalOrganizerApp.Hubs
{
    public class ReminderHub : Hub
    {
        public async Task SendReminder(string reminder)
        {
            await Clients.All.SendAsync("ReceiveReminder", reminder);
        }
    }
}
