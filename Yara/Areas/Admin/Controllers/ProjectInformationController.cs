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
			// تمرير التاسكات  من الادارة 
			// استخدام نظام أجايا وجيرا 
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