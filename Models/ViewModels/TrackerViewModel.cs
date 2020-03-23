using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;

namespace Models.ViewModels
{
    public class TrackerViewModel
    {
        public List<Patients> Patients => new UnitOfWork().PatientsRepo.Get().ToList();
        public int? Confirmed => Patients.Count(m => m.Result == "Confirmed");
        public int? Total => Patients.Count();
        public int? Awaiting => Patients.Count(m => m.Result  == "AWAITING LAB RESULTS");
        public int? Death=> Patients.Count(m => m.Result == "Dead");
        public int? Negative => Patients.Count(m => m.Result.Contains("NEGATIVE") || m.Result.Contains("DISCHARGE"));
    }

}

//            var negative = unitOfWork.PatientsRepo.Fetch(m => m.Result .Contains("NEGATIVE") || m.Result.Contains("DISCHARGE") ).Count();
