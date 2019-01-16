using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonalOrganizerApp.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        // date of reminder 
        [Required(ErrorMessage = "Date is required."), Display(Name = "Date & Time")]
        public DateTime DateTime { get; set; }

        // if reminder has been sent already
        public bool ReminderSent { get; set; }

        // reminder description string
        public string Description { get; set; }
    }

    /* db context is the database for the app (all the models) */
    public class ReminderContext : DbContext
    {

        public ReminderContext(DbContextOptions<ReminderContext> options) : base(options)
        { }

        public DbSet<Reminder> Reminders { get; set; }
    }

    /* made for global use of remindercontext */
    public class ReminderContextFactory : IDisposable
    {
        private readonly ReminderContext Context;

        // creates a local instance of ReminderContext
        public ReminderContextFactory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReminderContext>();
            optionsBuilder.UseSqlite("Data Source=organizerapp.db");
            Context = new ReminderContext(optionsBuilder.Options);
        }

        // implemented by IDisposable for Context
        public void Dispose()
        {
            Context.Dispose();
        }

        // return List of reminders from context
        public Task<List<Reminder>> GetAllReminders()
        {
            var reminders = Context.Reminders.ToListAsync();
            return reminders;
        }

        // update reminder context with given updated reminder
        public async Task UpdateReminders(Reminder reminder)
        {
            try {
                // update context
                Context.Update(reminder);
                // save context
                await Context.SaveChangesAsync();
            }
            catch(DbUpdateException due) 
            {
                await Console.Out.WriteLineAsync(due.Message);
            }
        }

    }

}
