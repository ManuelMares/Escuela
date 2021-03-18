using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Escuela.Data
{
    public class Calificacion
    {

        [Range(0, 10, ErrorMessage = "La calificación debe estar entre 0 y 10")]
        public Double _Numero;

        [Range(0, 10, ErrorMessage = "La calificación debe estar entre 0 y 10")]
        public Double Numero
        {
            set
            {
                if(value <0 || value > 10)
                {
                }
                else
                {
                    _Numero = value;
                }
	        }
            get
            {
                return _Numero;
            }
        }
        public int Alumno { get; set; }
        public int Actividad { get; set; }

        //Constructor
        public Calificacion(Double numero, int alumno, int actividad)
        {
            this.Numero = numero;
            this.Alumno = alumno;
            this.Actividad = actividad;
        }


    }
}
