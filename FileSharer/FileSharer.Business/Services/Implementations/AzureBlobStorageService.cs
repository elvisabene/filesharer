using Azure.Storage.Blobs;
using FileSharer.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FileSharer.Business.Services.Implementations
{
    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobService;

        public AzureBlobStorageService(BlobServiceClient blobService)
        {
            _blobService = blobService;
        }

        public Stream Download(string fileName, int id, out string contentType)
        {
            var blobContainer = _blobService.GetBlobContainerClient("files");
            var blobs = blobContainer.GetBlobs();
            var blob = blobContainer.GetBlobClient(fileName);

            contentType = blob.GetProperties().Value.ContentType;

            var data = blob.DownloadContent().Value.Content.ToStream();

            return data;
        }

        public void Upload(IFormFile file)
        {
            var blobContainer = _blobService.GetBlobContainerClient("files");

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                ms.Position = 0;
                blobContainer.UploadBlob(file.FileName, ms);
            }
        }
    }
}
