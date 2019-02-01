using System;
using System.Collections.Generic;
using System.Text;
using HS.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HS.Data.Providers
{
    public class SQLiteDataProvider : IDataProvider
    {
        public DataProvider Provider => DataProvider.SQLite;

        public HSDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HSDbContext>();
            optionsBuilder.UseSqlite<HSDbContext>(connectionString);
            return new HSDbContext(optionsBuilder.Options);
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<HSDbContext>(d => d.UseSqlite(connectionString));
        }
    }
}
