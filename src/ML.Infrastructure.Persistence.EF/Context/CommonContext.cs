using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Common.Infrastructure.EF.Context
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
