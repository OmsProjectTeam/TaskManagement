using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class UserChingRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public string SelectedRoleId { get; set; }
    }
}
