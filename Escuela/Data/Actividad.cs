using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escuela.Data
{
    public class Actividad
    {
        public string Id { get; protected set; }

        public string Nombre { get; protected set; }
        public string Instruccion { get; protected set; }
        public string Nota { get; protected  set; }
        public DateTime FechaAsignacion { get; protected set; }
        public DateTime FechaEntrega { get; protected set; }
        public double Porcentaje { get; protected set; }
        public double Promedio { get; protected set; }
        public string TipoActividad { get; protected set; }
        public List<string> TipoActividadOpciones { get; set; }
        public List<string> RecursosId { get; protected set; }



        //Constructor
        public Actividad(string nombre, string tipoActividad, string instruccion, DateTime fechaAsignacion, 
            DateTime fechaEntrega, string nota, double porcentaje, List<string> recursosId, List<string> tiposActividad)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Nombre = nombre;
            this.Instruccion = instruccion == null ? "": instruccion;
            this.FechaAsignacion = fechaAsignacion == null ? DateTime.Now.Date : fechaAsignacion;
            this.FechaEntrega = fechaEntrega == null ? DateTime.Now.Date : fechaEntrega;
            this.Nota = nota == null ? "": nota ;
            this.RecursosId = recursosId.Any() ? new List<string>() : recursosId;
            this.TipoActividadOpciones = new List<string>() { };
            this.Promedio = 0;

            foreach (var tipo in tiposActividad)
            {
                CargarTiposActividadOpciones(tipo);
            }
            foreach (var recurso in recursosId)
            {
                Recursos_Crear(recurso);
            }

            ActualizarPorcentaje(porcentaje);
            ActualizarTipoActividad(tipoActividad);
        }


        public void ActualizarNombre(string nombre)
        {
            if(nombre != null)
            {
                Nombre = nombre;
            }
        }
        public void ActualizarInstruccion(string instruccion)
        {
            if (instruccion == null)
            {
                instruccion = "";
            }
            Instruccion = instruccion;
        }
        public void ActualizarNota(string nota)
        {
            if (nota == null)
            {
                nota = "";
            }
            Nota = nota;
        }
        public void ActualizarPorcentaje(double porcentaje)
        {
            if (porcentaje >0)
            {
                Porcentaje = porcentaje;
            }
            else
            {
                Porcentaje = 1;
            }
        }
        public void ActualizarTipoActividad(string tipoActividad)
        {
            if (tipoActividad != null)
            {
                foreach(var opcion in TipoActividadOpciones)
                {
                    if(opcion == tipoActividad)
                    {
                        TipoActividad = tipoActividad;

                    }
                }
            }
            else
            {
                if (TipoActividad == null)
                {
                    TipoActividad = "Actividad";
                }
            }
        }
        public void ActualizarPromedio(double promedio)
        {
            if (promedio <= 10 && promedio >= 0)
            {
                Promedio = promedio;
            }
        }
        public void ActualizarFechaAsignacion(DateTime fecha)
        {
            if (fecha != null)
            {
                FechaAsignacion = fecha;
            }
        }
        public void ActualizarFechaEntrega(DateTime fecha)
        {
            if (fecha != null && 
                fecha.Month >= FechaAsignacion.Month &&
                fecha.Day >= FechaAsignacion.Day)
            {
                FechaEntrega = fecha;
            }
        }

        protected void CargarTiposActividadOpciones(string recurso)
        {
            if (!TipoActividadOpciones.Contains(recurso))
            {
                TipoActividadOpciones.Add(recurso);
            }
        }
        public void TiposActividadOpciones_Agregar(string parametro)
        {
            if (!TipoActividadOpciones.Contains(parametro))
            {
                TipoActividadOpciones.Add(parametro);
            }
        }
        public void TiposActividadOpciones_Eliminar(string parametro)
        {
            if (TipoActividadOpciones.Contains(parametro))
            {
                if (TipoActividad == parametro)
                {
                    ActualizarTipoActividad("Actividad");
                }
                TipoActividadOpciones.Remove(parametro);
            }
        }
        public void TiposActividadOpciones_ActualizarNombre(string parametro, string nuevoNombre)
        {
            if (TipoActividadOpciones.Contains(parametro) && !TipoActividadOpciones.Contains(nuevoNombre))
            {
                if (TipoActividad == parametro)
                {
                    ActualizarTipoActividad(nuevoNombre);
                }
                TipoActividadOpciones.Add(nuevoNombre);
                TipoActividadOpciones.Remove(parametro);

            }
        }

        public void Recursos_Crear(string recurso)
        {
            if (!RecursosId.Contains(recurso))
            {
                RecursosId.Add(recurso);
            }
        }
        public void Recursos_Eliminar(string recurso)
        {
            if (RecursosId.Contains(recurso))
            {
                RecursosId.Remove(recurso);
            }
        }
        public void Recursos_ActualizarNombre(string recurso, string nuevoNombre)
        {
            if (RecursosId.Contains(recurso) && !RecursosId.Contains(nuevoNombre))
            {
                RecursosId.Add(nuevoNombre);
                RecursosId.Remove(recurso);
            }
        }



        public void Actualizar_RecursosId(List<string> nuevosRecursosId)
        {
            RecursosId = new List<string>() { };
            RecursosId_Agregar(nuevosRecursosId);
        }
        public void RecursosId_Agregar(List<string> recursosId)
        {
            if (recursosId.Any() == true)
            {
                foreach (var recurso in recursosId)
                {
                    if (recurso != null && recurso != "" && !RecursosId.Contains(recurso))
                    {
                        RecursosId.Add(recurso);
                    }
                }
            }
        }
        public void RecursosId_Eliminar(string RecursoId)
        {
            if (RecursoId != null && RecursoId != "" && RecursosId.Contains(RecursoId))
            {
                RecursosId.Remove(RecursoId);
            }
        }
        public void RecursosIds_ActualizarNombre(string RecursoId, string nuevoId)
        {
            if (RecursoId != null && RecursoId != "" && RecursosId.Contains(RecursoId) &&
                nuevoId != null && nuevoId != "" && !RecursosId.Contains(nuevoId))
            {
                RecursosId.Add(nuevoId);
                RecursosId.Remove(RecursoId);
            }
        }



    }
}
