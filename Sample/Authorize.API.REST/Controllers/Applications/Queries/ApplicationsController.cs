using Authorize.API.REST.Bases;
using Authorize.Application.Features.Applications.Queries.Models;
using Authorize.Application.Features.Applications.Queries.Search.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ML.DataStructure.Collections.DTO;
using System.Threading.Tasks;

namespace Authorize.API.REST.Controllers.Applications.Queries
{
    [ApiController]
    [Route("api/queries/applications")]
    public class ApplicationsController : QueriesController
    {
        public ApplicationsController(IMediator mediator)
            : base(mediator)
        {

        }
       
        [HttpPost("Search")]
        public async Task<PagedListDTO<ApplicationVM>> SearchApplication([FromBody] SearchAppQuery query)
        {
            var response = await Query(query);
            return response;
        }
    }
}
