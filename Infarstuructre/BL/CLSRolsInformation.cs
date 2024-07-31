using Domin.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
    public interface IIRolsInformation
    {
        List<IdentityRole> GetAll();
        IdentityRole GetById(string Id);
    public class CLSRolsInformation: IIRolsInformation
    {
        RoleManager<IdentityRole> _roleManager;

        public CLSRolsInformation(RoleManager<IdentityRole> roleManager)
        {
            _roleManager=roleManager;
        }
        public List<IdentityRole> GetAll()
        {
            List<IdentityRole> MySlider = _roleManager.Roles.OrderBy(x => x.Name).ToList();
            return MySlider;
        }

        public IdentityRole GetById(string Id)
        {
            IdentityRole sslid = _roleManager.Roles.FirstOrDefault(a => a.Id == Id);
            return sslid;
        }


          

        }
}
}
