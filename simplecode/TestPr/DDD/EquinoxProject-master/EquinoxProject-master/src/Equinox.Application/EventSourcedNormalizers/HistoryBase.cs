using System;
using System.Collections.Generic;
using System.Text;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class HistoryBase
    {
        public string Action { get; set; }
        public string Id { get; set; }

        public string Timestamp { get; set; }
        public string Who { get; set; }
    }
}
