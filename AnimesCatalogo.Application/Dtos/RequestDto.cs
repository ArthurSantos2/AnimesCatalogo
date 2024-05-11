using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public sealed class RequestDto
    {
        [DefaultValue("Pokemon")]
        public string nome { get; set; }
        [DefaultValue("Um anime de aventura e cooperação.")]
        public string descricao { get; set; }
        [DefaultValue("Anonimo")]
        public string diretor { get; set; }
    }
}
