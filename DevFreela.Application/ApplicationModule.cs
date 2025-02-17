using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHandler();
            return services;
        }
        private static IServiceCollection AddHandler(this IServiceCollection services)
        {
            services.AddMediatR(config => 
            config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());

            services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining<GetAllProjectsQuery>());

            services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertCommandBehavior>();
            return services;
        }
    }
}
