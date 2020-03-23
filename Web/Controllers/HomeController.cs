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

        public ActionResult IndexPartial()
        {
            return PartialView();
        }
        public ActionResult PUIPartial()
        {
            var pUI = unitOfWork.PatientsRepo.Fetch().Count();
            ViewBag.PUI = pUI;
            return PartialView();
        }
        public ActionResult ConfirmedPartial()
        {
            var confirmed = unitOfWork.PatientsRepo.Fetch(m => m.Result == "CONFIRMED").Count();
            ViewBag.Confirmed = confirmed;
            return PartialView();
        }

        public ActionResult NegativePartial()
        {
            var negative = unitOfWork.PatientsRepo.Fetch(m => m.Result .Contains("NEGATIVE") || m.Result.Contains("DISCHARGE") ).Count();
            ViewBag.negative = negative;
            return PartialView();
        }

        public ActionResult DeathsPartial()
        {
            var Death = unitOfWork.PatientsRepo.Fetch(m => m.Result == "Death").Count();
            ViewBag.Death = Death;
            return PartialView();
        }
        public ActionResult AwaitingPartial()
        {
            var awaiting = unitOfWork.PatientsRepo.Fetch(m => m.Result == "AWAITING LAB RESULTS").Count();
            ViewBag.Awaiting = awaiting;
            return PartialView();
        }
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult PatientsGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]string municipality = "", [ModelBinder(typeof(DevExpressEditorsBinder))]string status = "")
        {
            var model = unitOfWork.PatientsRepo.Get(m => m.Area.Contains(municipality) );
            if(!string.IsNullOrWhiteSpace(status))
                model = unitOfWork.PatientsRepo.Get(m => m.Result.Contains(status));
            return PartialView("_PatientsGridViewPartial", model);
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

        public JsonResult ChartDataPartial()
        {
            List<string> municipality = new List<string>() { "VILLAVERDE", "BAGABAG", "DIADI", "DUPAX DEL SUR", "AMBAGUIO", "SANTA FE", "BAMBANG", "BAYOMBONG", "ARITAO", "DUPAX DEL NORTE", "SOLANO", "QUEZON", "KASIBU", "KAYAPA", "ALFONSO CASTANEDA" }.OrderBy(x => x).ToList();
            var model = municipality.Select(x => new { Municipality = x, Total = unitOfWork.PatientsRepo.Fetch(m => m.Area == x).Count() }); //unitOfWork.PatientsRepo.Fetch().GroupBy(x => x.Area).Select(x => new { Area = x.Key }).ToList().Select(x => new { Area = x.Area, Total
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartPartial()
        {
            List<string> municipality = new List<string>() { "VILLAVERDE", "BAGABAG", "DIADI", "DUPAX DEL SUR", "AMBAGUIO", "SANTA FE", "BAMBANG", "BAYOMBONG", "ARITAO", "DUPAX DEL NORTE", "SOLANO", "QUEZON", "KASIBU", "KAYAPA", "ALFONSO CASTANEDA" }.OrderBy(x => x).ToList();
            var model = municipality.Select(x => new { Municipality = x, Total = unitOfWork.PatientsRepo.Fetch(m => m.Area == x).Count() }); //unitOfWork.PatientsRepo.Fetch().GroupBy(x => x.Area).Select(x => new { Area = x.Key }).ToList().Select(x => new { Area = x.Area, Total = unitOfWork.PatientsRepo.Fetch(m => m.Area == x.Area).Count() });
            return PartialView("_ChartPartial", model);
        }
    }
}