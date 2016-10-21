using System.Collections.Generic;
using System.Threading.Tasks;
using Devpro.TfsApi.Dto;
using Devpro.TfsApi.Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devpro.TfsApi.Controllers
{
    [Authorize]
    [Route("api/queries")]
    public class QueriesController : Controller
    {
        private readonly IQueryService _queryService;

        public QueriesController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        // 
        /// <summary>
        /// GET: api/values
        /// 
        /// </summary>
        /// <param name="teamProjectName">Team project name. For example "BlueSky".</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<WorkItemDto>> Get(string teamProjectName)
        {
            return await _queryService.DoRestSample(teamProjectName);
        }
    }
}
