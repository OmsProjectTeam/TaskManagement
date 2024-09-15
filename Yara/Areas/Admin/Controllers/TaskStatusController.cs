

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TaskStatusController : Controller
    {
        MasterDbcontext dbcontext;
        IITaskStatus iTaskStatus;
        public TaskStatusController(MasterDbcontext dbcontext1,IITaskStatus iTaskStatus1)
        {
            dbcontext= dbcontext1;
            iTaskStatus = iTaskStatus1;
        }
        public IActionResult MyTaskStatus()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTaskStatus = iTaskStatus.GetAll();
            return View(vmodel);
        }

        public IActionResult MyTaskStatusAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTaskStatus = iTaskStatus.GetAll();
            return View(vmodel);
        }
        public IActionResult AddTaskStatus(int? IdTaskStatus)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTaskStatus = iTaskStatus.GetAll();
            if (IdTaskStatus != null)
            {
                vmodel.TaskStatus = iTaskStatus.GetById(Convert.ToInt32(IdTaskStatus));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        public IActionResult AddTaskStatusAr(int? IdTaskStatus)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTaskStatus = iTaskStatus.GetAll();
            if (IdTaskStatus != null)
            {
                vmodel.TaskStatus = iTaskStatus.GetById(Convert.ToInt32(IdTaskStatus));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBTaskStatus slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTaskStatus = model.TaskStatus.IdTaskStatus;
                slider.TaskStatus = model.TaskStatus.TaskStatus;
                slider.TaskStatusAr = model.TaskStatus.TaskStatusAr;
                slider.Active = model.TaskStatus.Active;

                slider.DataEntry = model.TaskStatus.DataEntry;
                slider.DateTimeEntry = model.TaskStatus.DateTimeEntry;
                slider.CurrentState = model.TaskStatus.CurrentState;
                if (slider.IdTaskStatus == 0 || slider.IdTaskStatus == null)
                {
                    if (dbcontext.TBTaskStatuss.Where(a => a.TaskStatus == slider.TaskStatus).ToList().Count > 0)
                    {
                        TempData["TaskStatus"] = ResourceWeb.VLTaskStatusDoplceted;
                        return RedirectToAction("AddTaskStatus", model);
                    }

                    if (dbcontext.TBTaskStatuss.Where(a => a.TaskStatusAr == slider.TaskStatusAr).ToList().Count > 0)
                    {
                        TempData["TaskStatusAr"] = ResourceWeb.VLTaskStatusArDoplceted;
                        return RedirectToAction("AddTaskStatus", model);
                    }
                    var reqwest = iTaskStatus.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyTaskStatus");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddTaskStatus");
                    }
                }
                else
                {
                    var reqestUpdate = iTaskStatus.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTaskStatus");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddTaskStatus");

                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddTaskStatus");

            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBTaskStatus slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTaskStatus = model.TaskStatus.IdTaskStatus;
                slider.TaskStatus = model.TaskStatus.TaskStatus;
                slider.TaskStatusAr = model.TaskStatus.TaskStatusAr;
                slider.Active = model.TaskStatus.Active;
                slider.DataEntry = model.TaskStatus.DataEntry;
                slider.DateTimeEntry = model.TaskStatus.DateTimeEntry;
                slider.CurrentState = model.TaskStatus.CurrentState;
                if (slider.IdTaskStatus == 0 || slider.IdTaskStatus == null)
                {
                    if (dbcontext.TBTaskStatuss.Where(a => a.TaskStatus == slider.TaskStatus).ToList().Count > 0)
                    {
                        TempData["TaskStatus"] = ResourceWebAr.VLTaskStatusDoplceted;
                        return RedirectToAction("AddTaskStatusAr", model);
                    }
                    if (dbcontext.TBTaskStatuss.Where(a => a.TaskStatusAr == slider.TaskStatusAr).ToList().Count > 0)
                    {
                        TempData["TaskStatusAr"] = ResourceWebAr.VLTaskStatusArDoplceted;
                        return RedirectToAction("AddTaskStatusAr", model);
                    }
                    var reqwest = iTaskStatus.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWebAr.VLSavedSuccessfully;
                        return RedirectToAction("MyTaskStatusAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                        return RedirectToAction("AddTaskStatusAr");

                    }
                }
                else
                {
                    var reqestUpdate = iTaskStatus.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWebAr.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTaskStatusAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWebAr.VLErrorUpdate;
                        return RedirectToAction("AddTaskStatusAr");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                return RedirectToAction("AddTaskStatusAr");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdTaskStatus)
        {
            var reqwistDelete = iTaskStatus.deleteData(IdTaskStatus);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyTaskStatus");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyTaskStatus");

            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 


        }


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDataAr(int IdTaskStatus)
        {
            var reqwistDelete = iTaskStatus.deleteData(IdTaskStatus);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWebAr.VLdELETESuccessfully;
                return RedirectToAction("MyTaskStatusAr");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWebAr.VLErrorDeleteData;
                return RedirectToAction("MyTaskStatusAr");

            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 


        }
    }
}
