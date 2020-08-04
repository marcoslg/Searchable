using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authorize.API.REST.Bases
{
    public class QueriesController : ControllerBase
    {

        protected readonly IMediator _mediator;
        public QueriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected async Task<TResponse> Query<TResponse>(IRequest<TResponse> query)
              where TResponse : class
        {
            var response = await _mediator.Send(query);
            return response;
        }
    }
}
