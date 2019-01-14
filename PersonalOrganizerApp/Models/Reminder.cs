using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PersonalOrganizerApp.Models;

namespace PersonalOrganizerApp.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        // date of reminder 
        [Required(ErrorMessage = "Date is required."), Display(Name = "Date & Time")]
        public DateTime DateTime { get; set; }

        public string Description { get; set; }
    }

    // db context is the dbcontext for the app (all the models)
    public class ReminderContext: DbContext
    {
        public ReminderContext(DbContextOptions<ReminderContext> options) : base(options)
        { }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
    }
}
