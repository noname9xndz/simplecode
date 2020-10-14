﻿using System;
using System.Linq;
using Microsoft.Extensions.Options;
using Weapsy.Cqrs.Dependencies;
using Weapsy.Data.Configuration;

namespace Weapsy.Data
{
    public class ContextFactory : IContextFactory
    {
        private Configuration.Data DataConfiguration { get; }
        private ConnectionStrings ConnectionStrings { get; }
        private readonly IResolver _resolver;

        public ContextFactory(IOptions<Configuration.Data> dataOptions,
            IResolver resolver, IOptions<ConnectionStrings> connectionStringsOption)
        {
            DataConfiguration = dataOptions.Value;
            ConnectionStrings = connectionStringsOption.Value;
            _resolver = resolver;
        }

        public WeapsyDbContext Create()
        {
            var dataProvider = _resolver.ResolveAll<IDataProvider>().SingleOrDefault(x => x.Provider == DataConfiguration.Provider);

            if (dataProvider == null)
                throw new Exception("The Data Provider entry in appsettings.json is empty or the one specified has not been found!");

            return dataProvider.CreateDbContext(ConnectionStrings.DefaultConnection);
        }
    }
}
