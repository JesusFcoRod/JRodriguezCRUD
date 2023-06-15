using System;
using System.Collections.Generic;

namespace DL;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public int? Edad { get; set; }

    public string? Nss { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? Padecimiento { get; set; }

    public int? IdDoctor { get; set; }

    public virtual Doctor? IdDoctorNavigation { get; set; }

    public string NombreDoctor { get; set; }
}
