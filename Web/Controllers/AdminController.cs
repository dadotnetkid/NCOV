using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Repository;

namespace Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult PatientsGridViewPartial()
        {
            var model = unitOfWork.PatientsRepo.Get();
            return PartialView("_PatientsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PatientsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Patients item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.PatientsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.PatientsRepo.Get();
            return PartialView("_PatientsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PatientsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Patients item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.PatientsRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.PatientsRepo.Get();
            return PartialView("_PatientsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PatientsGridViewPartialDelete(System.Int32 Id)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.PatientsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.PatientsRepo.Get();
            return PartialView("_PatientsGridViewPartial", model);
        }
    }
}