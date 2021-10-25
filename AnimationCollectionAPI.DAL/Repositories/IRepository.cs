using AnimationCollectionAPI.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationCollectionAPI.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        bool Exists(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
    }
}
