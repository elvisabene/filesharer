using Azure.Storage.Blobs;
using FileSharer.Business.Services.Implementations;
using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Data.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileSharer.Business.Dependency
{
    public static class BllDependency
    {
        public static IServiceCollection AddBll(this IServiceCollection services, IConfigurationSection connectionStringsSection)
        {
            services.AddDal(connectionStringsSection.GetSection("DefaultConnection").Value);

            services.AddSingleton(new BlobServiceClient(
                connectionStringsSection.GetSection("AzureBlobStorageConnection").Value));
            services.AddSingleton<IFileStorageService, AzureBlobStorageService>();

            services.AddScoped<IFileCategoryService, FileCategoryService>();
            services.AddScoped<IService<FileExtension>, FileExtensionService>();
            services.AddScoped<IFileItemService, FileItemService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
