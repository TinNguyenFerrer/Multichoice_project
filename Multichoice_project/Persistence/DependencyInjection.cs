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
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAnswerRepositories, AnswerRepositories>();
            services.AddTransient<IEducationalFieldRepositories, EducationalFieldRepositories>();
            services.AddTransient<IQuestionRepositories, QuestionRepositories>();
            services.AddTransient<IQuestionTypeRepositories, QuestionTypeRepositories>();
            services.AddTransient<IResultRepositories, ResultRepositories>();
            services.AddTransient<ISubjectRepositories, SubjectRepositories>();
            services.AddTransient<ITestRepositories, TestRepositories>();
            services.AddTransient<IUserRepositories, UserRepositories>();
            return services;
        }
    }
}
