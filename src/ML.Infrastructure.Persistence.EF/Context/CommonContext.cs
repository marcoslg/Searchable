using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ML.Infrastructure.Persistence.EF.Context
{
    public class CommonContext : DbContext
    {
        public CommonContext(
            DbContextOptions options)
            : base(options)
        {

        }


    }
}
