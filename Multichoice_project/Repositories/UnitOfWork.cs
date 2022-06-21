using Multichoice_project.f;
using Multichoice_project.Models;
namespace Multichoice_project.Repositories
{
    public class UnitOfWork
    {
        public Multichoise_DBContext dbcontext { get; set; }
        public IRepositories<Answer> AnswerRepository { get; }
        public IRepositories<EducationalField> EducationalFieldRepository { get; }
        public IRepositories<Question> QuestionRepository { get; }
        public IRepositories<QuestionType> QuestionTypeRepository { get; }
        public IRepositories<Result> ResultRepository { get; }
        public IRepositories<Subject> SubjectRepository { get; }
        public IRepositories<Test> TestRepository { get; }
        public IRepositories<User> UserRepository { get; }

        public UnitOfWork(Multichoise_DBContext dbcontext,
                          IRepositories<Answer> AnswerRepository,
                          IRepositories<EducationalField> EducationalFieldRepository,
                          IRepositories<Question> QuestionRepository, 
                          IRepositories<QuestionType> QuestionTypeRepository,
                          IRepositories<Result> ResultRepository,
                          IRepositories<Subject> SubjectRepository,
                          IRepositories<Test> TestRepository,
                          IRepositories<User> UserRepository)
        {
            this.dbcontext = dbcontext;
            this.AnswerRepository = AnswerRepository;
            this.EducationalFieldRepository = EducationalFieldRepository;
            this.QuestionTypeRepository = QuestionTypeRepository;
            this.ResultRepository = ResultRepository;
            this.QuestionRepository = QuestionRepository;
            this.SubjectRepository = SubjectRepository;
            this.QuestionRepository = QuestionRepository;
            this.TestRepository = TestRepository;
            this.UserRepository = UserRepository;

            this.AnswerRepository.DbContext = this.dbcontext;
            this.EducationalFieldRepository.DbContext = this.dbcontext;
            this.QuestionTypeRepository.DbContext = this.dbcontext;
            this.ResultRepository.DbContext = this.dbcontext;
            this.QuestionRepository.DbContext = this.dbcontext;
            this.SubjectRepository.DbContext = this.dbcontext;
            this.QuestionRepository.DbContext = this.dbcontext;
            this.TestRepository.DbContext = this.dbcontext;
            this.UserRepository.DbContext = this.dbcontext;
        }
        public UnitOfWork(Multichoise_DBContext dbcontext)
        {
            this.dbcontext = dbcontext;
            this.AnswerRepository = new Repositories<Answer>(dbcontext);
            this.EducationalFieldRepository = new Repositories<EducationalField>(dbcontext);
            this.QuestionTypeRepository = new Repositories<QuestionType>(dbcontext);
            this.ResultRepository = new Repositories<Result>(dbcontext);
            this.QuestionRepository = new Repositories<Question>(dbcontext);
            this.SubjectRepository = new Repositories<Subject>(dbcontext);
            this.QuestionRepository = new Repositories<Question>(dbcontext);
            this.TestRepository = new Repositories<Test>(dbcontext);
            this.UserRepository = new Repositories<User>(dbcontext);

            this.AnswerRepository.DbContext = this.dbcontext;
            this.EducationalFieldRepository.DbContext = this.dbcontext;
            this.QuestionTypeRepository.DbContext = this.dbcontext;
            this.ResultRepository.DbContext = this.dbcontext;
            this.QuestionRepository.DbContext = this.dbcontext;
            this.SubjectRepository.DbContext = this.dbcontext;
            this.QuestionRepository.DbContext = this.dbcontext;
            this.TestRepository.DbContext = this.dbcontext;
            this.UserRepository.DbContext = this.dbcontext;
        }

        public void SaveChange()
        {
            dbcontext.SaveChanges();

        }
        public void Dispose()
        {
            dbcontext.Dispose();
        }
    }
}
