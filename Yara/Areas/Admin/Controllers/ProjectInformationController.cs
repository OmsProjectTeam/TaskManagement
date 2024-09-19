using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Yara.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ProjectInformationController : Controller
	{
		MasterDbcontext dbcontext;
		IIProjectInformation iProjectInformation;
		IIProjectType iProjectType;
		public ProjectInformationController(MasterDbcontext dbcontext1,IIProjectInformation iProjectInformation1,IIProjectType  iProjectType1)
        {
			dbcontext= dbcontext1;
			iProjectInformation = iProjectInformation1;
			iProjectType = iProjectType1;
		}
		public IActionResult MyProjectInformation()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListViewProjectInformation = iProjectInformation.GetAll();
			return View(vmodel);
		}
		public IActionResult MyProjectInformationAr()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListViewProjectInformation = iProjectInformation.GetAll();
			return View(vmodel);
		}
		public IActionResult AddProjectInformation(int? IdProjectInformation)
		{
			ViewBag.projectType= iProjectType.GetAll();
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListViewProjectInformation = iProjectInformation.GetAll();
			if (IdProjectInformation != null)
			{
				vmodel.ProjectInformation = iProjectInformation.GetById(Convert.ToInt32(IdProjectInformation));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}
		public IActionResult AddProjectInformationAr(int? IdProjectInformation)
		{
			ViewBag.projectType = iProjectType.GetAll();

			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListViewProjectInformation = iProjectInformation.GetAll();
			if (IdProjectInformation != null)
			{
				vmodel.ProjectInformation = iProjectInformation.GetById(Convert.ToInt32(IdProjectInformation));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBProjectInformation slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdProjectInformation = model.ProjectInformation.IdProjectInformation;
				slider.IdProjectType = model.ProjectInformation.IdProjectType;
				slider.ProjectName = model.ProjectInformation.ProjectName;
				slider.ProjectDescription = model.ProjectInformation.ProjectDescription;
				slider.ProjectNameAr = model.ProjectInformation.ProjectNameAr;
				slider.ProjectDescriptionAr = model.ProjectInformation.ProjectDescriptionAr;
				slider.ProjectStart = model.ProjectInformation.ProjectStart;
				slider.ProjectEnd = model.ProjectInformation.ProjectEnd;		
				slider.DataEntry = model.ProjectInformation.DataEntry;
				slider.DateTimeEntry = model.ProjectInformation.DateTimeEntry;
				slider.CurrentState = model.ProjectInformation.CurrentState;
				if (slider.IdProjectInformation == 0 || slider.IdProjectInformation == null)
				{
					if (dbcontext.TBProjectInformations.Where(a => a.ProjectName == slider.ProjectName).ToList().Count > 0)
					{
						TempData["ProjectInformation"] = ResourceWeb.VLProjectInformationDoplceted;
						return RedirectToAction("AddProjectInformation", model);
					}
					if (dbcontext.TBProjectInformations.Where(a => a.ProjectNameAr == slider.ProjectNameAr).ToList().Count > 0)
					{
						TempData["ProjectInformationAr"] = ResourceWeb.VLProjectInformationArDoplceted;
						return RedirectToAction("AddProjectInformation", model);
					}
					var reqwest = iProjectInformation.saveData(slider);
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
                            message.From.Add(new MailboxAddress(slider.ProjectName, emailSetting.MailSender));

                            message.To.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "New Project  " + "By:" + slider.DataEntry;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"New Project  \n\n\n" +
                                           $"Attn: Mr  saif aldin\n\n\n" +
                                           $"Greetings" +
                                           $"A new project has been created entitled :\n\n\n" +
                                           $"Titel : {slider.ProjectName}\n\n\n" +
                                           $"Description : {slider.ProjectDescription}\n\n\n" +
                                           $"Start Date : {slider.ProjectStart}\n\n\n" +
                                           $"End Date: {slider.ProjectEnd}\n\n\n" +
                                           $"Add by  : {slider.DataEntry}\n\n\n"
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
						return RedirectToAction("MyProjectInformation");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return RedirectToAction("AddProjectInformation");
					}
				}
				else
				{
					var reqestUpdate = iProjectInformation.UpdateData(slider);
					if (reqestUpdate == true)
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
                            message.From.Add(new MailboxAddress(slider.ProjectName, emailSetting.MailSender));
                           
                            message.To.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "Update Project  " + "By:" + slider.DataEntry;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"New Project  \n\n\n" +
                                           $"Attn: Mr  saif aldin\n\n\n" +
                                           $"Greetings" +
                                           $"Please note that the following project has been modified. :\n\n\n" +
                                        
                                           $"Titel : {slider.ProjectName}\n\n\n" +
                                           $"Description : {slider.ProjectDescription}\n\n\n" +                                      
                                           $"Start Date : {slider.ProjectStart}\n\n\n" +
                                           $"End Date: {slider.ProjectEnd}\n\n\n" +
                                           $"Add by  : {slider.DataEntry}\n\n\n"
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
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyProjectInformation");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
						return RedirectToAction("AddProjectInformation");

					}
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return RedirectToAction("AddProjectInformation");

			}
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBProjectInformation slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdProjectInformation = model.ProjectInformation.IdProjectInformation;
				slider.IdProjectType = model.ProjectInformation.IdProjectType;
				slider.ProjectName = model.ProjectInformation.ProjectName;
				slider.ProjectDescription = model.ProjectInformation.ProjectDescription;
				slider.ProjectNameAr = model.ProjectInformation.ProjectNameAr;
				slider.ProjectDescriptionAr = model.ProjectInformation.ProjectDescriptionAr;
				slider.ProjectStart = model.ProjectInformation.ProjectStart;
				slider.ProjectEnd = model.ProjectInformation.ProjectEnd;
				slider.DataEntry = model.ProjectInformation.DataEntry;
				slider.DateTimeEntry = model.ProjectInformation.DateTimeEntry;
				slider.CurrentState = model.ProjectInformation.CurrentState;
				if (slider.IdProjectInformation == 0 || slider.IdProjectInformation == null)
				{
					if (dbcontext.TBProjectInformations.Where(a => a.ProjectName == slider.ProjectName).ToList().Count > 0)
					{
						TempData["ProjectInformation"] = ResourceWebAr.VLProjectInformationDoplceted;
						return RedirectToAction("AddProjectInformation", model);
					}

					if (dbcontext.TBProjectInformations.Where(a => a.ProjectNameAr == slider.ProjectNameAr).ToList().Count > 0)
					{
						TempData["ProjectInformationAr"] = ResourceWebAr.VLProjectInformationArDoplceted;
						return RedirectToAction("AddProjectInformation", model);
					}
					var reqwest = iProjectInformation.saveData(slider);
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
                            message.From.Add(new MailboxAddress(slider.ProjectNameAr, emailSetting.MailSender));

                            message.To.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "مشروع جديد  " + "By:" + slider.DataEntry;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"مشروع جديد   \n\n\n" +
                                           $"عناية السيد/ة :  saif aldin . المحترم /ة\n\n\n" +
                                           $"تحية طيبة وبعد " +
                                           $"تم أنشاء مشروع جديد وتاليا تفاصيله  :\n\n\n" +
                                        
                                           $"اسم المشروع  : {slider.ProjectNameAr}\n\n\n" +
                                           $"وصف المشروع : {slider.ProjectDescriptionAr}\n\n\n" +
                                           $"تاريخ البدأ  : {slider.ProjectStart}\n\n\n" +
                                           $"تاريخ الانتهاء: {slider.ProjectEnd}\n\n\n" +
                                           $"تم الاضافة بواسطة  : {slider.DataEntry}\n\n\n"
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
                        TempData["Saved successfully"] = ResourceWebAr.VLSavedSuccessfully;
						return RedirectToAction("MyProjectInformationAr");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
						return RedirectToAction("AddProjectInformationAr");
					}
				}
				else
				{
					var reqestUpdate = iProjectInformation.UpdateData(slider);
					if (reqestUpdate == true)
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
                            message.From.Add(new MailboxAddress(slider.ProjectNameAr, emailSetting.MailSender));

                            message.To.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "تعديل مشروع " + "By:" + slider.DataEntry;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"مشروع جديد   \n\n\n" +
                                           $"عناية السيد/ة :  saif aldin . المحترم /ة\n\n\n" +
                                           $"تحية طيبة وبعد " +
                                           $"يرجى العلم بأنه تم تعديل المشروع التالي   :\n\n\n" +

                                           $"اسم المشروع  : {slider.ProjectNameAr}\n\n\n" +
                                           $"وصف المشروع : {slider.ProjectDescriptionAr}\n\n\n" +
                                           $"تاريخ البدأ  : {slider.ProjectStart}\n\n\n" +
                                           $"تاريخ الانتهاء: {slider.ProjectEnd}\n\n\n" +
                                           $"تم الاضافة بواسطة  : {slider.DataEntry}\n\n\n"
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





                        TempData["Saved successfully"] = ResourceWebAr.VLUpdatedSuccessfully;
						return RedirectToAction("MyProjectInformationAr");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWebAr.VLErrorUpdate;
						return RedirectToAction("AddProjectInformationAr");
					}
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
				return RedirectToAction("AddProjectInformationAr");
			}
		}
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteData(int IdProjectInformation)
		{
			var reqwistDelete = iProjectInformation.deleteData(IdProjectInformation);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyProjectInformation");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyProjectInformation");

			}		
		}
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteDataAr(int IdProjectInformation)
		{
			var reqwistDelete = iProjectInformation.deleteData(IdProjectInformation);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWebAr.VLdELETESuccessfully;
				return RedirectToAction("MyProjectInformationAr");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWebAr.VLErrorDeleteData;
				return RedirectToAction("MyProjectInformationAr");
			}
			// تمرير التاسكات  من الادارة 
			// استخدام نظام أجايا وجيرا 

		}
	}
}