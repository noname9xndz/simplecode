using ModuleApp.Module.News.Models;
using System.Threading.Tasks;

namespace ModuleApp.Module.News.Services.Interfaces
{
    public interface INewsCategoryService
    {
        Task Create(NewsCategory category);

        Task Update(NewsCategory category);

        Task Delete(long id);

        Task Delete(NewsCategory category);
    }
}