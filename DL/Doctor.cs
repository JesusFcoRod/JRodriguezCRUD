using System;
using System.Collections.Generic;

namespace DL;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string? Nombre { get; set; }

    public string? Ap { get; set; }

    public string? Am { get; set; }

    public int? Edad { get; set; }

    public int? IdEspecialidad { get; set; }

    public string EspecialidadNombre { get; set; }

    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
