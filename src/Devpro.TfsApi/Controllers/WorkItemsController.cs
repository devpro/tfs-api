using System.Collections.Generic;
using System.Threading.Tasks;
using Devpro.TfsApi.Dto;
using Devpro.TfsApi.Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devpro.TfsApi.Controllers
{
    [Authorize]
    [Route("api/workitems")]
    public class WorkItemsController : Controller
    {
        private readonly IQueryService _queryService;

        public WorkItemsController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        /// <summary>
        /// GET: api/values
        /// </summary>
        /// <param name="teamProjectName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<WorkItemDto>> Get(string teamProjectName)
        {
            return await _queryService.DoRestSample(teamProjectName);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //    // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //    // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    // For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see http://go.microsoft.com/fwlink/?LinkID=717803
        //}
    }
}
