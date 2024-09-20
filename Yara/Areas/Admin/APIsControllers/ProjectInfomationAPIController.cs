using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectInfomationAPIController : ControllerBase
    {
        IIProjectInformation iProjectInformation;
        ApiResponse ApiResponse;
        MasterDbcontext dbcontext;
        public ProjectInfomationAPIController(IIProjectInformation iProjectInformation, MasterDbcontext dbcontext)
        {

            ApiResponse = new ApiResponse();
            this.iProjectInformation = iProjectInformation;
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await iProjectInformation.GetAllAsync();
                if (data == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                ApiResponse.Result = data;
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await iProjectInformation.GetByIdAsync(id);
                if (data == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                ApiResponse.Result = data;
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(TBProjectInformation model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                var result = await iProjectInformation.AddDataAsync(model);
                if (result)
                    await SendEmail(model);

                return Ok(ApiResponse);

            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBProjectInformation model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                var result = await iProjectInformation.UpdateDataAsync(model);
                if (result)
                    await SendEmail(model);

                return Ok(ApiResponse);

            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var item = GetById(id);
                if (item == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                await iProjectInformation.DeleteDataAsync(id);
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        private async Task SendEmail(TBProjectInformation model)
        {
            var emailSetting = await dbcontext.TBEmailAlartSettings
                                    .OrderByDescending(n => n.IdEmailAlartSetting)
                                    .Where(a => a.CurrentState == true && a.Active == true)
                                    .FirstOrDefaultAsync();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(model.ProjectName, emailSetting.MailSender));

            message.To.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
            message.Subject = "New Project  " + "By:" + model.DataEntry;
            var builder = new BodyBuilder
            {
                TextBody = $"New Project  \n\n\n" +
                           $"Attn: Mr  saif aldin\n\n\n" +
                           $"Greetings" +
                           $"A new project has been created entitled :\n\n\n" +
                           $"Titel : {model.ProjectName}\n\n\n" +
                           $"Description : {model.ProjectDescription}\n\n\n" +
                           $"Start Date : {model.ProjectStart}\n\n\n" +
                           $"End Date: {model.ProjectEnd}\n\n\n" +
                           $"Add by  : {model.DataEntry}\n\n\n"
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
