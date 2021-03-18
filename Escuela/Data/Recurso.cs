using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escuela.Data
{
    public class Recurso
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Link { get; set; }
        public List<string> Etiquetas { get; set; }
        public List<string> Carpetas { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }


        //Constructor
        public Recurso(string nombre, string descripcion, string link, DateTime fecha, List<string> etiquetas, List<string> carpetas)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Link = link;
            this.Etiquetas = new List<string>() { };
            this.Carpetas = new List<string>() { };
            this.Fecha = fecha == null ? DateTime.Now.Date : fecha;

            Etiquetas_Agregar(etiquetas);
            Carpetas_Agregar(carpetas);
        }

        public void Etiquetas_Agregar(List<string> etiquetas)
        {
            if (etiquetas.Any() == true)
            {
                foreach (var etiqueta in etiquetas)
                {
                    if (etiqueta != null && etiqueta != "" && !Etiquetas.Contains(etiqueta))
                    {
                        Etiquetas.Add(etiqueta);
                    }
                }
            }
        }
        public void Etiquetas_Eliminar(string etiqueta)
        {
            if (etiqueta != null && etiqueta != "" && Etiquetas.Contains(etiqueta))
            {
                Etiquetas.Remove(etiqueta);
            }
        }
        public void Etiquetas_ActualizarNombre(string etiqueta, string nuevoNombre)
        {
            if (etiqueta != null && etiqueta != "" && Etiquetas.Contains(etiqueta) &&
                nuevoNombre != null && nuevoNombre != "" && !Etiquetas.Contains(nuevoNombre))
            {
                Etiquetas.Add(nuevoNombre);
                Etiquetas.Remove(etiqueta);
            }
        }



        public void Carpetas_Agregar(List<string> carpetas)
        {
            if (carpetas.Any() == true)
            {
                foreach (var carpeta in carpetas)
                {
                    if (carpeta != null && carpeta != "" && !Carpetas.Contains(carpeta))
                    {
                        Carpetas.Add(carpeta);
                    }
                }
            }
        }
        public void Carpetas_Eliminar(string carpeta)
        {
            if (carpeta != null && carpeta != "" && Etiquetas.Contains(carpeta))
            {
                Carpetas.Remove(carpeta);
            }
        }
        public void Carpetas_ActualizarNombre(string carpeta, string nuevoNombre)
        {
            if (carpeta != null && carpeta != "" && Etiquetas.Contains(carpeta) &&
                nuevoNombre != null && nuevoNombre != "" && !Etiquetas.Contains(nuevoNombre))
            {
                Carpetas.Add(nuevoNombre);
                Carpetas.Remove(carpeta);
            }
        }



        public void Actualizar_Nombre(string nombre)
        {
            if (nombre != null && nombre != "")
            {
                Nombre = nombre;
            }
        }
        public void Actualizar_Descripcion(string descripcion)
        {
            if (descripcion != null && descripcion != "")
            {
                Descripcion = descripcion;
            }
        }
        public void Actualizar_Link(string link)
        {
            if (link != null && link != "")
            {
                Link = link;
            }
        }
        public void Actualizar_Fecha(DateTime fecha)
        {
            if (fecha != null)
            {
                Fecha = fecha;
            }
        }
        public void Actualizar_Etiquetas(List<string> etiquetas)
        {
            Etiquetas = new List<string>() { };
            Etiquetas_Agregar(etiquetas);
        }
        public void Actualizar_Carpetas(List<string> carpetas)
        {
            Carpetas = new List<string>() { };
            Carpetas_Agregar(carpetas);
        }
    }
}
