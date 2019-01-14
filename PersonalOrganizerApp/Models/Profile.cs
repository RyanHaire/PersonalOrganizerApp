using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PersonalOrganizerApp.Models
{
    public class Profile
    {

        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required."), Phone, Display(Name = "Phone Number")]
        public long PhoneNumber { get; set; }
    }
}
