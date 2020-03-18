using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Repository;
using Models.ViewModels;

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

        public ActionResult Print()
        {
            rptTracker rpt = new rptTracker()
            {
                DataSource = new List<TrackerViewModel>() { new TrackerViewModel() }

            };
            MemoryStream ms = new MemoryStream();
            rpt.ExportToPdf(ms);
            return File(ms.ToArray(), "application/pdf", "TRACKER.pdf");
        }

        public ActionResult BarPartial()
        {
            return PartialView();
        }

        public ActionResult ChartPartial()
        {
            List<string> municipality = new List<string>() { "VILLAVERDE", "BAGABAG", "DIADI", "BAYOMBONG", "DUPAX DEL SUR", "AMBAGUIO", "SANTA FE", "BAMBANG", "BAYOMBONG", "ARITAO", "DUPAX DEL NORTE", "SOLANO", "QUEZON", "KASIBU", "KAYAPA", "ALFONSO CASTANEDA" }.OrderBy(x => x).ToList();
            var model = municipality.Select(x => new { Municipality = x, Total = unitOfWork.PatientsRepo.Fetch(m => m.Area == x).Count() }); //unitOfWork.PatientsRepo.Fetch().GroupBy(x => x.Area).Select(x => new { Area = x.Key }).ToList().Select(x => new { Area = x.Area, Total = unitOfWork.PatientsRepo.Fetch(m => m.Area == x.Area).Count() });
            return PartialView("_ChartPartial", model);
        }
    }
}