using Microsoft.AspNetCore.Http;
using System.IO;

namespace FileSharer.Business.Services.Interfaces
{
    public interface IFileStorageService
    {
        void Upload(IFormFile file);

        Stream Download(string fileName, int id, out string contentType);
    }
}
