using Microsoft.EntityFrameworkCore;

namespace ModuleApp.Infrastructure.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}