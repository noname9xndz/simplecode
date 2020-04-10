
using ModuleApp.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace ModuleApp.Module.Core.Models.MVC
{
    public class Media : EntityBase
    {
        [StringLength(450)]
        public string Caption { get; set; }

        public int FileSize { get; set; }

        [StringLength(450)]
        public string FileName { get; set; }

        public MediaType MediaType { get; set; }
    }
}