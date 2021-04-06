using System;

namespace Domain.model
{
    [Serializable]
    public class Entity<TId>
    {
        
        public TId Id { get; set; }
    }
}