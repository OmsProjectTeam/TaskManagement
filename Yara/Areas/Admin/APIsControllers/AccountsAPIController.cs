using Domin.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yara.Areas.Admin.Controllers;

namespace Yara.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsAPIController : ControllerBase
    {
        private readonly AccountsController _accountsController;

        public AccountsAPIController(AccountsController accountsController)
        {
            _accountsController = accountsController;
        }

        [HttpPost("AddRolesAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRolesAsync([FromBody] IdentityRole model)
        {
            ViewmMODeElMASTER viewm = new ViewmMODeElMASTER();

            viewm.sIdentityRole = model;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.Roles(viewm);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRoleAsync/{roleName}")]
        public async Task<IActionResult> DeleteRoleAsync(string roleName)
        {
            var result = await _accountsController.DeleteRole(roleName);
            return Ok(result);
        }

        [HttpPost("RegisterAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAsync([FromBody] NewRegister model)
        {
            RegisterViewModel rModel = new RegisterViewModel();
            rModel.NewRegister = model;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.Registers(rModel);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUserAsync/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var result = await _accountsController.DeleteUser(userId);
            return Ok(result);
        }

        [HttpPost("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordViewModel? model)
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.ChangePassword = model;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.ChangePassword1(new ViewmMODeElMASTER(), registerViewModel);
            return Ok(result);
        }

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.LoginAsync(model, returnUrl);
            return Ok(result);
        }

        [HttpPost("LogoutAsync")]
        public async Task<IActionResult> LogoutAsync()
        {
            var result = await _accountsController.Logout1();
            return Ok(result);
        }

        [HttpPost("RegistersAsync")]
        public async Task<IActionResult> RegistersAsync([FromBody] NewRegister model)
        {
            RegisterViewModel rModel = new RegisterViewModel();
            rModel.NewRegister = model;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.Registers1(rModel);
            return Ok(result);
        }

        [HttpPost("RegistersCustomerAsync")]
        public async Task<IActionResult> RegistersCustomerAsync([FromBody] NewRegister model)
        {
            RegisterViewModel rModel = new RegisterViewModel();
            rModel.NewRegister = model;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.RegistersCustomer(rModel);
            return Ok(result);
        }

        [HttpPost("RegistersMerchantAsync")]
        public async Task<IActionResult> RegistersMerchantAsync([FromBody] NewRegister model)
        {
            RegisterViewModel rModel = new RegisterViewModel();
            rModel.NewRegister = model;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.RegistersMerchant(rModel);
            return Ok(result);
        }

        [HttpPost("RegistersEdite/{Id}")]
        public async Task<IActionResult> RegistersEdite([FromBody] ApplicationUser model, [FromHeader] List<IFormFile> Files, string returnUrl, string? Id)
        {
            ViewmMODeElMASTER viewm = new ViewmMODeElMASTER();
            viewm.sUser = model;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.RegistersEdite(viewm, Files, returnUrl, Id);
            return Ok(result);
        }

        [HttpPost("AddEditRolesUserAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEditRolesUserAsync([FromBody] List<SelectListItem> model)
        {
            ViewmMODeElMASTER viewm = new ViewmMODeElMASTER();
            viewm.Roles1 = model;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.AddEditRolesUser(viewm);
            return Ok(result);
        }
    }
}
