﻿using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();
            services.AddHandler();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ISkillService, SkillService>();
            return services;
        }

        private static IServiceCollection AddHandler(this IServiceCollection services)
        {
            services.AddMediatR(config => 
            config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());

            services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining<GetAllProjectsQuery>());

            return services;
        }
    }
}
