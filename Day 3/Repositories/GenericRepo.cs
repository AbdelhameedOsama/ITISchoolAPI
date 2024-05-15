using Day_3.Models;
using Microsoft.EntityFrameworkCore;

namespace Day_3.Repositories
{
	public class GenericRepo<TEntity> where TEntity : class 
	{
		ITI_DBContext db;
        public GenericRepo(ITI_DBContext db)
        {
            this.db =db;
        }
        public List<TEntity> getAll()
        {
            return db.Set<TEntity>().ToList();
		}
        public TEntity getById(int id)
        {
            return db.Set<TEntity>().Find(id);
        }
        public void add(TEntity entity)
        {
			db.Set<TEntity>().Add(entity);
		}
        public void update(TEntity entity)
        {
			db.Set<TEntity>().Update(entity);
		}
		public void delete(int id)
        {
			db.Set<TEntity>().Remove(getById(id));
		}
        
    }
}
