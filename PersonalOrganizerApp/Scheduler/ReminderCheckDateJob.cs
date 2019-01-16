using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
using PersonalOrganizerApp.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PersonalOrganizerApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace PersonalOrganizerApp.Scheduler
{
    public class ReminderCheckDateJob : IJob
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ReminderCheckDateJob> logger;
        private readonly IHubContext<ReminderHub> _reminderHub;

        public ReminderCheckDateJob(IConfiguration configuration, ILogger<ReminderCheckDateJob> logger, IHubContext<ReminderHub> hub)
        {
            this.logger = logger;
            this.configuration = configuration;
            _reminderHub = hub;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            DateTime currentTime;
            try
            {
                logger.LogInformation("Start of Execute");
                using (ReminderContextFactory reminderContextFactory = new ReminderContextFactory())
                {
                    // get all reminders from ReminderContext
                    var reminders = await reminderContextFactory.GetAllReminders();

                    // loop through reminder list
                    foreach (var rem in reminders)
                    {
                        currentTime = DateTime.Now;
                       
                        /* DateTime.ToString("g")
                         * returns string format mm/dd/yyyy hh:mm AM/PM */

                        // check if time of reminder is now
                        if (currentTime.ToString("g").Equals(rem.DateTime.ToString("g")) && !rem.ReminderSent)
                        {
                            // set reminder boolean to true since its going to be sent to client
                            rem.ReminderSent = true;

                            //update reminder in context
                            await reminderContextFactory.UpdateReminders(rem);

                            // send reminder to client with SignalR realtime api hub
                            await _reminderHub.Clients.All.SendAsync("ReceiveReminder", rem.Description);
                        }
                        else
                        {
                            logger.LogInformation("Reminder isnt ready.");
                        }
                    
                    } // end of foreach loop
                } // end of using statement
            }
            catch (Exception ex)
            {
                logger.LogError("Error happened here");
                logger.LogError(ex.ToString());
            }

            await Task.CompletedTask;
        }
    }
}
