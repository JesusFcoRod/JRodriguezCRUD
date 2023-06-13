using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Especialidad
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Especialidads.FromSqlRaw("[EspecialidadGetAll]").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Especialidad especialidad = new ML.Especialidad();

                            especialidad.IdEspecialidad = item.IdEspecialidad;
                            especialidad.EspecialidadNombre = item.Nombre;

                            result.Objects.Add(especialidad);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}