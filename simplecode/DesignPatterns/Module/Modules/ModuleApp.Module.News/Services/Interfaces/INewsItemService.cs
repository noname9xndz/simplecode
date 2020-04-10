using ModuleApp.Module.News.Models;
using System.Threading.Tasks;

namespace ModuleApp.Module.News.Services.Interfaces
{
    public interface INewsItemService
    {
        void Create(NewsItem newsItem);

        void Update(NewsItem newsItem);

        Task Delete(long id);

        Task Delete(NewsItem newsItem);
    }
}