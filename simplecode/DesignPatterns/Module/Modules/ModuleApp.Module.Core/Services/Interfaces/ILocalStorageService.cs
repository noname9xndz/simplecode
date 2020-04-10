using System.IO;
using System.Threading.Tasks;

namespace ModuleApp.Module.Core.Services.Interfaces
{
    public interface ILocalStorageService
    {
        string GetMediaUrl(string fileName);

        Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null);

        Task DeleteMediaAsync(string fileName);
    }
}