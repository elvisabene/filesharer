using FileSharer.Data.Database;
using FileSharer.Data.Repositories.Implementations;
using FileSharer.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FileSharer.Data.Dependency
{
    public static class DalDependency
    {
        public static IServiceCollection AddDal(this IServiceCollection services, string dbConnectionString)
        {
            services.AddSingleton<IDatabaseSettings>(new DatabaseSettings(dbConnectionString));
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IFileItemRepository, FileItemRepository>();

            return services;
        }
    }
}
