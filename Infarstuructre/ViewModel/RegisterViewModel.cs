using Domin.Entity;
using Domin.Resource;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
   public class RegisterViewModel
    {
        public List<VwUser> Users { get; set; }
        public NewRegister NewRegister { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
        public ApplicationUser sUser { get; set; }
        public ViewmMODeElMASTER ListIdentityRole { get; set; }
    }
    public class NewRegister
    {
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterName")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength100")]
        [MinLength(3, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RoleName")]
        public string RoleName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterEmailError")]
        public string Email { get; set; }
        public string? ImageUser { get; set; }
        public bool ActiveUser { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "Password")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength20")]
        [MinLength(5, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLengthPassword5")]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "ComparePassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "ComparePasswordcomferm")]
        public string ComparePassword { get; set; }
		[Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "userName")]
		[MaxLength(20, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength20")]
		[MinLength(3, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string userName { get; set; }	
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "PhoneNumber")]
		[MaxLength(20, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength20")]
		[MinLength(3, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string PhoneNumber { get; set; }

		public returnUrl? returnUrl { get; set; }
        public bool Rememberme { get; set; }





	}
}
