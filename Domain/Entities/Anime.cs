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

        public Anime(string animeName, string description) 
        {
            AnimeName = animeName;
            Description = description;
        }

        public void DesactiveAnime()
        {
            IsActive = false;
        }
    }
}
