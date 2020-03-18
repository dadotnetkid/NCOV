using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using Models.Repository;

namespace Web.Controllers
{
    [RoutePrefix("api/v2/patients")]
    public class PatientsApi : ApiController
    {
        // GET api/<controller>

        private UnitOfWork unitOfWork = new UnitOfWork();
        [Route("get")]
        [HttpGet]
        public IEnumerable<Patients> Get()
        {
            return unitOfWork.PatientsRepo.Get();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}