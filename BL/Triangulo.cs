using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Triangulo
    {
        public static ML.Result Add(ML.Triangulo triangulo)
        {
            ML.Result result = new ML.Result();

            triangulo.Perimetro = CalcularPerimetro(triangulo.Lado);
            triangulo.Area = CalcularArea(triangulo.Lado);

            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[TrianguloAdd] '{triangulo.Tipo}',{triangulo.Lado},{triangulo.Area},{triangulo.Perimetro}");
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

        public static ML.Result Update(ML.Triangulo triangulo)
        {
            ML.Result result = new ML.Result();

            triangulo.Perimetro = CalcularPerimetro(triangulo.Lado);
            triangulo.Area = CalcularArea(triangulo.Lado);

            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[TrianguloUpdate] {triangulo.IdTriangulo},'{triangulo.Tipo}',{triangulo.Lado},{triangulo.Area},{triangulo.Perimetro}");
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

        public static ML.Result Delete(int IdTriangulo)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[TrianguloDelete]");

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
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
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
                    var query = contex.Triangulos.FromSqlRaw("[TrianguloGetALL]").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Triangulo triangulo = new ML.Triangulo();

                            triangulo.IdTriangulo = item.IdTriangulo;
                            triangulo.Tipo = item.Tipo;
                            triangulo.Lado = item.Lado.Value;
                            triangulo.Perimetro = item.Perimetro.Value;
                            triangulo.Area = item.Area.Value;

                            result.Objects.Add(triangulo);
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

        public static ML.Result GetById(int IdTriangulo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezCrudContext contex = new DL.JrodriguezCrudContext())
                {
                    var query = contex.Triangulos.FromSqlRaw($"[TrianguloGetById] {IdTriangulo}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Triangulo triangulo = new ML.Triangulo();

                        triangulo.IdTriangulo = query.IdTriangulo;
                        triangulo.Tipo = query.Tipo;
                        triangulo.Lado = query.Lado.Value;
                        triangulo.Perimetro = query.Perimetro.Value;
                        triangulo.Area = query.Area.Value;

                        result.Object = triangulo;
                    }
                    result.Correct = true;
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

        public static decimal CalcularPerimetro(decimal lado)
        {
            decimal resultado = lado * 3;
            return resultado;
        }

        public static decimal CalcularArea(decimal lado)
        {
            decimal resultado = (lado * lado) / 2;
            return resultado;
        }
    }
}
