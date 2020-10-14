﻿using Weapsy.Cqrs.Domain;
using Weapsy.Framework.Domain;

namespace Weapsy.Domain.ModuleTypes.Events
{
    public class ModuleTypeDetailsUpdated : DomainEvent
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ViewType ViewType { get; set; }
        public string ViewName { get; set; }
        public EditType EditType { get; set; }
        public string EditUrl { get; set; }
    }
}
