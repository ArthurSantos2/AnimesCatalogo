using Domain.SeedWork;

namespace Domain.Entities
{
    public sealed class Anime : BaseEntity
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Director { get; private set; } 
        public bool IsActive { get; private set; }

        protected Anime() { }

        public Anime(string name, string description, string director) 
        {
            Name = name;
            Description = description;
            Director = director;
            IsActive = true;
        }

        public void DesactiveAnime()
        {
            IsActive = false;
        }
    }
}
