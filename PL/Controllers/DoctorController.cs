using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DoctorController : Controller
    {
        [HttpGet]

        public ActionResult GetAll()//En 1 hora solo termine hasta aqui D:
        {
            ML.Doctor doctor = new ML.Doctor();
            ML.Result result = BL.Doctor.GetAll();

            if (result.Correct)
            {
                doctor.Doctores = result.Objects;
                return View(doctor);
            }
            return View(doctor);
        }

        [HttpGet]
        public ActionResult Form(int? IdDoctor)
        {
            ML.Result resultEspecialidad = BL.Especialidad.GetAll();
            ML.Doctor doctor = new ML.Doctor();

            doctor.Especialidad = new ML.Especialidad();

            if (resultEspecialidad.Correct)
            {
                doctor.Especialidad.Especialidades = resultEspecialidad.Objects;
            }

            if (IdDoctor != null)
            {
                ML.Result result = BL.Doctor.GetById(IdDoctor.Value);
                if (result.Correct)
                {
                    doctor = (ML.Doctor)result.Object;
                    doctor.Especialidad.Especialidades = resultEspecialidad.Objects;
                    return View(doctor);
                }
                else
                {
                    ViewBag.Message = "Error al consultar la iformacion del registro";
                    return PartialView("Modal");
                }

            }
            else
            {
                return View(doctor);
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Doctor doctor)
        {
            ML.Result result = new ML.Result();

            if (doctor.IdDoctor == 0)
            {
                result = BL.Doctor.Add(doctor);
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
                result = BL.Doctor.Update(doctor);
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

        [HttpGet]
        public ActionResult Delete(int IdDoctor)
        {
            ML.Result result = BL.Doctor.Delete(IdDoctor);

            if (result.Correct)
            {
                ViewBag.Message = "Registro Eliminado";
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
