using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHandler();
            services.AddValidation();
            return services;
        }
        private static IServiceCollection AddHandler(this IServiceCollection services)
        {
            services.AddMediatR(config => 
            config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());

            services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertCommandBehavior>();
            return services;
        }

        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<InsertProjectCommand>();
            return services;
        }
    }
}
