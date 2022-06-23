using Microsoft.EntityFrameworkCore;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.Repositories;

namespace Multichoice_project.Persistence.Repositories
{
    public class EducationalFieldRepositories : Repositories<EducationalField>, IEducationalFieldRepositories
    {
        public EducationalFieldRepositories(Multichoise_DBContext dbContext) : base(dbContext)
        {
        }
    }
}
