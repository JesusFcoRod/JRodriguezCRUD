using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Doctor
    {
        public static ML.Result Add(ML.Doctor doctor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[DOCTORADD] '{doctor.Nombre}','{doctor.AP}','{doctor.AM}',{doctor.Edad},{doctor.Especialidad.IdEspecialidad}");
                    if (query != null)
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

        public static ML.Result Update(ML.Doctor doctor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[DoctorUpdate] {doctor.IdDoctor},'{doctor.Nombre}','{doctor.AP}','{doctor.AM}',{doctor.Edad},{doctor.Especialidad.IdEspecialidad}");
                    if (query != null)
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

        public static ML.Result Delete(int IdDoctor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[DoctorDelete] {IdDoctor}");
                    if (query != null)
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
                    var query = contex.Doctors.FromSqlRaw("[DoctorGetAll]").ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            ML.Doctor doctor = new ML.Doctor();

                            doctor.IdDoctor = item.IdDoctor;
                            doctor.NombreCompleto = item.Nombre + " " + item.Ap + " " + item.Am;
                            doctor.Edad = item.Edad.Value;

                            doctor.Especialidad = new ML.Especialidad();
                            doctor.Especialidad.IdEspecialidad = item.IdEspecialidad.Value;
                            doctor.Especialidad.EspecialidadNombre = item.EspecialidadNombre;

                            result.Objects.Add(doctor);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.Ex = e;
                result.ErrorMessage = e.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetById(int IdDoctor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Doctors.FromSqlRaw($" [DoctorGetById] {IdDoctor}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Doctor doctor = new ML.Doctor();
                        doctor.Nombre = query.Nombre;
                        doctor.AP = query.Ap;
                        doctor.AM = query.Am;
                        doctor.Edad = query.Edad.Value;
                        doctor.Especialidad = new ML.Especialidad();
                        doctor.Especialidad.IdEspecialidad = query.IdEspecialidad.Value;
                        doctor.Especialidad.EspecialidadNombre = query.EspecialidadNombre;

                        result.Object = doctor;
                        result.Correct = true;

                    }
                }
            }
            catch (Exception e)
            {
                result.Ex = e;
                result.ErrorMessage = e.Message;
                result.Correct = false;
            }
            return result;
        }
    }
}
