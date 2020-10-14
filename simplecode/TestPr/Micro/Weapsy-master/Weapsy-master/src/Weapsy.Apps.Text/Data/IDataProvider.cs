﻿using Microsoft.Extensions.DependencyInjection;
using Weapsy.Data.Configuration;

namespace Weapsy.Apps.Text.Data
{
    public interface IDataProvider
    {
        DataProvider Provider { get; }
        IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString);
        TextDbContext CreateDbContext(string connectionString);
    }
}
