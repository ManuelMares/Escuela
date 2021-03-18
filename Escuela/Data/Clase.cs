using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escuela.Data
{
    public class Clase
    {

        public string Id { get; }
        public string Nombre { get; protected set; }
        public string Color { get; protected set; }
        public Horario Horario { get; protected set; }
        public int Generacion { get; protected set; }  
        public double Promedio { get; protected set; }
        public double ClasePorcentaje { get; protected set; }
        
        public Dictionary<string, Dictionary<string, double>> PromediosTipo { get; set; }
                        //tipo              //{promedio, double} 
                                            //{porcentaje, double}
        


        //Constructor
        public Clase(string nombre, string color, Horario horario, int generacion)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Nombre = nombre;
            this.Color = color;
            this.Horario = horario;
            this.Generacion = generacion <2000 ? DateTime.Now.Year : generacion;
            this.PromediosTipo = new Dictionary<string, Dictionary<string, double>>();
        }


        public void ActualizarNombre(string nombre)
        {
            if(nombre != null)
            {
                Nombre = nombre;
            }
        }
        public void ActualizarColor(string color)
        {
            if (color != null)
            {
                Color = color;
            }
        }
        public void ActualizarHorario(Horario horario)
        {
            if (horario != null)
            {
                Horario = horario;
            }
        }
        public string MostrarHorario()
        {
            string hr1 =  Horario.HoraInicio.ToShortDateString();
            string hr2 = Horario.HoraInicio.ToShortDateString();

            return hr1 + " " + hr2;
        }
        public void ActualizarGeneracion(int generacion)
        {
            if (generacion <2000)
            {
                Generacion = generacion;
            }
        }
        public void ActualizarClasePorcentaje(double clasePorcentaje)
        {
            if (clasePorcentaje <= 0)
            {
                ClasePorcentaje = clasePorcentaje;
            }
        }

        public void PromediosTipo_Crear(string tipo, double promedio, double porcentaje)
        {
            if (!PromediosTipo.ContainsKey(tipo))
            {
                if (!String.IsNullOrEmpty(promedio.ToString()))
                {
                    if (!String.IsNullOrEmpty(porcentaje.ToString()))
                    {
                        PromediosTipo.Add(tipo, new Dictionary<string, double>());
                        PromediosTipo[tipo].Add("promedio", promedio);
                        PromediosTipo[tipo].Add("porcentaje", porcentaje);
                    }
                    else
                    {
                        PromediosTipo.Add(tipo, new Dictionary<string, double>());
                        PromediosTipo[tipo].Add("promedio", promedio);
                        PromediosTipo[tipo].Add("porcentaje", 0);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(porcentaje.ToString()))
                    {
                        PromediosTipo.Add(tipo, new Dictionary<string, double>());
                        PromediosTipo[tipo].Add("promedio", 0);
                        PromediosTipo[tipo].Add("porcentaje", porcentaje);
                    }
                    else
                    {
                        PromediosTipo.Add(tipo, new Dictionary<string, double>());
                        PromediosTipo[tipo].Add("promedio", 0);
                        PromediosTipo[tipo].Add("porcentaje", 0);
                    }
                }
                ActualizarPromedio();
            }

        }
        public void PromediosTipo_Eliminar(string tipo)
        {
            PromediosTipo.Remove(tipo);
            ActualizarPromedio();
        }
        public void PromediosTipo_ActualizaNombre(string tipo, string nuevoNombre)
        {
            PromediosTipo.Add(nuevoNombre, new Dictionary<string, double>());
            PromediosTipo[nuevoNombre].Add("porcentaje", PromediosTipo[tipo]["porcentaje"]);
            PromediosTipo[nuevoNombre].Add("promedio", PromediosTipo[tipo]["promedio"]);
            PromediosTipo.Remove(tipo);
        }
        public void PromediosTipo_ActualizaPromedio(string tipo, double valor)
        {
            PromediosTipo[tipo]["promedio"] = valor;
            ActualizarPromedio();
        }
        public void PromediosTipo_ActualizaPorcentaje(string tipo, double porcentaje)
        {
            PromediosTipo[tipo]["porcentaje"] = porcentaje;
            ActualizarPromedio();
        }


        public void ActualizarPromedio()
        {
            double suma = 0;
            double contador = 0;
            double promedio = 0;
            foreach(var tipo in PromediosTipo)
            {
                suma += tipo.Value["porcentaje"] * tipo.Value["promedio"];
                contador +=  tipo.Value["porcentaje"];
            }
            promedio = suma / contador;
            Promedio = promedio;
        }


    }
}
