using Authorize.Application.Features.Applications.Queries.Models;
using System.Linq;

namespace Authorize.Application.Features.Applications.Queries.Search
{
    internal static class ApplicationMapper
    {
        public static ApplicationVM ToMap(this Domain.Applications.Application application)
            => new ApplicationVM(application.Name, application.Description, application.IsEnabled);

        public static IQueryable<ApplicationVM> ToMap(this IQueryable<Domain.Applications.Application> applicationQuery, int? pageSize, int? page)
        {
            if (pageSize.HasValue && page.HasValue)
            {
                var pageNotNull = page.Value;
                var pageSizeNotNull = pageSize.Value;
                applicationQuery = applicationQuery.Skip(pageNotNull * pageSizeNotNull).Take(pageSizeNotNull);
            }
            return applicationQuery.OrderBy(ap => ap.Name).Select(x => x.ToMap());
        }
    }
}