﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServerCompact;

namespace Common.Infrastructure.EF.Context
{
    public class DbLocalConfiguration : DbConfiguration
    {
        public DbLocalConfiguration()
        {
            SetProviderServices(
              SqlCeProviderServices.ProviderInvariantName,
              SqlCeProviderServices.Instance
          );
            SetDefaultConnectionFactory(
                new SqlCeConnectionFactory(SqlCeProviderServices.ProviderInvariantName)
            );
        }
    }
}