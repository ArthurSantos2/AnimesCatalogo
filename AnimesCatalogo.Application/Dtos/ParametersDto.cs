using System.ComponentModel;

namespace Application.Dtos
{
    public sealed class ParametersDto
    {
        [DefaultValue("Pokemon")]
        public string? Nome { get; set; }
        [DefaultValue("Anonimo")]
        public string? Diretor {  get; set; }
        [DefaultValue("Aventura")]
        public string? PalavraChave { get; set; }
        public int QuantidadePaginas { get; set; }
        public int NumeroPagina { get; set; }
    }
}
