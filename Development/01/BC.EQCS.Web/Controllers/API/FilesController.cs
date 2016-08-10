using System.Collections.Generic;
using System.Web.Http;

namespace BC.EQCS.Web.Controllers.API
{
    public class FilesController : ApiController
    {
        // GET: api/Files
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Files/5
        public string Get(string filename)
        {
            return "value";
        }

        // POST: api/Files
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Files/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Files/5
        public void Delete(int id)
        {
        }
    }
}
