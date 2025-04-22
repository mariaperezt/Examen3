using Examen3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace Examen3.Clases
{
    public class clsMatricula
    {
        private DBExamen3Entities DBexamen3 = new DBExamen3Entities();

        public Matricula matricula {get; set;}

        //consultar todos 
        [AllowAnonymous]
        public List<Matricula> ConsultarTodos()
        {
            return DBexamen3.Matriculas
                .ToList();
        }

        //ingresar matricula
        [Authorize]
        public string Insertar()
        {
            try
            {
                DBexamen3.Matriculas.Add(matricula);
                DBexamen3.SaveChanges();
                return "La matricula se inserto correctamente. ";

            }
            catch (Exception ex)
            { 
                return "Error al ingresar matricula. Recuerde que primero debe de haber un estudiante registrado " + ex.Message;
            }
        
        }

       

        //consultar matricula Por documento
        public Matricula ConsultarXDocumento(int idEstudiante)
        {           
            Matricula matri = DBexamen3.Matriculas.FirstOrDefault(e => e.idEstudiante == idEstudiante);
            return matri;      
        }

        //consultar matricula por semestre
        public Matricula ConsultarXSemestre(string SemestreMatricula)
        {
            Matricula matri = DBexamen3.Matriculas.FirstOrDefault(e => e.SemestreMatricula == SemestreMatricula);
            return matri;  
        }

        //actualizar matricula
        /*   public string Actualizar()
           {
               try 
               {
                   Matricula matr = ConsultarXDocumento(matricula.idEstudiante);
                   if (matr == null)
                   {
                       return "El id del estudiante que desea actualizar no es valido, verifique de nuevo. ";
                   }
                   matr.SemestreMatricula = matricula.SemestreMatricula;
                   matr.FechaMatricula = matricula.FechaMatricula;
                   return "Se actualizo correctamente la matricula del estudiante "; 
               }
               catch (Exception ex)
               {
                   return "Error al actualizar la matricula. " + ex.Message;
               }      
           }*/
        public string Actualizar()
        {
            try
            {
                // Consultar el registro existente de la matrícula usando el idEstudiante
                Matricula matr = ConsultarXDocumento(matricula.idEstudiante);

                // Verificar si el registro existe
                if (matr == null)
                {
                    return "El id del estudiante que desea actualizar no es válido, verifique de nuevo.";
                }

                // Actualizar las propiedades de la matrícula con los nuevos valores
                matr.NumeroCreditos = matricula.NumeroCreditos;
                matr.ValorCredito = matricula.ValorCredito;
                matr.TotalMatricula = matricula.TotalMatricula;
                matr.FechaMatricula = matricula.FechaMatricula;
                matr.SemestreMatricula = matricula.SemestreMatricula;
                matr.MateriasMatriculadas = matricula.MateriasMatriculadas;

                // En caso de que también quieras actualizar la relación con el estudiante (aunque no sea necesario en este caso)
                if (matricula.Estudiante != null)
                {
                    matr.Estudiante = matricula.Estudiante;
                }

                // Guardar los cambios en la base de datos
                DBexamen3.SaveChanges();

                return "Se actualizó correctamente la matrícula del estudiante.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar la matrícula. " + ex.Message;
            }
        }


        //eliminar matricula
        public string Eliminar()
        {
            try
            {
                Matricula matr = ConsultarXDocumento(matricula.idEstudiante);
                if (matr == null)
                {
                    return "Para eliminar la matricula del estudiante es necesario su id," +
                        " El id del estudiante que desea Eliminar no es valido, verifique de nuevo. ";

                }

                DBexamen3.Matriculas. Remove(matr);
                DBexamen3.SaveChanges();
                return "Se elimino correctamente la matricula del documento ";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}