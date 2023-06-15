using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Triangulo
    {
        public int IdTriangulo { get; set; }
        public string Tipo { get; set; }
        public decimal Lado { get; set; }
        public decimal Perimetro { get; set; }
        public decimal Area { get; set; }

        public List<object> Triangulos { get; set; }
    }
}
