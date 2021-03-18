using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escuela.Data
{
    public class Horario
    {
        public DateTime HoraInicio  { get; set; } 
        public DateTime HoraFinal  { get; set; }
        public int Duracion  { get; set; }


        //Constructor
        public Horario( DateTime horaInicio, DateTime horaFinal)
        {
            this.HoraInicio = horaInicio;
            this.HoraFinal = horaFinal;
            CalcularDuracion();
        }

        private void CalcularDuracion()
        {
        }

        public void ActualizarHoraInicio(DateTime hora)
        {
            HoraInicio = hora;
        }

        public void ActualizarHoraFinal(DateTime hora)
        {
            HoraFinal = hora;
            CalcularDuracion();
        }
    }
}
