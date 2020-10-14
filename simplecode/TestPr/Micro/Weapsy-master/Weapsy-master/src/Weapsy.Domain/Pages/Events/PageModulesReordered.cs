﻿using System;
using System.Collections.Generic;
using Weapsy.Cqrs.Domain;
using Weapsy.Framework.Domain;

namespace Weapsy.Domain.Pages.Events
{
    public class PageModulesReordered : DomainEvent
    {
        public Guid SiteId { get; set; }
        public IList<PageModule> PageModules { get; set; }

        public class PageModule
        {
            public Guid PageModuleId { get; set; }
            public string Zone { get; set; }
            public int SortOrder { get; set; }
        }
    }
}
