using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Paciente
    {
        public static ML.Result Add(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[PacienteAdd] '{paciente.Nombre}','{paciente.ApellidoPaterno}','{paciente.ApellidoMaterno}',{paciente.Edad},'{paciente.NSS}','{paciente.FechaIngreso}','{paciente.Padecimiento}',{paciente.Doctor.IdDoctor}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result Update(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[PacienteUpdate] {paciente.IdPaciente},'{paciente.Nombre}','{paciente.ApellidoPaterno}','{paciente.ApellidoMaterno}',{paciente.Edad},'{paciente.NSS}','{paciente.FechaIngreso}','{paciente.Padecimiento}',{paciente.Doctor.IdDoctor}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result Delete(int IdPaciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[PacienteDelete] {IdPaciente}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Pacientes.FromSqlRaw("[PacienteGetAll]").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Paciente paciente = new ML.Paciente();

                            paciente.IdPaciente = item.IdPaciente;
                            paciente.NombreCompleto = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
                            paciente.Edad = item.Edad.Value;
                            paciente.NSS = item.Nss;
                            paciente.FechaIngreso = item.FechaIngreso.Value.ToString("dd/MM/yyyy");
                            paciente.Padecimiento = item.Padecimiento;

                            paciente.Doctor = new ML.Doctor();
                            paciente.Doctor.IdDoctor = item.IdDoctor.Value;
                            paciente.Doctor.Nombre = item.NombreDoctor;

                            result.Objects.Add(paciente);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetById(int IdPaciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Pacientes.FromSqlRaw($"[PacienteGetById] {IdPaciente}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Paciente paciente = new ML.Paciente();

                        paciente.IdPaciente = query.IdPaciente;
                        paciente.Nombre = query.Nombre;
                        paciente.ApellidoPaterno = query.ApellidoPaterno;
                        paciente.ApellidoMaterno = query.ApellidoMaterno;
                        paciente.Edad = query.Edad.Value;
                        paciente.NSS = query.Nss;
                        paciente.FechaIngreso = query.FechaIngreso.Value.ToString("dd/MM/yyyy");
                        paciente.Padecimiento = query.Padecimiento;

                        paciente.Doctor = new ML.Doctor();
                        paciente.Doctor.IdDoctor = query.IdDoctor.Value;
                        paciente.Doctor.Nombre = query.NombreDoctor;

                        result.Object = paciente;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
    }
}
