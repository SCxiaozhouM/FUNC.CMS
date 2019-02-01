using HS.Data.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Data
{
    public interface IDataProvider
    {
        DataProvider Provider { get; }

        IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString);

        HSDbContext CreateDbContext(string connectionString);
    }
}
