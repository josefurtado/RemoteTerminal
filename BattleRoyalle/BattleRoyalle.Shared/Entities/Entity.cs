using System;

namespace BattleRoyalle.Shared.Entities
{
    public abstract class Entity
    {

        public Entity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
