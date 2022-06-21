using Microsoft.EntityFrameworkCore;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.Repositories;

namespace Multichoice_project.Persistence.Repositories
{
    public class UserRepositories :Repositories<User>, IUserRepositories
    {
        public UserRepositories(Multichoise_DBContext dbContext) : base(dbContext)
        {
        }

        
    }
}
