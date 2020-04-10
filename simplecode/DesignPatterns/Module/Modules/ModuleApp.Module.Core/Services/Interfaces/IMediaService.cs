using ModuleApp.Module.Core.Models.MVC;
using System.IO;
using System.Threading.Tasks;

namespace ModuleApp.Module.Core.Services.Interfaces
{
    public interface IMediaService
    {
        string GetMediaUrl(Media media);

        string GetMediaUrl(string fileName);

        string GetThumbnailUrl(Media media);

        Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null);

        Task DeleteMediaAsync(Media media);

        Task DeleteMediaAsync(string fileName);
    }
}