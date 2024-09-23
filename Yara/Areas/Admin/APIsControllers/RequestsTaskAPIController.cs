using Infarstuructre.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Yara.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsTaskAPIController : ControllerBase
    {
        IIRequestsTask iRequestsTask;
        ApiResponse response;
        MasterDbcontext dbcontext;
        IIUserInformation iUserInformation;
        private readonly UserManager<ApplicationUser> _userManager;
        public RequestsTaskAPIController(IIRequestsTask iRequestsTask, MasterDbcontext dbcontext1, IIUserInformation iUserInformation, UserManager<ApplicationUser> userManager)
        {
            this.iRequestsTask = iRequestsTask;
            response = new ApiResponse();
            dbcontext = dbcontext1;
            this.iUserInformation = iUserInformation;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await iRequestsTask.GetAllAsync();
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

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await iRequestsTask.GetByIdAsync(id);
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

        [HttpGet("GetByViewId/{id}")]
        public async Task<IActionResult> GetByViewId(int id)
        {
            try
            {
                var data = await iRequestsTask.GetByIdviewAsync(id);
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
        public async Task<IActionResult> AddData([FromForm] TBRequestsTask model, List<IFormFile> Files)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;


                var file = HttpContext.Request.Form.Files;
                if (file.Count() > 0)
                {
                    string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                    file[0].CopyTo(fileStream);
                    model.Photo = Photo;
                    fileStream.Close();
                }
                var reqwest = await iRequestsTask.AddDataAsync(model);
                if (reqwest == true)
                {
                    var user = await _userManager.GetUserAsync(User);
                    var userId = user.Id;

                    if (user == null)
                    {
                        response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        return Ok(response);
                    }

                    string namedovlober = user.Name;
                    string email = user.Email;
                    var TAskStatus =  await iRequestsTask.GetByIdviewAsync(model.IdRequestsTask);

                    if (user == null)
                    {
                        response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        return Ok(response);
                    }

                    string ProjectName = TAskStatus.ProjectName;
                    string taskstEn = TAskStatus.TitleEn;
                    string DescriptionEn = TAskStatus.DescriptionEn;
                    string StartDate = TAskStatus.StartDate.ToString();
                    string EndtDate = TAskStatus.EndtDate.ToString();
                    //send email
                    var emailSetting = await dbcontext.TBEmailAlartSettings
                       .OrderByDescending(n => n.IdEmailAlartSetting)
                       .Where(a => a.CurrentState == true && a.Active == true)
                       .FirstOrDefaultAsync();
                    // التحقق من وجود إعدادات البريد الإلكتروني
                    if (emailSetting != null)
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress(model.RequestsTitelEn, emailSetting.MailSender));
                        message.To.Add(new MailboxAddress(namedovlober, email));
                        message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                        message.Subject = "New Request  " + "By:" + model.AddedBy;
                        var builder = new BodyBuilder
                        {
                            TextBody = $"New Request  \n\n\n" +
                                       $"Attn: Mr  {namedovlober}Greetings ...,\n\n\n" +

                                       $"Please pay careful attention to the following request and respond to it as soon as possible.. :\n\n\n" +
                                       $"The request is related to the project: {ProjectName}\n\n\n" +
                                       $"And related to the Task Name : {taskstEn}\n\n\n" +
                                       $"Which includes : {DescriptionEn}\n\n\n" +
                                       $"Start Date : {StartDate}\n\n\n" +
                                       $"End Date: {EndtDate}\n\n\n" +
                                       $"The Titel request: {model.RequestsTitelEn}\n\n\n" +
                                       $"Description request: {model.RequestsEn}\n\n\n" +
                                       $"Add by  : {model.AddedBy}\n\n\n"
                        };
                        // إضافة الصورة كملف مرفق إذا كانت موجودة
                        if (!string.IsNullOrEmpty(model.Photo))
                        {
                            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images/Home", model.Photo);
                            builder.Attachments.Add(imagePath);
                        }
                        message.Body = builder.ToMessageBody();
                        using (var client = new SmtpClient())
                        {
                            await client.ConnectAsync(emailSetting.SmtpServer, emailSetting.PortServer, SecureSocketOptions.StartTls);
                            await client.AuthenticateAsync(emailSetting.MailSender, emailSetting.PasswordEmail);
                            await client.SendAsync(message);
                            await client.DisconnectAsync(true);
                        }
                    }
                    else
                    {
                        // التعامل مع الحالة التي لا توجد فيها إعدادات البريد الإلكتروني
                        // يمكنك تسجيل خطأ أو تنفيذ إجراءات أخرى هنا
                    }

                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return Ok(response);
                }
                else
                {
                    if (file.Count() > 0)
                    {
                        var PhotoNAme = model.Photo;
                        await iRequestsTask.DELETPHOTOWethErrorAsync(PhotoNAme);

                        response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        return Ok(response);
                    }

                    else
                    {
                        response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }

            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateData(TBRequestsTask model, List<IFormFile> Files)
        {
            var file = HttpContext.Request.Form.Files;
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;


                if (file.Count() == 0) 
                {
                    model.Photo = model.Photo;

                    var reqest = await iRequestsTask.UpdateDataAsync(model);
                    if (reqest == true)
                    {
                        var user = await _userManager.GetUserAsync(User);
                        var userId = user.Id;

                        if (user == null)
                        {
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                            return Ok(response);
                        }



                        string namedovlober = user.Name;
                        string email = user.Email;

                        var TAskStatus = await iRequestsTask.GetByIdviewAsync(model.IdRequestsTask);

                        string ProjectName = TAskStatus.ProjectName;
                        string taskstEn = TAskStatus.TitleEn;
                        string DescriptionEn = TAskStatus.DescriptionEn;
                        string StartDate = TAskStatus.StartDate.ToString();
                        string EndtDate = TAskStatus.EndtDate.ToString();

                        var emailSetting = await dbcontext.TBEmailAlartSettings
                           .OrderByDescending(n => n.IdEmailAlartSetting)
                           .Where(a => a.CurrentState == true && a.Active == true)
                           .FirstOrDefaultAsync();

                        if (emailSetting != null)
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(model.RequestsTitelEn, emailSetting.MailSender));
                            message.To.Add(new MailboxAddress(namedovlober, email));
                            message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "Update  Request  " + "By:" + model.AddedBy;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"Update Request  \n\n\n" +
                                           $"Attn: Mr  {namedovlober}Greetings ...,\n\n\n" +

                                           $"Please be careful attention to the following request and respond to it as soon as possible.. :\n\n\n" +
                                           $"The request is related to the project: {ProjectName}\n\n\n" +
                                           $"And related to the Task Name : {taskstEn}\n\n\n" +
                                           $"Which includes : {DescriptionEn}\n\n\n" +
                                           $"Start Date : {StartDate}\n\n\n" +
                                           $"End Date: {EndtDate}\n\n\n" +
                                           $"The Titel request: {model.RequestsTitelEn}\n\n\n" +
                                           $"Description request: {model.RequestsEn}\n\n\n" +
                                           $"Add by  : {model.AddedBy}\n\n\n"
                            };
                            // إضافة الصورة كملف مرفق إذا كانت موجودة
                            if (!string.IsNullOrEmpty(model.Photo))
                            {
                                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", model.Photo);
                                builder.Attachments.Add(imagePath);
                            }
                            message.Body = builder.ToMessageBody();
                            using (var client = new SmtpClient())
                            {
                                await client.ConnectAsync(emailSetting.SmtpServer, emailSetting.PortServer, SecureSocketOptions.StartTls);
                                await client.AuthenticateAsync(emailSetting.MailSender, emailSetting.PasswordEmail);
                                await client.SendAsync(message);
                                await client.DisconnectAsync(true);
                            }
                        }
                        else
                        {
                            // التعامل مع الحالة التي لا توجد فيها إعدادات البريد الإلكتروني
                            // يمكنك تسجيل خطأ أو تنفيذ إجراءات أخرى هنا
                        }

                        if (user == null)
                        {
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                            return Ok(response);
                        }
                    }
                    else
                    {
                        var PhotoNAme = model.Photo;
                        {
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                            return Ok(response);
                        }
                    }
                }
                else
                {  
                    string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                    file[0].CopyTo(fileStream);
                    model.Photo = Photo;
                    fileStream.Close();
                    var reqweistDeletPoto = await iRequestsTask.DELETPHOTOAsync(model.IdRequestsTask);
                    var reqestUpdate2 = iRequestsTask.UpdateData(model);
                    if (reqestUpdate2 == true)
                    {
                        ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
                        var userd = vmodel.sUser = iUserInformation.GetById(model.UserId);
                        var user = await _userManager.FindByIdAsync(model.UserId);
                        //var user = await _userManager.GetUserAsync(User);
                        if (user == null)
                        {
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                            return Ok(response);
                        }

                        string namedovlober = user.Name;
                        string email = user.Email;

                        var TAskStatus = vmodel.ViewRequestsTask = iRequestsTask.GetByIdview(model.IdRequestsTask);

                        string ProjectName = TAskStatus.ProjectName;
                        string taskstEn = TAskStatus.TitleEn;
                        string DescriptionEn = TAskStatus.DescriptionEn;
                        string StartDate = TAskStatus.StartDate.ToString();
                        string EndtDate = TAskStatus.EndtDate.ToString();
                        //send email
                        var emailSetting = await dbcontext.TBEmailAlartSettings
                           .OrderByDescending(n => n.IdEmailAlartSetting)
                           .Where(a => a.CurrentState == true && a.Active == true)
                           .FirstOrDefaultAsync();
                        // التحقق من وجود إعدادات البريد الإلكتروني
                        if (emailSetting != null)
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(model.RequestsTitelEn, emailSetting.MailSender));
                            message.To.Add(new MailboxAddress(namedovlober, email));
                            message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "Update  Request  " + "By:" + model.AddedBy;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"Update Request  \n\n\n" +
                                           $"Attn: Mr  {namedovlober}Greetings ...,\n\n\n" +
                                           $"Please pay careful attention to the following request and respond to it as soon as possible.. :\n\n\n" +
                                           $"The request is related to the project: {ProjectName}\n\n\n" +
                                           $"And related to the Task Name : {taskstEn}\n\n\n" +
                                           $"Which includes : {DescriptionEn}\n\n\n" +
                                           $"Start Date : {StartDate}\n\n\n" +
                                           $"End Date: {EndtDate}\n\n\n" +
                                           $"The Titel request: {model.RequestsTitelEn}\n\n\n" +
                                           $"Description request: {model.RequestsEn}\n\n\n" +
                                           $"Add by  : {model.AddedBy}\n\n\n"
                            };
                            // إضافة الصورة كملف مرفق إذا كانت موجودة
                            if (!string.IsNullOrEmpty(model.Photo))
                            {
                                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", model.Photo);
                                builder.Attachments.Add(imagePath);
                            }
                            message.Body = builder.ToMessageBody();
                            using (var client = new SmtpClient())
                            {
                                await client.ConnectAsync(emailSetting.SmtpServer, emailSetting.PortServer, SecureSocketOptions.StartTls);
                                await client.AuthenticateAsync(emailSetting.MailSender, emailSetting.PasswordEmail);
                                await client.SendAsync(message);
                                await client.DisconnectAsync(true);
                            }
                        }
                        else
                        {
                            // التعامل مع الحالة التي لا توجد فيها إعدادات البريد الإلكتروني
                            // يمكنك تسجيل خطأ أو تنفيذ إجراءات أخرى هنا
                        }

                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                            return Ok(response);
                        
                    }
                    else
                    {
                        var PhotoNAme = model.Photo;
                        var delet = await iRequestsTask.DELETPHOTOWethErrorAsync(PhotoNAme);
                        return RedirectToAction("AddEditRequestsTaskImage");
                    }
                }

                await iRequestsTask.UpdateDataAsync(model);
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

                await iRequestsTask.DeleteDataAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }
    }
}
