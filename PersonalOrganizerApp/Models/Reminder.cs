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

        [Display(Name = "Date & Time")]
        public DateTime DateTime { get; set; }
        public string Description { get; set; }

       
    }

    public class ReminderContext: DbContext
    {
        public ReminderContext(DbContextOptions<ReminderContext> options) : base(options)
        {

        }

        public DbSet<Reminder> Reminders { get; set; }
    }
}
