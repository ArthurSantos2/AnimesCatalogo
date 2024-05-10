using Domain.SeedWork;

namespace Domain.Entities
{
    public sealed class Anime : BaseEntity
    {
        public long Id { get; private set; }
        public string AnimeName { get; private set; }
        public string? Description { get; private set; }
        public string DirectorName { get; private set; } 
        public bool IsActive { get; private set; }

        protected Anime() { }

        public Anime(string animeName, string description, string directorName) 
        {
            AnimeName = animeName;
            Description = description;
            DirectorName = directorName;
            IsActive = true;
        }

        public void DesactiveAnime()
        {
            IsActive = false;
        }
    }
}
