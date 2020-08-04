
using Authorize.Application.Features.Applications.Queries.Models;
using Authorize.Application.Features.Applications.Queries.Search.Models;
using MediatR;
using ML.Data.Contracts.Respositories;
using ML.DataStructure.Collections.DTO;
using ML.DataStructure.Linq;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authorize.Application.Features.Applications.Queries.Search
{
    class SearchAppQueryHandler : IRequestHandler<SearchAppQuery, PagedListDTO<ApplicationVM>>
    {
        private readonly IQueryRepository<Domain.Applications.Application> _repository;
        public SearchAppQueryHandler(IQueryRepository<Domain.Applications.Application> context)
        {
            _repository = context;
        }
        public async Task<PagedListDTO<ApplicationVM>> Handle(SearchAppQuery search, CancellationToken cancellationToken)
        {
            Func<IQueryable<Domain.Applications.Application>, IOrderedQueryable<Domain.Applications.Application>> orderBy;            
            if (search.SortDescriptors != null && search.SortDescriptors.Any())
            {
                orderBy = ExpressionsBuilder.GetOrderBy<Domain.Applications.Application>(search.SortDescriptors[0].Member,
                    search.SortDescriptors[0].SortDirection == ListSortDirection.Ascending);
            }
            else
            {
                orderBy = contacts => contacts.OrderBy(a => a.Name);
            }

            var filterBy = ExpressionsBuilder.GetFilter<Domain.Applications.Application>(search);
            if (!search.PageSize.HasValue)
            {
                search.PageSize = int.MaxValue;
            }
            if (!search.Page.HasValue)
            {
                search.Page = 0;
            }
            var pagelist = _repository.Get(filterBy, search.Page.Value, search.PageSize.Value, orderBy);
            var result = new PagedListDTO<ApplicationVM>()
            {
                Rows = pagelist.Select(a => a.ToMap()).ToList(),
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                TotalCount = pagelist.TotalCount,
                TotalPages = pagelist.TotalPages               
                    

            };
            return result;
        }
      
    }
}
