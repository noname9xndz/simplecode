using System;
using System.Collections.Generic;
using System.Text;
using Order.Infrastructure.Models.Base;

namespace Order.Infrastructure.Extensions
{
    public static class CustomProgramExtension
    {
        public static readonly string Namespace = typeof(OrderProgram).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

    }
}
