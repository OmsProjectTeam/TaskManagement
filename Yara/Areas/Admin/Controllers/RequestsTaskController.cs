

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RequestsTaskController : Controller
    {
        IITask iTask;
        IIUserInformation iUserInformation;
        private readonly UserManager<ApplicationUser> _userManager;
        IIRequestsTask iRequestsTask;
        MasterDbcontext dbcontext;
        IIProjectInformation iProjectInformation;

        public RequestsTaskController(IITask iTask1, IIRequestsTask iRequestsTask1, IIUserInformation iUserInformation1, UserManager<ApplicationUser> userManager, MasterDbcontext dbcontext1, IIProjectInformation iProjectInformation1)
        {
            iTask = iTask1;
            iUserInformation = iUserInformation1;
            _userManager = userManager;
            iRequestsTask = iRequestsTask1;
            dbcontext = dbcontext1;
            iProjectInformation = iProjectInformation1;
        }
        public IActionResult MYRequestsTask()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewRequestsTask = iRequestsTask.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditRequestsTask(int? IdRequestsTask)
        {
            ViewBag.USer = iUserInformation.GetAllByRole("Developer,Admin");        
            ViewBag.Task = iTask.GetAll();     
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewRequestsTask = iRequestsTask.GetAll();
            if (IdRequestsTask != null)
            {
                vmodel.RequestsTask = iRequestsTask.GetById(Convert.ToInt32(IdRequestsTask));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditRequestsTaskImage(int? IdRequestsTask)
        {
            ViewBag.USer = iUserInformation.GetAllByRole("Developer,Admin");
            ViewBag.Task = iTask.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewRequestsTask = iRequestsTask.GetAll();
            if (IdRequestsTask != null)
            {
                vmodel.RequestsTask = iRequestsTask.GetById(Convert.ToInt32(IdRequestsTask));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBRequestsTask slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdRequestsTask = model.RequestsTask.IdRequestsTask;
                slider.IdTask = model.RequestsTask.IdTask;
                slider.UserId = model.RequestsTask.UserId;
                slider.UserId = model.RequestsTask.UserId;
                slider.Photo = model.RequestsTask.Photo;
                slider.RequestsAr = model.RequestsTask.RequestsAr;
                slider.RequestsEn = model.RequestsTask.RequestsEn;
                slider.RequestsTitelEn = model.RequestsTask.RequestsTitelEn;
                slider.RequestsTitelAr = model.RequestsTask.RequestsTitelAr;
                slider.AddedBy = model.RequestsTask.AddedBy;
                slider.DateTimeEntry = model.RequestsTask.DateTimeEntry;
                slider.DataEntry = model.RequestsTask.DataEntry;
                slider.CurrentState = model.RequestsTask.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdRequestsTask == 0 || slider.IdRequestsTask == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    var reqwest = iRequestsTask.saveData(slider);
                    if (reqwest == true)
                    {
                        ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
                        var userd = vmodel.sUser = iUserInformation.GetById(slider.UserId);
                        var user = await _userManager.FindByIdAsync(slider.UserId);
                        //var user = await _userManager.GetUserAsync(User);
                        if (user == null)
                            return NotFound();
                        string namedovlober = user.Name;
                        string email = user.Email;
                        var TAskStatus = vmodel.ViewRequestsTask = iRequestsTask.GetByIdview(slider.IdRequestsTask);
                        if (user == null)
                            return NotFound();
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
                            message.From.Add(new MailboxAddress(slider.RequestsTitelEn, emailSetting.MailSender));
                            message.To.Add(new MailboxAddress(namedovlober, email));
                            message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "New Request  " + "By:" + slider.AddedBy;
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
                                           $"The Titel request: {slider.RequestsTitelEn}\n\n\n" +
                                           $"Description request: {slider.RequestsEn}\n\n\n" +
                                           $"Add by  : {slider.AddedBy}\n\n\n"
                            };
                            // إضافة الصورة كملف مرفق إذا كانت موجودة
                            if (!string.IsNullOrEmpty(slider.Photo))
                            {
                                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
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
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYRequestsTask");
                    }
                    else
                    {       
                        if (file.Count() > 0)
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iRequestsTask.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditRequestsTask");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                            return RedirectToAction("AddEditRequestsTask");
                        }                       
                    }
                }
                else//في حال التعديل
                {
                    if (file.Count() == 0)// في حال لا توجد صورة 
                    {
                        slider.Photo = model.RequestsTask.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iRequestsTask.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {

                            //أرسال البردي الاكتروني بدون صورة 
                            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
                            var userd = vmodel.sUser = iUserInformation.GetById(slider.UserId);
                            var user = await _userManager.FindByIdAsync(slider.UserId);
                            //var user = await _userManager.GetUserAsync(User);
                            if (user == null)
                                return NotFound();
                            string namedovlober = user.Name;
                            string email = user.Email;
                            var TAskStatus = vmodel.ViewRequestsTask = iRequestsTask.GetByIdview(slider.IdRequestsTask);
                            if (user == null)
                                return NotFound();
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
                                message.From.Add(new MailboxAddress(slider.RequestsTitelEn, emailSetting.MailSender));
                                message.To.Add(new MailboxAddress(namedovlober, email));
                                message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                                message.Subject = "Update  Request  " + "By:" + slider.AddedBy;
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
                                               $"The Titel request: {slider.RequestsTitelEn}\n\n\n" +
                                               $"Description request: {slider.RequestsEn}\n\n\n" +
                                               $"Add by  : {slider.AddedBy}\n\n\n"
                                };
                                // إضافة الصورة كملف مرفق إذا كانت موجودة
                                if (!string.IsNullOrEmpty(slider.Photo))
                                {
                                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
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
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYRequestsTask");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iService.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditRequestsTask");
                        }
                    }
                    else
                    {

                        //في حال رفع صورة   
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                        var reqweistDeletPoto = iRequestsTask.DELETPHOTO(slider.IdRequestsTask);
                        var reqestUpdate2 = iRequestsTask.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
                            var userd = vmodel.sUser = iUserInformation.GetById(slider.UserId);
                            var user = await _userManager.FindByIdAsync(slider.UserId);
                            //var user = await _userManager.GetUserAsync(User);
                            if (user == null)
                                return NotFound();
                            string namedovlober = user.Name;
                            string email = user.Email;
                            var TAskStatus = vmodel.ViewRequestsTask = iRequestsTask.GetByIdview(slider.IdRequestsTask);
                            if (user == null)
                                return NotFound();
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
                                message.From.Add(new MailboxAddress(slider.RequestsTitelEn, emailSetting.MailSender));
                                message.To.Add(new MailboxAddress(namedovlober, email));
                                message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                                message.Subject = "Update  Request  " + "By:" + slider.AddedBy;
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
                                               $"The Titel request: {slider.RequestsTitelEn}\n\n\n" +
                                               $"Description request: {slider.RequestsEn}\n\n\n" +
                                               $"Add by  : {slider.AddedBy}\n\n\n"
                                };
                                // إضافة الصورة كملف مرفق إذا كانت موجودة
                                if (!string.IsNullOrEmpty(slider.Photo))
                                {
                                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
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
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYRequestsTask");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iRequestsTask.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditRequestsTaskImage");
                        }
                    }
                }                                     
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("MYRequestsTask");

            }
        }














        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdRequestsTask)
        {
            var reqwistDelete = iRequestsTask.deleteData(IdRequestsTask);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYRequestsTask");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYRequestsTask");
            }
        }
        //ar 
        public IActionResult MYRequestsTaskAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewRequestsTask = iRequestsTask.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditRequestsTaskAr(int? IdRequestsTask)
        {
            ViewBag.USer = iUserInformation.GetAllByRole("Developer,Admin");

            ViewBag.Task = iTask.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewRequestsTask = iRequestsTask.GetAll();
            if (IdRequestsTask != null)
            {
                vmodel.RequestsTask = iRequestsTask.GetById(Convert.ToInt32(IdRequestsTask));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditRequestsTaskImageAr(int? IdRequestsTask)
        {
            ViewBag.USer = iUserInformation.GetAllByRole("Developer,Admin");
            ViewBag.Task = iTask.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewRequestsTask = iRequestsTask.GetAll();
            if (IdRequestsTask != null)
            {
                vmodel.RequestsTask = iRequestsTask.GetById(Convert.ToInt32(IdRequestsTask));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBRequestsTask slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdRequestsTask = model.RequestsTask.IdRequestsTask;
                slider.IdTask = model.RequestsTask.IdTask;
                slider.UserId = model.RequestsTask.UserId;
                slider.UserId = model.RequestsTask.UserId;
                slider.Photo = model.RequestsTask.Photo;
                slider.RequestsAr = model.RequestsTask.RequestsAr;
                slider.RequestsEn = model.RequestsTask.RequestsEn;
                slider.RequestsTitelEn = model.RequestsTask.RequestsTitelEn;
                slider.RequestsTitelAr = model.RequestsTask.RequestsTitelAr;
                slider.AddedBy = model.RequestsTask.AddedBy;
                slider.DateTimeEntry = model.RequestsTask.DateTimeEntry;
                slider.DataEntry = model.RequestsTask.DataEntry;
                slider.CurrentState = model.RequestsTask.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdRequestsTask == 0 || slider.IdRequestsTask == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    var reqwest = iRequestsTask.saveData(slider);
                    if (reqwest == true)
                    {
                        ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
                        var userd = vmodel.sUser = iUserInformation.GetById(slider.UserId);
                        var user = await _userManager.FindByIdAsync(slider.UserId);
                        //var user = await _userManager.GetUserAsync(User);
                        if (user == null)
                            return NotFound();
                        string namedovlober = user.Name;
                        string email = user.Email;
                        var TAskStatus = vmodel.ViewRequestsTask = iRequestsTask.GetByIdview(slider.IdRequestsTask);
                        if (user == null)
                            return NotFound();
                      
                        string ProjectNameAr = TAskStatus.ProjectNameAr;
                       
                        string taskstAr = TAskStatus.TitleAr;
                        string DescriptionAr = TAskStatus.DescriptionAr;
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
                            message.From.Add(new MailboxAddress(slider.RequestsTitelAr, emailSetting.MailSender));
                            message.To.Add(new MailboxAddress(namedovlober, email));
                            message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "طلب جديد من قبل : "  + slider.AddedBy;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"طلب جديد   \n\n\n" +
                                           $"عناية السيد/ة  {namedovlober}  المحترم  ...,\n\n\n" +

                                           $"يرجى الانتباه جيدًا للطلب التالي والرد عليه في أقرب وقت ممكن.. :\n\n\n" +
                                           $"الطلب متعلق بمشروع : {ProjectNameAr}\n\n\n" +
                                           $"والمرتبط بالمهمة: {taskstAr}\n\n\n" +
                                           $"الموضحة تاليا : {DescriptionAr}\n\n\n" +
                                           $"والتي تبدأ : {StartDate}\n\n\n" +
                                           $"وتنتهي: {EndtDate}\n\n\n" +
                                           $"عنوان الطلب : {slider.RequestsTitelAr}\n\n\n" +
                                           $"وصف الطلب : {slider.RequestsAr}\n\n\n" +
                                           $"أنشأ بواسطة   : {slider.AddedBy}\n\n\n"
                            };
                            // إضافة الصورة كملف مرفق إذا كانت موجودة
                            if (!string.IsNullOrEmpty(slider.Photo))
                            {
                                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
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
                        TempData["Saved successfully"] = ResourceWebAr.VLSavedSuccessfully;
                        return RedirectToAction("MYRequestsTaskAr");
                    }
                    else
                    {
                        if (file.Count() > 0)
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iRequestsTask.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                            return RedirectToAction("AddEditRequestsTaskAr");
                        }
                        else
                        {
                            TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                            return RedirectToAction("AddEditRequestsTaskAr");
                        }
                    }
                }
                else//في حال التعديل
                {
                    if (file.Count() == 0)// في حال لا توجد صورة 
                    {
                        slider.Photo = model.RequestsTask.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iRequestsTask.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            //أرسال البردي الاكتروني بدون صورة 
                            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
                            var userd = vmodel.sUser = iUserInformation.GetById(slider.UserId);
                            var user = await _userManager.FindByIdAsync(slider.UserId);
                            //var user = await _userManager.GetUserAsync(User);
                            if (user == null)
                                return NotFound();
                            string namedovlober = user.Name;
                            string email = user.Email;
                            var TAskStatus = vmodel.ViewRequestsTask = iRequestsTask.GetByIdview(slider.IdRequestsTask);
                            if (user == null)
                                return NotFound();
                            string ProjectNameAr = TAskStatus.ProjectNameAr;

                            string taskstAr = TAskStatus.TitleAr;
                            string DescriptionAr = TAskStatus.DescriptionAr;
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
                                message.From.Add(new MailboxAddress(slider.RequestsTitelAr, emailSetting.MailSender));
                                message.To.Add(new MailboxAddress(namedovlober, email));
                                message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                                message.Subject = "تعديل الطلب  من قبل : " + slider.AddedBy;
                                var builder = new BodyBuilder
                                {
                                    TextBody = $"طلب معدل   \n\n\n" +
                                               $"عناية السيد/ة  {namedovlober}  المحترم  ...,\n\n\n" +

                                               $"يرجى الانتباه جيدًا للطلب التالي والرد عليه في أقرب وقت ممكن.. :\n\n\n" +
                                               $"الطلب متعلق بمشروع : {ProjectNameAr}\n\n\n" +
                                               $"والمرتبط بالمهمة: {taskstAr}\n\n\n" +
                                               $"الموضحة تاليا : {DescriptionAr}\n\n\n" +
                                               $"والتي تبدأ : {StartDate}\n\n\n" +
                                               $"وتنتهي: {EndtDate}\n\n\n" +
                                               $"عنوان الطلب : {slider.RequestsTitelAr}\n\n\n" +
                                               $"وصف الطلب : {slider.RequestsAr}\n\n\n" +
                                               $"أنشأ بواسطة   : {slider.AddedBy}\n\n\n"
                                };
                                // إضافة الصورة كملف مرفق إذا كانت موجودة
                                if (!string.IsNullOrEmpty(slider.Photo))
                                {
                                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
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
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYRequestsTaskAr");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iService.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditRequestsTaskAr");
                        }
                    }
                    else
                    {
                        //في حال رفع صورة   
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                        var reqweistDeletPoto = iRequestsTask.DELETPHOTO(slider.IdRequestsTask);
                        var reqestUpdate2 = iRequestsTask.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
                            var userd = vmodel.sUser = iUserInformation.GetById(slider.UserId);
                            var user = await _userManager.FindByIdAsync(slider.UserId);
                            //var user = await _userManager.GetUserAsync(User);
                            if (user == null)
                                return NotFound();
                            string namedovlober = user.Name;
                            string email = user.Email;
                            var TAskStatus = vmodel.ViewRequestsTask = iRequestsTask.GetByIdview(slider.IdRequestsTask);
                            if (user == null)
                                return NotFound();
                            string ProjectNameAr = TAskStatus.ProjectNameAr;

                            string taskstAr = TAskStatus.TitleAr;
                            string DescriptionAr = TAskStatus.DescriptionAr;
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
                                message.From.Add(new MailboxAddress(slider.RequestsTitelEn, emailSetting.MailSender));
                                message.To.Add(new MailboxAddress(namedovlober, email));
                                message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                                message.Subject = "تعديل الطلب  من قبل : " + slider.AddedBy;
                                var builder = new BodyBuilder
                                {
                                    TextBody = $"طلب معدل   \n\n\n" +
                                               $"عناية السيد/ة  {namedovlober}  المحترم  ...,\n\n\n" +

                                               $"يرجى الانتباه جيدًا للطلب التالي والرد عليه في أقرب وقت ممكن.. :\n\n\n" +
                                               $"الطلب متعلق بمشروع : {ProjectNameAr}\n\n\n" +
                                               $"والمرتبط بالمهمة: {taskstAr}\n\n\n" +
                                               $"الموضحة تاليا : {DescriptionAr}\n\n\n" +
                                               $"والتي تبدأ : {StartDate}\n\n\n" +
                                               $"وتنتهي: {EndtDate}\n\n\n" +
                                               $"عنوان الطلب : {slider.RequestsTitelAr}\n\n\n" +
                                               $"وصف الطلب : {slider.RequestsAr}\n\n\n" +
                                               $"أنشأ بواسطة   : {slider.AddedBy}\n\n\n"
                                };
                                // إضافة الصورة كملف مرفق إذا كانت موجودة
                                if (!string.IsNullOrEmpty(slider.Photo))
                                {
                                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
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
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYRequestsTaskAr");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iRequestsTask.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditRequestsTaskImageAr");
                        }
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("MYRequestsTaskAr");

            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDataAr(int IdRequestsTask)
        {
            var reqwistDelete = iRequestsTask.deleteData(IdRequestsTask);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYRequestsTaskAr");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYRequestsTaskAr");
            }
        }
    }
}