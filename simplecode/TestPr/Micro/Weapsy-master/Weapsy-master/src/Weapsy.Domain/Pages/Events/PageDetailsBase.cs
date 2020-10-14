﻿using System;
using System.Collections.Generic;
using Weapsy.Cqrs.Domain;

namespace Weapsy.Domain.Pages.Events
{
    public abstract class PageDetailsBase : DomainEvent
    {
        public Guid SiteId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public IEnumerable<PageLocalisation> PageLocalisations { get; set; } = new List<PageLocalisation>();
        public IEnumerable<PagePermission> PagePermissions { get; set; } = new List<PagePermission>();
        public IEnumerable<Guid> MenuIds { get; set; } = new List<Guid>();
    }
}