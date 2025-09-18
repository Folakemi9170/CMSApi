using CMSApi.Domain.Entities;
using System.Linq.Expressions;

namespace CMSApi.Application.Interfaces
{ 
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllByCondition(Expression<Func<T, bool>>? expression = null, List<string>? includes = null);
        T Get(long id);
        int Insert(T entity);
        void InsertRange(List<T> entity);
        void Update(T entity);
        void UpdateRange(List<T> entity);
        bool Delete(long id);
        bool DeleteAll(List<T> entity);
        bool Remove(long Id);
    }

}