﻿using Weapsy.Cqrs.Queries;

namespace Weapsy.Reporting.Apps.Queries
{
    public class IsAppInstalled : IQuery
    {
        public string Name { get; set; }
    }
}
