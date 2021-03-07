using System.Collections.Generic;
using MotorcycleContest.domain;

namespace MotorcycleContest.repository
{
    public interface IRepository<in TId, T> where T : Entity<TId>
    {
        int Size();
        
        void Save(T entity);

        void Delete(TId id);

        void Update(TId id, T entity);

        T FindOne(TId id);

        IEnumerable<T> FindAll();
    }
}