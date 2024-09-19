
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TaskController : Controller
    {
        MasterDbcontext dbcontext;
        IITask iTask;
        IITaskStatus iTaskStatus;
        IIProjectInformation iProjectInformation;
        IIUserInformation iUserInformation;
        IITypesOfTask iTypesOfTask;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(MasterDbcontext dbcontext1, IITask iTask1,IITaskStatus iTaskStatus1, IIProjectInformation iProjectInformation1, IIUserInformation iUserInformation1,IITypesOfTask iTypesOfTask1, UserManager<ApplicationUser> userManager)
        {
            dbcontext= dbcontext1;
            iTask = iTask1;
            iTaskStatus = iTaskStatus1;
            iProjectInformation = iProjectInformation1;
            iTypesOfTask = iTypesOfTask1;
            iUserInformation = iUserInformation1;
            _userManager = userManager;
        }
        public IActionResult MyTask()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewTask = iTask.GetAll();
            return View(vmodel);
        }
        public IActionResult MyTaskAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewTask = iTask.GetAll();
            return View(vmodel);
        }
        public IActionResult AddTask(int? IdTask)
        {
            ViewBag.USer = iUserInformation.GetAllByRole("Developer,Admin");
            ViewBag.TaskStatus = iTaskStatus.GetAll();
            ViewBag.ProjectInformation = iProjectInformation.GetAll();
            ViewBag.TypesOfTask = iTypesOfTask.GetAll();
        
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewTask = iTask.GetAll();
            if (IdTask != null)
            {
                vmodel.Task = iTask.GetById(Convert.ToInt32(IdTask));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddTaskAr(int? IdTask)
        {
            ViewBag.USer = iUserInformation.GetAllByRole("Developer,Admin");
            ViewBag.TaskStatus = iTaskStatus.GetAll();
            ViewBag.ProjectInformation = iProjectInformation.GetAll();
            ViewBag.TypesOfTask = iTypesOfTask.GetAll();

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewTask = iTask.GetAll();
            if (IdTask != null)
            {
                vmodel.Task = iTask.GetById(Convert.ToInt32(IdTask));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBTask slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTask = model.Task.IdTask;
                slider.IdTaskStatus = model.Task.IdTaskStatus;
                slider.IdProjectInformation = model.Task.IdProjectInformation;
                slider.IdTypesOfTask = model.Task.IdTypesOfTask;
           
                slider.TitleAr = model.Task.TitleAr;
                slider.TitleEn = model.Task.TitleEn;
                slider.DescriptionAr = model.Task.DescriptionAr;
                slider.DescriptionEn = model.Task.DescriptionEn;
                slider.StartDate = model.Task.StartDate;
                slider.EndtDate = model.Task.EndtDate;
                slider.UserId = model.Task.UserId;
                slider.AddedBy = model.Task.AddedBy;                        
                slider.DataEntry = model.Task.DataEntry;
                slider.DateTimeEntry = model.Task.DateTimeEntry;
                slider.CurrentState = model.Task.CurrentState;


                ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

                var userd = vmodel.sUser = iUserInformation.GetById(slider.UserId);

                var user = await _userManager.FindByIdAsync(slider.UserId);
                //var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound();

                string namedovlober = user.Name;
                string email = user.Email;




                var Project = vmodel.ProjectInformation = iProjectInformation.GetById(slider.IdProjectInformation);

                //var ProjectInfo = await iProjectInformation.FindByIdAsync(slider.IdProjectInformation);
                //var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound();

                string projektNameEn= Project.ProjectName;
                string projektNameAr= Project.ProjectNameAr;


                var TAskStatus = vmodel.TaskStatus = iTaskStatus.GetById(slider.IdTaskStatus);

                //var ProjectInfo = await iProjectInformation.FindByIdAsync(slider.IdProjectInformation);
                //var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound();

                string taskstEn = TAskStatus.TaskStatus;
                string taskstAr = TAskStatus.TaskStatusAr;
           


                if (slider.IdTask == 0 || slider.IdTask == null)
                {      
                    



                    var reqwest = iTask.saveData(slider);
                    if (reqwest == true)
                    {

                        //send email
                        var emailSetting = await dbcontext.TBEmailAlartSettings
                           .OrderByDescending(n => n.IdEmailAlartSetting)
                           .Where(a => a.CurrentState == true && a.Active == true)
                           .FirstOrDefaultAsync();

                        // التحقق من وجود إعدادات البريد الإلكتروني
                        if (emailSetting != null)
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress("New new message", emailSetting.MailSender));
                            message.To.Add(new MailboxAddress(namedovlober, email));
                            message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "New Task  " +"By:"+ slider.AddedBy;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"مهمة جديدة  \n\n\n" +

                                           $"Attn: Mr  {namedovlober}\n\n\n" +
                                           $"Greetings" +
                                           $"Here is the new mission and its details :\n\n\n" +
                                           $"The Task : {taskstEn}\n\n\n" +
                                           $"Titel : {slider.TitleEn}\n\n\n" +
                                           $"Description : {slider.DescriptionEn}\n\n\n" +
                                           $"Project  : {projektNameEn}\n\n\n" +
                                           $"Start Date : {slider.StartDate}\n\n\n" +
                                           $"End Date: {slider.EndtDate}\n\n\n" +
                                           $"Add by  : {slider.AddedBy}\n\n\n"


                            };

                            //// إضافة الصورة كملف مرفق إذا كانت موجودة
                            //if (!string.IsNullOrEmpty(slider.Photo))
                            //{
                            //    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
                            //    builder.Attachments.Add(imagePath);
                            //}

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

                    








                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyTask");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddTask");
                    }
                }
                else
                {
                    var reqestUpdate = iTask.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTask");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddTask");

                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddTask");

            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBTask slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTask = model.Task.IdTask;
                slider.IdTaskStatus = model.Task.IdTaskStatus;
                slider.IdProjectInformation = model.Task.IdProjectInformation;
                slider.IdTypesOfTask = model.Task.IdTypesOfTask;
                slider.TitleAr = model.Task.TitleAr;
                slider.TitleEn = model.Task.TitleEn;
                slider.DescriptionAr = model.Task.DescriptionAr;
                slider.DescriptionEn = model.Task.DescriptionEn;
                slider.StartDate = model.Task.StartDate;
                slider.EndtDate = model.Task.EndtDate;
                slider.UserId = model.Task.UserId;
                slider.AddedBy = model.Task.AddedBy;
                slider.DataEntry = model.Task.DataEntry;
                slider.DateTimeEntry = model.Task.DateTimeEntry;
                slider.CurrentState = model.Task.CurrentState;
                if (slider.IdTask == 0 || slider.IdTask == null)
                {
              
                    var reqwest = iTask.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWebAr.VLSavedSuccessfully;
                        return RedirectToAction("MyTaskAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                        return RedirectToAction("AddTaskAr");
                    }
                }
                else
                {
                    var reqestUpdate = iTask.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWebAr.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTaskAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWebAr.VLErrorUpdate;
                        return RedirectToAction("AddTaskAr");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                return RedirectToAction("AddTaskAr");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdTask)
        {
            var reqwistDelete = iTask.deleteData(IdTask);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyTask");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyTask");

            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDataAr(int IdTask)
        {
            var reqwistDelete = iTask.deleteData(IdTask);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWebAr.VLdELETESuccessfully;
                return RedirectToAction("MyTaskAr");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWebAr.VLErrorDeleteData;
                return RedirectToAction("MyTaskAr");
            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 
        }
    }
}
