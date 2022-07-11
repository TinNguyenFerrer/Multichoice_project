using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Multichoice_project.Persistence;
using Multichoice_project.Core;
using Multichoice_project.Persistence.Repositories;
using Multichoice_project.Core.Repositories;

namespace Multichoice_project.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAnswerRepositories, AnswerRepositories>();
            services.AddScoped<IEducationalFieldRepositories, EducationalFieldRepositories>();
            services.AddScoped<IQuestionRepositories, QuestionRepositories>();
            services.AddScoped<IQuestionTypeRepositories, QuestionTypeRepositories>();
            services.AddScoped<IResultRepositories, ResultRepositories>();
            services.AddScoped<ISubjectRepositories, SubjectRepositories>();
            services.AddScoped<ITestRepositories, TestRepositories>();
            services.AddScoped<IUserRepositories, UserRepositories>();
            return services;
        }
    }
}
