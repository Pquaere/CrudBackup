using learning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace learning.Controllers
{
    public class WebApiController : ApiController
    {
        // GET: api/WebApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WebApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WebApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WebApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WebApi/5
        public void Delete(int id)
        {
        }


        [HttpPost]
        public HttpResponseMessage Registration(Student obj)
        {
           
            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
    }
}
