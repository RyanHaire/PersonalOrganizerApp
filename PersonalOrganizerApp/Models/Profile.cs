using System;
using Microsoft.EntityFrameworkCore;
namespace PersonalOrganizerApp.Models
{
    public class Profile
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
    }
}
