using Multichoice_project.Core;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.Persistence.Repositories;

namespace Multichoice_project.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public Multichoise_DBContext dbcontext { get; set ; }

        public IAnswerRepositories AnswerRepository { get; set; }

        public IEducationalFieldRepositories EducationalFieldRepository { get; set; }

        public IQuestionRepositories QuestionRepository { get; set; }

        public IQuestionTypeRepositories QuestionTypeRepository { get; set; }

        public IResultRepositories ResultRepository { get; set; }

        public ISubjectRepositories SubjectRepository { get; set; }

        public ITestRepositories TestRepository { get; set; }

        public IUserRepositories UserRepository { get; set; }
        public UnitOfWork(Multichoise_DBContext _DBContext)
        {
            dbcontext = _DBContext;
            this.AnswerRepository = new AnswerRepositories(dbcontext);
            this.EducationalFieldRepository = new EducationalFieldRepositories(dbcontext);
            this.QuestionTypeRepository = new QuestionTypeRepositories(dbcontext);
            this.ResultRepository = new ResultRepositories(dbcontext);
            this.QuestionRepository = new QuestionRepositories(dbcontext);
            this.SubjectRepository = new SubjectRepositories(dbcontext);
            this.TestRepository = new TestRepositories(dbcontext);
            this.UserRepository = new UserRepositories(dbcontext);
        }

        public void Dispose()
        {
            dbcontext.Dispose();
        }

        public int SaveChange()
        {
            return dbcontext.SaveChanges();
        }
    }
}
