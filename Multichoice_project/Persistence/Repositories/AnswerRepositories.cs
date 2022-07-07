using Microsoft.EntityFrameworkCore;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.GenericRepositories;

namespace Multichoice_project.Persistence.Repositories
{
    public class AnswerRepositories : Repositories<Answer>, IAnswerRepositories
    {
        public AnswerRepositories(Multichoise_DBContext dbContext) : base(dbContext)
        {
        }
        public bool IsRightAnswer(int Id)
        {
            return this.GetByID(Id).IsCorrectAnswer;
        }
    }
}
