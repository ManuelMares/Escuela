using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escuela.Data
{
    public class Maestro
    {

        public string Id { get; }

        public string NombreCompleto { get; }
        public string Nombre { get; set; }
        public string Nombre_Segundo { get; set; }
        public string Nombre_ApellidoPaterno { get; set; }
        public string Nombre_ApellidoMaterno { get; set; }
        public string Grado { get; }

        public int Edad { get; set; }
        public int Generacion { get; set; }


        //Grupos y Recursos se crean aquí, se inicializan en el constructor, pero se asignan evenatualmente
        public List<string> Grupos { get; set; }

        public List<Recurso> Recursos { get; set; }



        //Constructor
        public Maestro(string nombre, string grado, int edad, int generacion, string nombre_ApellidoMaterno, string nombre_ApellidoPaterno, string nombre_Segundo)
        {
            this.Id = Guid.NewGuid().ToString();

            this.Nombre = nombre;
            this.Nombre_Segundo = nombre_Segundo;
            this.Nombre_ApellidoPaterno = nombre_ApellidoPaterno;
            this.Nombre_ApellidoMaterno = nombre_ApellidoMaterno;
            this.Grado = grado;
            this.Edad = edad;
            this.Generacion = generacion;
            this.NombreCompleto = nombre + " " + nombre_Segundo + " " + nombre_ApellidoPaterno + " " + nombre_ApellidoMaterno;

            this.Recursos = new List<Recurso>();
            this.Grupos = new List<string>();
        }

    }
}
