using Examen3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing.Text;
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

        private Matricula Calcular(Matricula matri)
        {
            matri.TotalMatricula = matri.NumeroCreditos * matri.ValorCredito;


            return matri;
        }


        //consultar todos 
        public List<Matricula> ConsultarTodos()
        {
            return DBexamen3.Matriculas
                .ToList();
        }

        //ingresar matricula

       
     
        public string Insertar()
        {

            try
            {
                Matricula NuevaMatricula = Calcular(matricula);
                // Validar si ya existe una matrícula con ese estudiante
                var existente = DBexamen3.Matriculas
                    .FirstOrDefault(m => m.idEstudiante == matricula.idEstudiante);

                if (existente != null)
                {
                    return "Ya existe una matrícula para este estudiante.";
                }

                DBexamen3.Matriculas.Add(NuevaMatricula);
                DBexamen3.SaveChanges();
                return "La matrícula se insertó correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al ingresar matrícula. Recuerde que primero debe haber un estudiante registrado. " + ex.Message;
            }
        }
        
        //consultar por docuemnto con join sql para buscar en las tablas
        public Matricula ConsultarXDocumento(string documento)
        {
            var matricula = (from m in DBexamen3.Matriculas
                             join e in DBexamen3.Estudiantes on m.idEstudiante equals e.idEstudiante
                             where e.Documento == documento
                             select m).FirstOrDefault();
            return matricula;
        }
        


        //consultar matricula Por id estudiante, solo para facilidad con actualizar
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
        public string Actualizar()
        {
            try
            {
                Matricula NuevaMatricula = Calcular(matricula);
                

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
                matr.TotalMatricula = NuevaMatricula.TotalMatricula;
                matr.FechaMatricula = matricula.FechaMatricula;
                matr.SemestreMatricula = matricula.SemestreMatricula;
                matr.MateriasMatriculadas = matricula.MateriasMatriculadas;

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