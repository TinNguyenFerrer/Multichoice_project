using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Multichoice_project.Models;
namespace Multichoice_project.Repositories
{
    public class Repositories<T> : IRepositories<T> where T : class
    {
        public DbSet<T> Dbset
        {
            get
            {
                return DbContext.Set<T>();
            }
        }

        public Multichoise_DBContext DbContext { get; set; }
        public Repositories(Multichoise_DBContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Delete(int ID)
        {
            T exist = Dbset.Find(ID);
            if (exist != null)
            {
                Dbset.Remove(exist);
            }

        }

        public IEnumerable<T> GetAll()
        {
            return Dbset.AsEnumerable();
        }

        public T GetByID(int id)
        {
            return Dbset.Find(id);
        }

        public void Insert(T TEntity)
        {
            Dbset.Add(TEntity);
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public void Update(T TEntity)
        {
            Dbset.Attach(TEntity);
            DbContext.Entry(TEntity).State = EntityState.Modified;
        }
    }
}

