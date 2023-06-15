using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PacienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Paciente paciente = new ML.Paciente();

            result = BL.Paciente.GetAll();

            if (result.Correct)
            {
                paciente.Pacientes = result.Objects;
                return View(paciente);
            }

            return View(paciente);
        }

        [HttpGet]

        public ActionResult Form(int? IdPaciente)
        {
            ML.Result resultDoctores = BL.Doctor.GetAll();
            ML.Paciente paciente = new ML.Paciente();

            paciente.Doctor = new ML.Doctor();

            if (resultDoctores.Correct)
            {
                paciente.Doctor.Doctores = resultDoctores.Objects;
            }

            if (IdPaciente != null)
            {
                ML.Result result = BL.Paciente.GetById(IdPaciente.Value);
                if (result.Correct)
                {
                    paciente = (ML.Paciente)result.Object;
                    paciente.Doctor.Doctores = resultDoctores.Objects;
                    return View(paciente);
                }
                else
                {
                    ViewBag.Message = "Error al consultar la iformacion del registro";
                    return PartialView("Modal");
                }

            }
            else
            {
                return View(paciente);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            if (paciente.IdPaciente == 0)
            {
                result = BL.Paciente.Add(paciente);
                if (result.Correct)
                {
                    ViewBag.Message = "Paciente registrado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un problema al intentar registrar al paciente";
                    return PartialView("Modal");
                }
            }
            else
            {
                result = BL.Paciente.Update(paciente);
                if (result.Correct)
                {
                    ViewBag.Message = "Los datos del paciente han sido actualizados";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un problema al intentar actualizar los datos del paciente";
                    return PartialView("Modal");
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdPaciente)
        {
            ML.Result result = BL.Paciente.Delete(IdPaciente);

            if (result.Correct)
            {
                ViewBag.Message = "El registro se elimino correctamente";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "Ocurrio un problema al intentar eliminar el registro";
                return PartialView("Modal");
            }
        }
    }
}
