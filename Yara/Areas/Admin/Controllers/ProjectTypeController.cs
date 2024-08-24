

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ProjectTypeController : Controller
    {
        MasterDbcontext dbcontext;
        IIProjectType iProjectType;
        public ProjectTypeController(MasterDbcontext dbcontext1,IIProjectType iProjectType1)
        {
            dbcontext = dbcontext1;
            iProjectType= iProjectType1; 
        }

        public IActionResult MyProjectType()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListProjectType = iProjectType.GetAll();
            return View(vmodel);
        }

        public IActionResult MyProjectTypeAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListProjectType = iProjectType.GetAll();
            return View(vmodel);
        }
        public IActionResult AddProjectType(int? IdProjectType)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListProjectType = iProjectType.GetAll();
            if (IdProjectType != null)
            {
                vmodel.ProjectType = iProjectType.GetById(Convert.ToInt32(IdProjectType));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        public IActionResult AddProjectTypeAr(int? IdProjectType)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListProjectType = iProjectType.GetAll();
            if (IdProjectType != null)
            {
                vmodel.ProjectType = iProjectType.GetById(Convert.ToInt32(IdProjectType));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBProjectType slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdProjectType = model.ProjectType.IdProjectType;
                slider.ProjectTypes = model.ProjectType.ProjectTypes;
                slider.ProjectTypesAr = model.ProjectType.ProjectTypesAr;
                slider.Active = model.ProjectType.Active;

                slider.DataEntry = model.ProjectType.DataEntry;
                slider.DateTimeEntry = model.ProjectType.DateTimeEntry;
                slider.CurrentState = model.ProjectType.CurrentState;
                if (slider.IdProjectType == 0 || slider.IdProjectType == null)
                {
                    if (dbcontext.TBProjectTypes.Where(a => a.ProjectTypes == slider.ProjectTypes).ToList().Count > 0)
                    {
                        TempData["ProjectType"] = ResourceWeb.VLProjectTypeDoplceted;
                        return RedirectToAction("AddProjectType", model);
                    }

                    if (dbcontext.TBProjectTypes.Where(a => a.ProjectTypesAr == slider.ProjectTypesAr).ToList().Count > 0)
                    {
                        TempData["ProjectTypeAr"] = ResourceWeb.VLProjectTypeDoplceted;
                        return RedirectToAction("AddProjectType", model);
                    }
                    var reqwest = iProjectType.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyProjectType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddProjectType");
                    }
                }
                else
                {
                    var reqestUpdate = iProjectType.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyProjectType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddProjectType");

                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddProjectType");

            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBProjectType slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdProjectType = model.ProjectType.IdProjectType;
                slider.ProjectTypes = model.ProjectType.ProjectTypes;
                slider.ProjectTypesAr = model.ProjectType.ProjectTypesAr;
                slider.Active = model.ProjectType.Active;
                slider.DataEntry = model.ProjectType.DataEntry;
                slider.DateTimeEntry = model.ProjectType.DateTimeEntry;
                slider.CurrentState = model.ProjectType.CurrentState;
                if (slider.IdProjectType == 0 || slider.IdProjectType == null)
                {
                    if (dbcontext.TBProjectTypes.Where(a => a.ProjectTypes == slider.ProjectTypes).ToList().Count > 0)
                    {
                        TempData["ProjectType"] = ResourceWebAr.VLProjectTypeDoplceted;
                        return RedirectToAction("AddProjectTypeAr", model);
                    }
                    if (dbcontext.TBProjectTypes.Where(a => a.ProjectTypesAr == slider.ProjectTypesAr).ToList().Count > 0)
                    {
                        TempData["ProjectTypeAr"] = ResourceWebAr.VLProjectTypeDoplceted;
                        return RedirectToAction("AddProjectTypeAr", model);
                    }
                    var reqwest = iProjectType.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWebAr.VLSavedSuccessfully;
                        return RedirectToAction("MyProjectTypeAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                        return RedirectToAction("AddProjectTypeAr");

                    }
                }
                else
                {
                    var reqestUpdate = iProjectType.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWebAr.VLUpdatedSuccessfully;
                        return RedirectToAction("MyProjectTypeAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWebAr.VLErrorUpdate;
                        return RedirectToAction("AddProjectTypeAr");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                return RedirectToAction("AddProjectTypeAr");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdProjectType)
        {
            var reqwistDelete = iProjectType.deleteData(IdProjectType);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyProjectType");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyProjectType");

            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 


        }


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDataAr(int IdProjectType)
        {
            var reqwistDelete = iProjectType.deleteData(IdProjectType);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWebAr.VLdELETESuccessfully;
                return RedirectToAction("MyProjectTypeAr");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWebAr.VLErrorDeleteData;
                return RedirectToAction("MyProjectTypeAr");

            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 


        }
    }
}
