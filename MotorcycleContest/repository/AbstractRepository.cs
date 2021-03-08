using System;
using System.Collections.Generic;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class AbstractRepository<TId, T> : IRepository<TId, T> where  T : Entity<TId>
    {
        protected IDictionary<TId, T> _entities;
        protected IValidator<T> _validator;

        public AbstractRepository(IValidator<T> validator)
        {
            _entities = new Dictionary<TId, T>();
            _validator = validator;
        }

        public int Size()
        {
            return _entities.Count;
        }

        public void Save(T entity)
        {
            try
            {
                _validator.Validate(entity);
            }
            catch (ValidationException)
            {
                Console.WriteLine("Entity " + entity + "is not valid!");
                throw;
            }

            var id = entity.Id;
            if (!_entities.ContainsKey(id))
            {
                _entities[id] = entity;
            }
            else
            {
                throw new RepositoryException("Id " + id + " already exists");
            }
        }

        public void Delete(TId id)
        {
            _entities.Remove(id);
            Console.WriteLine("Entity deleted " + id);
        }

        public void Update(TId id, T entity)
        {
            try
            {
                _validator.Validate(entity);
            }
            catch (ValidationException)
            {
                Console.WriteLine("Entity " + entity + "is not valid!");
                throw;
            }

            if (_entities.ContainsKey(id))
            {
                if (!id.Equals(entity.Id))
                    if (_entities[entity.Id] != null)
                    {
                        throw new RepositoryException("Id " + entity.Id + " already exists.");
                    }

                _entities[id] = entity;
                Console.WriteLine("Updated entity " + entity);
            }
            else
            {
                throw new RepositoryException("Id " + id + " does not exist.");
            }
        }

        public T FindOne(TId id)
        {
            var res = _entities[id];
            if (res != null)
            {
                return res;
            }
            
            throw new RepositoryException("Id not found " + id);
        }

        public IEnumerable<T> FindAll()
        {
            return _entities.Values;
        }
    }
}