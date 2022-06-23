using Microsoft.EntityFrameworkCore;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.Repositories;

namespace Multichoice_project.Persistence.Repositories
{
    public class AnswerRepositories : Repositories<Answer>, IAnswerRepositories
    {
        public AnswerRepositories(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
