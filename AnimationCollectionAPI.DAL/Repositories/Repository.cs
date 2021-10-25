using AnimationCollectionAPI.DAL;
using AnimationCollectionAPI.DAL.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationCollectionAPI.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly AnimationCollectionDbContext _context;

        public Repository(AnimationCollectionDbContext context)
        {
            _context = context;
        }
        public void Create(TEntity entity)
        {
             _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            // get list in desc order by id
            return _context.Set<TEntity>().OrderBy(e => e.Title);
        }

        public TEntity GetById(int id)
        {
            return  _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }
        // check if that id exists
        public bool Exists(int id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }

    }
}
