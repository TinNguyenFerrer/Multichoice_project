using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.Persistence.Repositories;

namespace Multichoice_project.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public Multichoise_DBContext dbcontext { get; set; }
        public IAnswerRepositories AnswerRepository { get; }
        public IEducationalFieldRepositories EducationalFieldRepository { get; }
        public IQuestionRepositories QuestionRepository { get; }
        public IQuestionTypeRepositories QuestionTypeRepository { get; }
        public IResultRepositories ResultRepository { get; }
        public ISubjectRepositories SubjectRepository { get; }
        public ITestRepositories TestRepository { get; }
        public IUserRepositories UserRepository { get; }

        public int SaveChange();
    }
}
