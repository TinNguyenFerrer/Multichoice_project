using Multichoice_project.Models;
using Multichoice_project.Repositories;

namespace Multichoice_project.Core.Repositories
{
    public interface ITestRepositories : IRepositories<Test>
    {
        public IEnumerable<Test> GetListByIdUser(int iduser);
        public IEnumerable<Test> GetListByIdSubjectId(int idsubj);
        

      
    }
}
