using FileSharer.Common.Entities;
using FileSharer.Data.Database;
using FileSharer.Data.DataConverters;
using FileSharer.Data.Infrastructure;
using FileSharer.Data.Repositories.Implementations;
using FileSharer.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FileSharer.Data.Dependency
{
    public static class DalDependency
    {
        public static IServiceCollection AddDal(this IServiceCollection services, string dbConnectionString)
        {
            services.AddScoped<IDatabaseSettings>(provider => new DatabaseSettings(dbConnectionString));
            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped<IRepository<FileCategory>, FileCategoryRepository>();
            services.AddScoped<IFileItemRepository, FileItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IDataConverter<FileCategory>, FileCategoryConverter>();
            services.AddSingleton<IDataConverter<FileItem>, FileItemConverter>();
            services.AddSingleton<IDataConverter<User>, UserConverter>();

            return services;
        }
    }
}
