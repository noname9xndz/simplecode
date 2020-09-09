using System;
using System.Collections.Generic;
using System.Text;

namespace TestMicro.Infrastructure.Models
{
    public class JsonErrorResponse
    {
        public string[] Messages { get; set; }

        public object DeveloperMessage { get; set; }
    }
}
