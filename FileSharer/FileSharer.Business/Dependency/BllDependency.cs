﻿using FileSharer.Business.Services.Implementations;
using FileSharer.Business.Services.Interfaces;
using FileSharer.Common.Entities;
using FileSharer.Data.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace FileSharer.Business.Dependency
{
    public static class BllDependency
    {
        public static IServiceCollection AddBll(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDal(dbConnectionString);

            services.AddScoped<IFileCategoryService, FileCategoryService>();
            services.AddScoped<IService<FileExtension>, FileExtensionService>();
            services.AddScoped<IFileItemService, FileItemService>();
            services.AddScoped<IService<Role>, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
