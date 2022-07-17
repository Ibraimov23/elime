using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElimeProject.Models
{
    public class User : IdentityUser
    {
        public DateTime? Registered_At { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public long Inn { get; set; }
        [Required]
        public long Ogrn { get; set; }
        public User()
        {
            this.Registered_At = DateTime.Now;
        }
    }
}
