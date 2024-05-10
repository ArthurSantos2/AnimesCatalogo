
using System.ComponentModel;

namespace Application.Dtos
{
    public class AnimeDto
    {
        [DefaultValue("Pokemon")]
        public string nome { get; set; }
        [DefaultValue("Um anime de aventura e cooperação.")]
        public string descricao { get; set; }
        [DefaultValue("Anonimo")]
        public string diretor { get; set; }
    };
}
