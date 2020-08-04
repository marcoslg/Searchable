using Authorize.Application.Features.Applications.Queries.Models;
using MediatR;
using ML.DataStructure.Collections.DTO;

namespace Authorize.Application.Features.Applications.Queries.Search.Models
{

    public class SearchAppQuery : ML.DataStructure.Linq.Entities.Search, IRequest<PagedListDTO<ApplicationVM>>
    {
        
    }
}
