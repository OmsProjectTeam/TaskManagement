

namespace Yara.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class EmailAlartSettingController : Controller
	{
		MasterDbcontext dbcontext;
		IIEmailAlartSetting iEmailAlartSetting;
		public EmailAlartSettingController(MasterDbcontext dbcontext1,IIEmailAlartSetting iEmailAlartSetting1)
        {
			dbcontext= dbcontext1;
			iEmailAlartSetting = iEmailAlartSetting1;

		}
		public IActionResult MyEmailAlartSetting()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListEmailAlartSetting = iEmailAlartSetting.GetAll();
			return View(vmodel);
		}

		public IActionResult MyEmailAlartSettingAr()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListEmailAlartSetting = iEmailAlartSetting.GetAll();
			return View(vmodel);
		}
		public IActionResult AddEmailAlartSetting(int? IdEmailAlartSetting)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListEmailAlartSetting = iEmailAlartSetting.GetAll();
			if (IdEmailAlartSetting != null)
			{
				vmodel.EmailAlartSetting = iEmailAlartSetting.GetById(Convert.ToInt32(IdEmailAlartSetting));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

		public IActionResult AddEmailAlartSettingAr(int? IdEmailAlartSetting)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListEmailAlartSetting = iEmailAlartSetting.GetAll();
			if (IdEmailAlartSetting != null)
			{
				vmodel.EmailAlartSetting = iEmailAlartSetting.GetById(Convert.ToInt32(IdEmailAlartSetting));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBEmailAlartSetting slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdEmailAlartSetting = model.EmailAlartSetting.IdEmailAlartSetting;
				slider.MailSender = model.EmailAlartSetting.MailSender;
				slider.SmtpServer = model.EmailAlartSetting.SmtpServer;
				slider.PortServer = model.EmailAlartSetting.PortServer;
				slider.PasswordEmail = model.EmailAlartSetting.PasswordEmail;
				slider.Ssl_validity = model.EmailAlartSetting.Ssl_validity;	
				slider.DataEntry = model.EmailAlartSetting.DataEntry;
				slider.DateTimeEntry = model.EmailAlartSetting.DateTimeEntry;
				slider.CurrentState = model.EmailAlartSetting.CurrentState;
				slider.Active = model.EmailAlartSetting.Active;
				if (slider.IdEmailAlartSetting == 0 || slider.IdEmailAlartSetting == null)
				{
					if (dbcontext.TBEmailAlartSettings.Where(a => a.MailSender == slider.MailSender).ToList().Count > 0)
					{
						TempData["EmailAlartSetting"] = ResourceWeb.VLEmailAlartSettingDoplceted;
						return RedirectToAction("AddEmailAlartSetting", model);
					}
					var reqwest = iEmailAlartSetting.saveData(slider);
					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyEmailAlartSetting");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEmailAlartSetting");
                    }
				}
				else
				{
					var reqestUpdate = iEmailAlartSetting.UpdateData(slider);
					if (reqestUpdate == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyEmailAlartSetting");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddEmailAlartSetting");
                    }
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return Redirect(returnUrl);
			}
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBEmailAlartSetting slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdEmailAlartSetting = model.EmailAlartSetting.IdEmailAlartSetting;
				slider.MailSender = model.EmailAlartSetting.MailSender;
				slider.SmtpServer = model.EmailAlartSetting.SmtpServer;
				slider.PortServer = model.EmailAlartSetting.PortServer;
				slider.PasswordEmail = model.EmailAlartSetting.PasswordEmail;
				slider.Ssl_validity = model.EmailAlartSetting.Ssl_validity;
				slider.DataEntry = model.EmailAlartSetting.DataEntry;
				slider.DateTimeEntry = model.EmailAlartSetting.DateTimeEntry;
				slider.CurrentState = model.EmailAlartSetting.CurrentState;
				slider.Active = model.EmailAlartSetting.Active;
				if (slider.IdEmailAlartSetting == 0 || slider.IdEmailAlartSetting == null)
				{
					if (dbcontext.TBEmailAlartSettings.Where(a => a.MailSender == slider.MailSender).ToList().Count > 0)
					{
						TempData["EmailAlartSetting"] = ResourceWeb.VLEmailAlartSettingDoplceted;
						return RedirectToAction("AddEmailAlartSettingAr", model);
					}

					var reqwest = iEmailAlartSetting.saveData(slider);
					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyEmailAlartSettingAr");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEmailAlartSettingAr");
                    }
				}
				else
				{
					var reqestUpdate = iEmailAlartSetting.UpdateData(slider);
					if (reqestUpdate == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyEmailAlartSettingAr");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddEmailAlartSettingAr");
                    }
				}
			}
			catch
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
				return Redirect(returnUrl);
			}
		}
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteData(int IdEmailAlartSetting)
		{
			var reqwistDelete = iEmailAlartSetting.deleteData(IdEmailAlartSetting);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyEmailAlartSetting");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyEmailAlartSetting");

			}
		}
			}
	}