using Multichoice_project.Models;
using Multichoice_project.GenericRepositories;

namespace Multichoice_project.Core.Repositories
{
    public interface IUserRepositories : IRepositories<User> 
    {

        public void SwitchRole(int id);
    }
}
