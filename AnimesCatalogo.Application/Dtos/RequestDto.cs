using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public sealed class RequestDto
    {
        public string Nome {  get; set; }
        public string Diretor { get; set; }
        public string Resumo { get; set; }
    }
}
