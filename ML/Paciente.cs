using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int Edad { get; set; }
        public string NSS { get; set; }
        public string FechaIngreso { get; set; }
        public string Padecimiento { get; set; }

        public string NombreCompleto { get; set; }
        public ML.Doctor Doctor { get; set; }
        public List<object> Pacientes { get; set; }
    }
}
