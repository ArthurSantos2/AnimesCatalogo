using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    internal interface IDbConnectionFactory
    {
        string ConnectionString { get; }
    }
}
