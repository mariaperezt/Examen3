using Examen3.Clases;
using Examen3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Examen3.Controllers
{
    [RoutePrefix("api/Matricula")]

    public class MatriculasController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Matricula> ConsultarTodos()
        {
            clsMatricula matricula = new clsMatricula();
            return matricula.ConsultarTodos();
        }


        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Matricula matricula)
        {
            clsMatricula Matriculaa = new clsMatricula();

            Matriculaa.matricula = matricula;

            return Matriculaa.Insertar();
        }

        [HttpGet]
        [Route("ConsultarXDocumento")]
        public Matricula ConsultarXDocumento(int idEstudiante)
        {
            clsMatricula matricula = new clsMatricula();
            return matricula.ConsultarXDocumento(idEstudiante);
        }

        [HttpGet]
        [Route("ConsultarXSemestre")]
        public Matricula ConsultarXSemestre(string SemestreMatricula)
        {
            clsMatricula matricula = new clsMatricula();
            return matricula.ConsultarXSemestre(SemestreMatricula);
        }

        [HttpPut]
        [Route ("Actualizar")]
        public string Actualizar([FromBody] Matricula matricula)
        {
            clsMatricula Matriculaa = new clsMatricula();

            Matriculaa.matricula = matricula;
            return Matriculaa.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Matricula matricula)
        {
            clsMatricula Matriculaa = new clsMatricula();
            Matriculaa.matricula = matricula;
            return Matriculaa.Eliminar();
        }






    }
}