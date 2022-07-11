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
        public UnitOfWork(Multichoise_DBContext _DBContext, 
            IAnswerRepositories answerRepositories, 
            IEducationalFieldRepositories educationalFieldRepositories,
            IQuestionTypeRepositories questionTypeRepositories,
            IResultRepositories resultRepositories,
            IQuestionRepositories questionRepositories,
            ISubjectRepositories subjectRepositories,
            ITestRepositories testRepositories,
            IUserRepositories userRepositories)
        {
            this.dbcontext = _DBContext;
            this.AnswerRepository = answerRepositories;
            this.EducationalFieldRepository = educationalFieldRepositories;
            this.QuestionTypeRepository = questionTypeRepositories;
            this.ResultRepository = resultRepositories;
            this.QuestionRepository = questionRepositories;
            this.SubjectRepository = subjectRepositories;
            this.TestRepository = testRepositories;
            this.UserRepository = userRepositories;
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
