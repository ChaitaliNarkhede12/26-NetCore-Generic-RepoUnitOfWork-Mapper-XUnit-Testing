using Microsoft.EntityFrameworkCore;
using QSS.TCCS.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QSS.TCCS.DataAccess.Repositories
{
    public class Repository<TEntity, TType> : IRepository<TEntity, TType> where TEntity : class
    {
        protected IUnitOfWork _unitOfWork;

        protected DbSet<TEntity> _entities { get; }
        protected DbContext _context;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            if (_unitOfWork.Context != null)
            {
                _context = _unitOfWork.Context;
                _entities = _context.Set<TEntity>();
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetById(TType id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetById(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _entities.AddAsync(entity);
            return result.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return _entities.Update(entity).Entity;
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public async Task RemoveById(TType id)
        {
            var entity = await GetById(id);
            Remove(entity);
        }

        public int SaveChanges()
        {
            return _unitOfWork.Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.Context.SaveChangesAsync();
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.FirstOrDefaultAsync(predicate);
        }
       

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
        } 


        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }


        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }


        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> query = null;
            foreach (var includeExpression in includeExpressions)
            {
                query = _entities.Include(includeExpression);
            }

            return query ?? _entities;
        }

        public Task<bool> CanSafeToRemove(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
