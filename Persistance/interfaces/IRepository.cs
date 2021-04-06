using System.Collections.Generic;
using Domain.model;

namespace Persistance.interfaces
{
    public interface IRepository<in TId, T> where T : Entity<TId>
    {
        int Size();
        
        void Save(T entity);

        void Delete(TId id);

        void Update(TId id, T entity);

        T FindOne(TId id);

        List<T> FindAll();
    }
}