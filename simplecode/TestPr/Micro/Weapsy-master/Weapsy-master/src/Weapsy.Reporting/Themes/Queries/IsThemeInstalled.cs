﻿using Weapsy.Cqrs.Queries;

namespace Weapsy.Reporting.Themes.Queries
{
    public class IsThemeInstalled : IQuery
    {
        public string Name { get; set; }
    }
}
