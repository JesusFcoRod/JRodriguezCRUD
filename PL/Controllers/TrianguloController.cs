using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class TrianguloController : Controller
    {
        [HttpGet]

        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Triangulo triangulo = new ML.Triangulo();

            result = BL.Triangulo.GetAll();

            if (result.Correct)
            {
                triangulo.Triangulos = result.Objects;
                return View(triangulo);
            }

            return View(triangulo);

        }

        [HttpGet]

        public ActionResult Form(int? IdTriangulo)
        {
            ML.Triangulo triangulo = new ML.Triangulo();

            if (IdTriangulo != null)
            {
                ML.Result result = BL.Triangulo.GetById(IdTriangulo.Value);

                if (result.Correct)
                {
                    triangulo = (ML.Triangulo)result.Object;
                    return View(triangulo);
                }
                else
                {
                    ViewBag.Message = "Error al consultar la iformacion del registro";
                    return PartialView("Modal");
                }
            }
            else
            {
                return View(triangulo);
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Triangulo triangulo)
        {
            ML.Result result = new ML.Result();

            if (triangulo.IdTriangulo == 0)
            {
                result = BL.Triangulo.Add(triangulo);
                if (result.Correct)
                {
                    ViewBag.Message = "Registro agregado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un problema al intenter agregar el registro";
                    return PartialView("Modal");
                }
            }
            else
            {
                result = BL.Triangulo.Update(triangulo);
                if (result.Correct)
                {
                    ViewBag.Message = "Registro actualizado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al querer actualizar el registro";
                    return PartialView("Modal");
                }
            }
        }
    }
}
