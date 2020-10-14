﻿using Weapsy.Cqrs.Events;
using Weapsy.Data.Caching;
using Weapsy.Domain.ModuleTypes.Events;

namespace Weapsy.Data.Reporting.Apps
{
    public class ModuleTypeEventsHandler : 
        IEventHandler<ModuleTypeCreated>,
        IEventHandler<ModuleTypeDetailsUpdated>
    {
        private readonly ICacheManager _cacheManager;

        public ModuleTypeEventsHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void Handle(ModuleTypeCreated @event)
        {
            ClearCache();
        }

        public void Handle(ModuleTypeDetailsUpdated @event)
        {
            ClearCache();
        }

        private void ClearCache()
        {
             _cacheManager.Remove(CacheKeys.ModuleTypesCacheKey);
        }
    }
}
