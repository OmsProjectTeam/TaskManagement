using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class UserViweModal
    {
		[Required]
		public string fullName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNoumbe { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
      

        public string ReturnUrl { get; set; }

        public List<IdentityRole> Role { get; set; }



        public List<IdentityUser> useer { get; set; }
    }
}
