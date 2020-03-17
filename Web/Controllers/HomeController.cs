using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Repository;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PUIPartial()
        {
            var pUI = unitOfWork.PatientsRepo.Fetch().Count();
            ViewBag.PUI = pUI;
            return PartialView();
        }
        public ActionResult ConfirmedPartial()
        {
            var confirmed = unitOfWork.PatientsRepo.Fetch(m => m.Status == "CONFIRMED").Count();
            ViewBag.Confirmed = confirmed;
            return PartialView();
        }

        public ActionResult NegativePartial()
        {
            var negative = unitOfWork.PatientsRepo.Fetch(m => m.Status == "NEGATIVE").Count();
            ViewBag.negative = negative;
            return PartialView();
        }
        public ActionResult PatientsPartial()
        {
            var model = unitOfWork.PatientsRepo.Get();
            return PartialView(model);
        }

        public ActionResult DeathsPartial()
        {
            var Death = unitOfWork.PatientsRepo.Fetch(m => m.Status == "Death").Count();
            ViewBag.Death = Death;
            return PartialView();
        }
        public ActionResult AwaitingPartial()
        {
            var awaiting = unitOfWork.PatientsRepo.Fetch(m => m.Status == "AWAITING LAB RESULTS").Count();
            ViewBag.Awaiting = awaiting;
            return PartialView();
        }
    }
}