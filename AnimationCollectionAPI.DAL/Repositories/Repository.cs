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
        public async Task CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }
        // check if that id exists
        public bool Exists(int id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().OrderByDescending(e => e.Id).ToListAsync();
        }
    }
}
