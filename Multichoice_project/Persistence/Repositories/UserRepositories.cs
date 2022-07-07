using Microsoft.EntityFrameworkCore;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.GenericRepositories;

namespace Multichoice_project.Persistence.Repositories
{
    public class UserRepositories : Repositories<User>, IUserRepositories
    {
        public UserRepositories(Multichoise_DBContext dbContext) : base(dbContext)
        {
        }
        public void SwitchRole(int id)
        {
            User user = this.GetByID(id);

            Console.WriteLine("------SwitchRole------");

            if (user != null)
            {
                if (String.Compare(user.RoleName,"Admin",true)==0)
                {
                    user.RoleName = "User";
                    
                }
                else
                {
                    user.RoleName = "Admin";

                }
                this.Update(user);
            }
        }


    }
}
