using System;
using System.Collections.Generic;
using System.Text;
using HS.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HS.Data.Providers
{
    public class MySQLDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.MySQL;

        public HSDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HSDbContext>();
            optionsBuilder.UseMySQL(connectionString);

            return new HSDbContext(optionsBuilder.Options);
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HSDbContext>(d => d.UseMySQL(connectionString));
            return services;
        }
    }
}
