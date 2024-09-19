using Infarstuructre.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Threading.Tasks;

namespace Yara.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAPIController : ControllerBase
    {
        IITask iTask;
        ApiResponse response;
        IIUserInformation iUserInformation;
        IITaskStatus iTaskStatus;
        IIProjectInformation iProjectInformation;
        private readonly UserManager<ApplicationUser> _userManager;
        MasterDbcontext dbcontext;
        public TaskAPIController(IITask iTask1, IIUserInformation iUserInformation, UserManager<ApplicationUser> userManager,
            MasterDbcontext dbcontext, IITaskStatus iTaskStatus, IIProjectInformation iProjectInformation1)
        {
            iTask = iTask1;
            response = new ApiResponse();
            this.iUserInformation = iUserInformation;
            _userManager = userManager;
            this.dbcontext = dbcontext;
            this.iTaskStatus = iTaskStatus;
            iProjectInformation = iProjectInformation1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iTask.GetAllAsync();
                if(allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }

            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await iTask.GetByIdAsync(id);
                if (data == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = data;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(TBTask model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                var result = await iTask.AddDataAsync(model);
                if (result)
                {
                    await SendEmail(model);
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBTask model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                var result = await iTask.UpdateDataAsync(model);
                if(result)
                    await SendEmail(model);

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var item = GetById(id);
                if (item == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                await iTask.DeleteDataAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }


        private async Task SendEmail(TBTask model)
        {
            var userd = iUserInformation.GetById(model.UserId);
            var user = await _userManager.FindByIdAsync(model.UserId);
            string develovoer = user.Name;
            string email = user.Email;

            var emailSetting = await dbcontext.TBEmailAlartSettings
                           .OrderByDescending(n => n.IdEmailAlartSetting)
                           .Where(a => a.CurrentState == true && a.Active == true)
                           .FirstOrDefaultAsync();

            var Project = iProjectInformation.GetById(model.IdProjectInformation);
            string projektNameAr = Project.ProjectNameAr;

            var TAskStatus = iTaskStatus.GetById(model.IdTaskStatus);
            string taskstAr = TAskStatus.TaskStatusAr;



            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(model.TitleAr, emailSetting.MailSender));
            message.To.Add(new MailboxAddress(develovoer, email));
            message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
            message.Subject = "مهمة جديدة  " + "بواسطة :" + model.AddedBy;
            var builder = new BodyBuilder
            {
                TextBody = $"مهمة جديدة   \n\n\n" +
                           $"عناية السيد/ة: {develovoer} . المحترم/ة\n\n\n" +
                           $"تحية طيبة وبعد " +
                           $"أليك تفاصيل المهمة الجديد  :\n\n\n" +
                           $"الحالة   : {taskstAr}\n\n\n" +
                           $"المهمة  : {model.TitleAr}\n\n\n" +
                           $"الوصف : {model.DescriptionAr}\n\n\n" +
                           $"المشروع  : {projektNameAr}\n\n\n" +
                           $"تاريخ البداية : {model.StartDate}\n\n\n" +
                           $"تاريخ الانتهاء: {model.EndtDate}\n\n\n" +
                           $"مضافة بواسطة  : {model.AddedBy}\n\n\n"
            };

            message.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(emailSetting.SmtpServer, emailSetting.PortServer, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailSetting.MailSender, emailSetting.PasswordEmail);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
