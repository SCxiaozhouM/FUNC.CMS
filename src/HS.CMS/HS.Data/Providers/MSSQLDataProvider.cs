using System;
using System.Collections.Generic;
using System.Text;
using HS.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HS.Data.Providers
{
    public class MSSQLDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.MSSQL;

        public HSDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HSDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new HSDbContext(optionsBuilder.Options);
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {

            services.AddDbContext<HSDbContext>(d=>d.UseSqlServer(connectionString));
            return services;
        }
    }
}
