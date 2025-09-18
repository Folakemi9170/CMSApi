using CMSApi.Application.Interfaces;
using CMSApi.Domain.Entities;
using CMSApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CMSApi.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CMSDbContext _dbContext;
        private DbSet<T> entities;

        public GenericRepository(CMSDbContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<T>();
        }



        public T Get(long id)
        {
            var entity = entities!.SingleOrDefault(x => x.Id == id);

            if (entity != null)
            {
                return entity;
            }

            return null;
            // return entities.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<T> GetAllByCondition(Expression<Func<T, bool>>? expression = null, List<string>? includes = null)
        {
            IQueryable<T> query = entities;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            var result = query.AsNoTracking().AsEnumerable();
            return result;

        }


        public void InsertRange(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddRange(entity);
            _dbContext.SaveChanges();
        }

        public int Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            int count = _dbContext.SaveChanges();
            return count;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _dbContext.SaveChanges();
        }

        public void UpdateRange(List<T> entity)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _dbContext.UpdateRange(entity);
            _dbContext.SaveChanges();
        }

        public bool Delete(long id)
        {
            var entity = entities!.SingleOrDefault(x => x.Id == id);
            if (entity != null)
            {
                entities.Remove(entity);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }


        public bool DeleteAll(List<T> entity)
        {

            if (entity == null)
            {
                return false;
            }
            _dbContext.RemoveRange(0, entity.Count);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Remove(long Id)
        {
            var entity = entities!.SingleOrDefault(x => x.Id == Id);
            if (entity == null)
            {
                return false;
            }
            _dbContext.Remove(entity);
            return true;
        }
    }
}