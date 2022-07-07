using Multichoice_project.Models;
using Multichoice_project.GenericRepositories;

namespace Multichoice_project.Core.Repositories
{
    public interface IQuestionRepositories : IRepositories<Question>
    {
        public IEnumerable<Question> GetQuestionsAnswersByIdTest(int id);
    }
}
