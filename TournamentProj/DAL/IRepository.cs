using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TournamentProj.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, //Filter function
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, //Order by function
            string includeProperties);
        public TEntity GetById(object id);

        public void Insert(TEntity entity);
        public void Delete(object id);

        public void Delete(TEntity entityToDelete);

        public void Update(TEntity entityToUpdate);
    }
}