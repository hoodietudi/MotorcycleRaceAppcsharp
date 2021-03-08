using System.Collections.Generic;
using MotorcycleContest.model;

namespace MotorcycleContest.repository.interfaces
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