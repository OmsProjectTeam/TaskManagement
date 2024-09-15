using Domin.Entity;
using Infarstuructre.Data;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.BL
{
	public interface IIUserInformation
	{
		List<VwUser> GetAll();
		ApplicationUser GetById(string? Id);
		List<ApplicationUser> GetAllByName(string name);
		List<VwUser> GetAllbyId(string userId);
		List<ApplicationUser> GetAllByNameall();
        List<ApplicationUser> GetAllByRole(string roles);

        // ////////////////////////////////////API//////////////////////////////////

        Task<List<VwUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string? Id);
        Task<List<ApplicationUser>> GetAllByNameAsync(string name);
        Task<List<VwUser>> GetAllbyIdAsync(string userId);
        Task<List<ApplicationUser>> GetAllByNameallAsync();



    }
	public class CLSUserInformation: IIUserInformation
	{
		UserManager<ApplicationUser> _userManager;
		MasterDbcontext dbcontext;

		public CLSUserInformation(UserManager<ApplicationUser> userManager,MasterDbcontext dbcontext1)
        {
			_userManager=userManager;

		}
		public List<VwUser> GetAll()

		{
			//Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
			//List<VwUser> MySlider = dbcontext.VwUsers.OrderBy(x => x.Role).ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
			List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(n => n.ActiveUser == true).ToList();
			return MySlider;
		}

        public Task<List<VwUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public List<VwUser> GetAllbyId(string userId)

		{
			//Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
			List<VwUser> MySlider = dbcontext.VwUsers.Where(x => x.Id== userId).Where(n => n.ActiveUser == true).ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
																					 //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
			return MySlider;
		}

        public ApplicationUser GetById(string? Id)
        {
            ApplicationUser sslid = _userManager.Users.FirstOrDefault(a => a.Id == Id);
            return sslid;
        }


        public List<ApplicationUser> GetAllByName(string name)

		{
			//Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
			List<ApplicationUser> MySlider = _userManager.Users.Where(x => x.Email == name).Where(n => n.ActiveUser == true).ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
																							 //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
			return MySlider;
		}
        public List<ApplicationUser> GetAllByNameall()

        {
            //Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
            List<ApplicationUser> MySlider = _userManager.Users.OrderByDescending(x => x.Id).Where(n=>n.ActiveUser==true). ToList(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                      //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }
        public List<ApplicationUser> GetAllByRole(string roles)
        {
            // جلب جميع المستخدمين النشطين
            List<ApplicationUser> allActiveUsers = GetAllByNameall2();

            // تقسيم الأدوار إذا كانت مفصولة بفاصلة
            var roleList = roles.Split(',').Select(r => r.Trim()).ToList();

            // تصفية المستخدمين بناءً على الأدوار المطلوبة
            var usersWithRoles = allActiveUsers
                .Where(user => roleList.Any(role => UserHasRole(user, role)))
                .ToList();

            return usersWithRoles;
        }
        // دالة تستخدم للتحقق مما إذا كان للمستخدم صلاحية معينة
        private bool UserHasRole(ApplicationUser user, string role)
        {
            // تحقق من الصلاحية - هذا يعتمد على كيفية تخزين الصلاحيات
            // افترض أن هناك طريقة في _userManager للتحقق من الصلاحيات
            return _userManager.IsInRoleAsync(user, role).Result;
        }

        // الدالة الأصلية لجلب جميع المستخدمين النشطين
        public List<ApplicationUser> GetAllByNameall2()
        {
            List<ApplicationUser> MySlider = _userManager.Users
                .OrderByDescending(x => x.Id)
                .Where(n => n.ActiveUser == true)
                .ToList();

            return MySlider;
        }
        // ///////////////////////////////////APIs///////////////////////////////////////////////
        public async Task<List<ApplicationUser>> GetAllByNameallAsync()
        {
            //Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
            List<ApplicationUser> MySlider = await _userManager.Users.OrderByDescending(x => x.Id).Where(n => n.ActiveUser == true).ToListAsync(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                                                        //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }

        public async Task<List<ApplicationUser>> GetAllByNameAsync(string name)
        {
            List<ApplicationUser> MySlider = await _userManager.Users.Where(x => x.Email == name).Where(n => n.ActiveUser == true).ToListAsync(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                                                       //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }



        public async Task<ApplicationUser> GetByIdAsync(string? Id)
        {
            ApplicationUser sslid = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == Id);
            return sslid;
        }


        public async Task<List<VwUser>> GetAllbyIdAsync(string userId)
        {
            List<VwUser> MySlider = await dbcontext.VwUsers.Where(x => x.Id == userId).Where(n => n.ActiveUser == true).ToListAsync(); //_userManager.Users.OrderBy(x=>x.Name).ToList()
                                                                                                                            //List<VwUser> MySlider = dbcontext.VwUsers.OrderByDescending(n => n.Id).Where(a => a.ActiveUser == true).ToList();
            return MySlider;
        }
    }
}
