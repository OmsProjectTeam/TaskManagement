using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TypesOfTaskController : Controller
    {
        MasterDbcontext dbcontext;
        IITypesOfTask iTypesOfTask;
        public TypesOfTaskController(MasterDbcontext dbcontex1,IITypesOfTask iTypesOfTask1)
        {
            dbcontext= dbcontex1;
            iTypesOfTask= iTypesOfTask1;
        }
        public IActionResult MyTypesOfTask()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypesOfTask = iTypesOfTask.GetAll();
            return View(vmodel);
        }

        public IActionResult MyTypesOfTaskAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypesOfTask = iTypesOfTask.GetAll();
            return View(vmodel);
        }
        public IActionResult AddTypesOfTask(int? IdTypesOfTask)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypesOfTask = iTypesOfTask.GetAll();
            if (IdTypesOfTask != null)
            {
                vmodel.TypesOfTask = iTypesOfTask.GetById(Convert.ToInt32(IdTypesOfTask));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        public IActionResult AddTypesOfTaskAr(int? IdTypesOfTask)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypesOfTask = iTypesOfTask.GetAll();
            if (IdTypesOfTask != null)
            {
                vmodel.TypesOfTask = iTypesOfTask.GetById(Convert.ToInt32(IdTypesOfTask));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBTypesOfTask slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTypesOfTask = model.TypesOfTask.IdTypesOfTask;
                slider.TypesOfTask = model.TypesOfTask.TypesOfTask;
                slider.TypesOfTaskAr = model.TypesOfTask.TypesOfTaskAr;
                slider.Active = model.TypesOfTask.Active;

                slider.DataEntry = model.TypesOfTask.DataEntry;
                slider.DateTimeEntry = model.TypesOfTask.DateTimeEntry;
                slider.CurrentState = model.TypesOfTask.CurrentState;
                if (slider.IdTypesOfTask == 0 || slider.IdTypesOfTask == null)
                {
                    if (dbcontext.TBTypesOfTasks.Where(a => a.TypesOfTask == slider.TypesOfTask).ToList().Count > 0)
                    {
                        TempData["TypesOfTask"] = ResourceWeb.VLTypesOfTaskDoplceted;
                        return RedirectToAction("AddTypesOfTask", model);
                    }

                    if (dbcontext.TBTypesOfTasks.Where(a => a.TypesOfTaskAr == slider.TypesOfTaskAr).ToList().Count > 0)
                    {
                        TempData["TypesOfTaskAr"] = ResourceWeb.VLTypesOfTaskDoplceted;
                        return RedirectToAction("AddTypesOfTask", model);
                    }
                    var reqwest = iTypesOfTask.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyTypesOfTask");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddTypesOfTask");
                    }
                }
                else
                {
                    var reqestUpdate = iTypesOfTask.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTypesOfTask");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddTypesOfTask");

                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddTypesOfTask");

            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBTypesOfTask slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTypesOfTask = model.TypesOfTask.IdTypesOfTask;
                slider.TypesOfTask = model.TypesOfTask.TypesOfTask;
                slider.TypesOfTaskAr = model.TypesOfTask.TypesOfTaskAr;
                slider.Active = model.TypesOfTask.Active;
                slider.DataEntry = model.TypesOfTask.DataEntry;
                slider.DateTimeEntry = model.TypesOfTask.DateTimeEntry;
                slider.CurrentState = model.TypesOfTask.CurrentState;
                if (slider.IdTypesOfTask == 0 || slider.IdTypesOfTask == null)
                {
                    if (dbcontext.TBTypesOfTasks.Where(a => a.TypesOfTask == slider.TypesOfTask).ToList().Count > 0)
                    {
                        TempData["TypesOfTask"] = ResourceWebAr.VLTypesOfTaskDoplceted;
                        return RedirectToAction("AddTypesOfTaskAr", model);
                    }
                    if (dbcontext.TBTypesOfTasks.Where(a => a.TypesOfTaskAr == slider.TypesOfTaskAr).ToList().Count > 0)
                    {
                        TempData["TypesOfTaskAr"] = ResourceWebAr.VLTypesOfTaskArDoplceted;
                        return RedirectToAction("AddTypesOfTaskAr", model);
                    }
                    var reqwest = iTypesOfTask.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWebAr.VLSavedSuccessfully;
                        return RedirectToAction("MyTypesOfTaskAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                        return RedirectToAction("AddTypesOfTaskAr");

                    }
                }
                else
                {
                    var reqestUpdate = iTypesOfTask.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWebAr.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTypesOfTaskAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWebAr.VLErrorUpdate;
                        return RedirectToAction("AddTypesOfTaskAr");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWebAr.VLErrorSave;
                return RedirectToAction("AddTypesOfTaskAr");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdTypesOfTask)
        {
            var reqwistDelete = iTypesOfTask.deleteData(IdTypesOfTask);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyTypesOfTask");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyTypesOfTask");

            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 


        }


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDataAr(int IdTypesOfTask)
        {
            var reqwistDelete = iTypesOfTask.deleteData(IdTypesOfTask);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWebAr.VLdELETESuccessfully;
                return RedirectToAction("MyTypesOfTaskAr");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWebAr.VLErrorDeleteData;
                return RedirectToAction("MyTypesOfTaskAr");

            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 


        }
    }
}
