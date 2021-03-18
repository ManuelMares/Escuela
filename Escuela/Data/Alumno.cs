using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escuela.Data
{
    public class Alumno
    {
        public string Id { get; protected set; }
        public string NombreCompleto { get; protected set; }
        public List<string> Nombres { get; protected set; }
        public List<string> Apellidos { get; protected set; }
        public string Apodo { get; protected set; }
        public DateTime FechaNacimiento { get; protected set; }
        public int Edad { get; protected set; }
        public string Grupo { get; protected set; }
        public string Nota { get; protected set; }
        public int NumeroLista{ get; protected set; }
        public double PromedioGeneral { get; protected set; }

        public Dictionary<DateTime, bool> Asistencias { get; set; }
        public Dictionary<string, double> Asistencias_TotalEfectivaPorcentajeCalificacion { get; set; }
                        // { {asistenciaTotal,    double},
                        //   {asistenciaEfectiva, double},
                        //   {porcentaje,         double}      
                        //   {calificacion,       double},}
        public Dictionary<string, Dictionary<string, double>> Promedio_Clase_PromedioPorcentaje { get; set; }
                        // clase  //{  {promedio, double},
                                  //{  {porcentaje, double},
        public Dictionary<string, Dictionary<string, Dictionary<string, double>>> Promedios_ClaseTipos_CalificacionPorcentaje { get; set; }
                        //Clase            //tipo         //{  {calificacion, double},
                                                          //   {porcentaje,   double}  }


        //Constructor
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, double>>>> calificaciones_ClaseTipoActividad_ValorPorcentaje { get; set; }
                         //clase            //tipo           //actividad      //{ {porcentaje,   double},
                                                                              //  {calificacion, double}   }
        public Alumno(List<string> nombres, List<string> apellidos, string apodo, DateTime fechaNacimiento, string grupo,
            string nota, int numeroLista)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Nombres = nombres == null ? new List<string>{ "nuevo Alumno" , "segundo nombre"}: nombres ;
            this.Apellidos = apellidos == null ? new List<string> { "Apellido1", "apellido 2" } : apellidos;
            this.Apodo = apodo == null ? nombres[0] : apodo;
            this.FechaNacimiento = fechaNacimiento == null ? new DateTime(2000, 01, 01): fechaNacimiento;
            this.Grupo = grupo == null? "Grupo1": grupo;
            this.Nota = nota == null ? "" : nota;
            this.Edad = 0;
            this.NumeroLista = numeroLista;

            this.Asistencias = new Dictionary<DateTime, bool>() { };
            this.Promedio_Clase_PromedioPorcentaje = new Dictionary<string, Dictionary<string, double>>();
            this.Asistencias_TotalEfectivaPorcentajeCalificacion = new Dictionary<string, double>() ;
            this.calificaciones_ClaseTipoActividad_ValorPorcentaje = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, double>>>>() { };
            this.Promedios_ClaseTipos_CalificacionPorcentaje = new Dictionary<string, Dictionary<string, Dictionary<string, double>>>() { };

            CargarNombreCompleto();
            ObtenerEdad();
            InicializarAsistencia();
         }

        public void InicializarAsistencia()
        {
            Asistencias_TotalEfectivaPorcentajeCalificacion["asistenciaTotal"] = 0;
            Asistencias_TotalEfectivaPorcentajeCalificacion["asistenciaEfectiva"] = 0;
            Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"] = 0;
            Asistencias_TotalEfectivaPorcentajeCalificacion["calificacion"] = 0;
        }




        public void Actualizar_Promedios()
        {
            PromediosTipo_Tipo_ActualizarCalificacion();
            PromedioClase_Clase_ActualizarPromedio();
            ObtenerPromedio();
        }
        protected void PromediosTipo_Tipo_ActualizarCalificacion()
        {
            foreach (var clase in calificaciones_ClaseTipoActividad_ValorPorcentaje)
            {
                foreach (var tipo in calificaciones_ClaseTipoActividad_ValorPorcentaje[clase.Key])
                {
                    double sumaPromedio = 0;
                    double contadorPromedio = 0;
                    double promedioTipo = 0;
                    foreach (var actividad in tipo.Value)
                    {
                        if (!String.IsNullOrEmpty(actividad.Value["porcentaje"].ToString()) && actividad.Value["porcentaje"]>0)
                        {
                            sumaPromedio += actividad.Value["porcentaje"] * actividad.Value["calificacion"];
                            contadorPromedio += actividad.Value["porcentaje"];
                        }
                    }
                    if (contadorPromedio > 0 && Promedios_ClaseTipos_CalificacionPorcentaje.ContainsKey(clase.Key) &&
                        Promedios_ClaseTipos_CalificacionPorcentaje[clase.Key].ContainsKey(tipo.Key) &&
                        Promedios_ClaseTipos_CalificacionPorcentaje[clase.Key][tipo.Key] != null)
                    {
                        promedioTipo = sumaPromedio / contadorPromedio;
                        Promedios_ClaseTipos_CalificacionPorcentaje[clase.Key][tipo.Key]["calificacion"] = promedioTipo;
                    }
                }
            }


        }
        protected void PromedioClase_Clase_ActualizarPromedio()
        {
            foreach (var clase in Promedio_Clase_PromedioPorcentaje)
            {
                double sumaClase = 0;
                double contadorClase = 0;
                double promedioClase = 0;
                foreach (var promedio in Promedios_ClaseTipos_CalificacionPorcentaje[clase.Key])
                {
                    if (!String.IsNullOrEmpty(promedio.Value["porcentaje"].ToString()) && promedio.Value["porcentaje"]>0)
                    {
                        sumaClase += promedio.Value["calificacion"] * promedio.Value["porcentaje"];
                        contadorClase += promedio.Value["porcentaje"];
                    }
                }
                if (contadorClase > 0 && Promedio_Clase_PromedioPorcentaje.ContainsKey(clase.Key) && 
                    Promedio_Clase_PromedioPorcentaje[clase.Key].ContainsKey("promedio"))
                {
                    promedioClase = sumaClase / contadorClase;
                    Promedio_Clase_PromedioPorcentaje[clase.Key]["promedio"] = promedioClase;
                }
            }
        }
        protected void ObtenerPromedio()
        {
            double sumaPromedio = 0;
            double contadorPromedio = 0;
            double promedio = 0;
            foreach (var clase in Promedio_Clase_PromedioPorcentaje)
            {
                if (clase.Value.ContainsKey("promedio") && !String.IsNullOrEmpty(clase.Value["promedio"].ToString()) && clase.Value["promedio"]>0)
                {
                    sumaPromedio += clase.Value["promedio"] * clase.Value["porcentaje"];
                    contadorPromedio += clase.Value["porcentaje"];
                }
            }
            if (contadorPromedio >0)
            {
                promedio = sumaPromedio / contadorPromedio;
                if (Asistencias_TotalEfectivaPorcentajeCalificacion.ContainsKey("calificacion") &&
                    !String.IsNullOrEmpty(Asistencias_TotalEfectivaPorcentajeCalificacion["calificacion"].ToString()) &&
                    Asistencias_TotalEfectivaPorcentajeCalificacion["calificacion"] >0)
                {
                    promedio += Asistencias_TotalEfectivaPorcentajeCalificacion["calificacion"];
                }
                PromedioGeneral = promedio; 
            }
        }

        public void Clase_Crear(string nombreClase)
        {
            Calificaciones_Clase_Crear(nombreClase);
            PromediosTipo_Clase_Crear(nombreClase);
            PromedioClase_Clase_Crear(nombreClase);
            Actualizar_Promedios();
        }
        public void Clase_Eliminar(string nombreClase)
        {
            Calificaciones_Clase_Eliminar(nombreClase);
            PromediosTipo_Clase_Eliminar(nombreClase);
            PromedioClase_Clase_Eliminar(nombreClase);
            Actualizar_Promedios();
        }
        public void Clase_ActualizarNombre(string nombreClase, string nuevoNombre)
        {
            Calificaciones_Clase_ActualizarNombre(nombreClase, nuevoNombre);
            PromediosTipo_Clase_ActualizarNombre(nombreClase, nuevoNombre);
            PromedioClase_Clase_ActualizarNombre(nombreClase, nuevoNombre);
            Actualizar_Promedios();
        }
        public void Clase_ActualizarPorcentaje(string nombreClase, double porcentaje)
        {
            PromedioClase_Clase_ActualizarPorcentaje(nombreClase, porcentaje);
            Actualizar_Promedios();
        }

        //ya que en la interfaz todo es una copia, el usuario puede crear varis parámetros nuevos o editarlos y después enviar la info.
        //Por éso aquí se deben recibir listas o diccionarios
        public void Tipos_Crear(string clase, Dictionary<string, double> parametros)
        {
            foreach (var parametro in parametros)
            {
                Calificaciones_Tipo_Crear(clase, parametro.Key);
                PromediosTipo_Tipo_Crear(clase, parametro.Key, parametro.Value);
                Actualizar_Promedios();
            }
        }
        public void Tipos_Eliminar(string clase, List<string> nombresTipo)
        {
            foreach (var nombre in nombresTipo)
            {
                Calificaciones_Tipo_Eliminar(clase, nombre);
                PromediosTipo_Tipo_Eliminar(clase, nombre);
                Actualizar_Promedios();
            }
        }
        public void Tipos_ActualizarNombre(string clase, Dictionary<string, string> nombresParametros)
        {
            foreach (var nombreParametro in nombresParametros)
            {
                Calificaciones_Tipo_ActualizarNombre(clase, nombreParametro.Key, nombreParametro.Value);
                PromediosTipo_Tipo_ActualizarNombre(clase, nombreParametro.Key, nombreParametro.Value);
                Actualizar_Promedios();
            }
        }
        public void Tipos_ActualizarPorcentajes(string clase, Dictionary<string, double> parametros)
        {
            foreach (var parametro in parametros)
            {
                PromediosTipo_Tipo_ActualizarPorcentaje(clase, parametro.Key, parametro.Value);
                Actualizar_Promedios();
            }
        }


        //mover una calificacion activa la secuencia de actualizar promedio
        public void Actividad_Crear(string clase, string tipo, string nombreActividad, double porcentajeActividad)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(clase) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].ContainsKey(tipo) &&
                !calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][tipo].ContainsKey(nombreActividad))
            {
                calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][tipo].Add(nombreActividad, new Dictionary<string, double>());
                calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][tipo][nombreActividad].Add("porcentaje", porcentajeActividad);
                calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][tipo][nombreActividad].Add("calificacion", 0);
                Actualizar_Promedios();
            }
        }
        public void Actividad_Eliminar(string nombreClase, string nombreTipo, string nombreActividad)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(nombreClase) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase].ContainsKey(nombreTipo) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo].ContainsKey(nombreActividad))
            {
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo].Remove(nombreActividad);
                Actualizar_Promedios();
            }
        }
        public void Actividad_ActualizarNombre(string nombreClase, string nombreTipo, string nombreActividadViejo, string nombreActividadNuevo)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(nombreClase) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase].ContainsKey(nombreTipo) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo].ContainsKey(nombreActividadViejo) &&
                !calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo].ContainsKey(nombreActividadNuevo))
            {
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo].Add(
                        nombreActividadNuevo, new Dictionary<string, double>() {
                                                                            {"porcentaje", calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo][nombreActividadViejo]["porcentaje"]},
                                                                            {"calificacion", calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo][nombreActividadViejo]["calificacion"]}
                                                                                });
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo].Remove(nombreActividadViejo);
                Actualizar_Promedios();
            }
        }
        public void Actividad_ActualizarCalificacion(string nombreClase, string nombreTipo, string nombreActividad, double calificacion)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(nombreClase) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase].ContainsKey(nombreTipo) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo].ContainsKey(nombreActividad) &&
                calificacion<= 10 && calificacion>= 0)
            {
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo][nombreActividad]["calificacion"] = calificacion;
                Actualizar_Promedios();
            }
        }
        public void Actividad_ActividadActualizarPorcentaje(string nombreClase, string nombreTipo, string nombreActividad, double porcentaje)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(nombreClase) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase].ContainsKey(nombreTipo) &&
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo].ContainsKey(nombreActividad) &&
                porcentaje >= 0)
            {
                calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreClase][nombreTipo][nombreActividad]["porcentaje"] = porcentaje;
                Actualizar_Promedios();
            }
        }


        protected void Asistencia_ObtenerValor()
        {
            double razon = Asistencia_Contar();
            if (Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"] >= 0 && razon >=0)
            {
                Asistencias_TotalEfectivaPorcentajeCalificacion["calificacion"] =
                    razon * Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"];

            }
        }
        protected double Asistencia_Contar()
        {
            int contadorTotal = 0;
            int contadorPositivo = 0;
            foreach (var fecha in Asistencias)
            {
                contadorTotal++;
                if (fecha.Value == true)
                {
                    contadorPositivo++;
                }
            }

            if (contadorTotal != 0)
            {
                Asistencias_TotalEfectivaPorcentajeCalificacion["asistenciaTotal"] =  contadorTotal;
                Asistencias_TotalEfectivaPorcentajeCalificacion["asistenciaEfectiva"]  = contadorPositivo;
                double razon = (double) contadorPositivo / (double)contadorTotal;
                return razon;
            }
            else
            {
                return 0;
            }
        }
        public void Asistencia_ActualizarPorcentaje(double porcentaje)
        {
            if (porcentaje >= 0)
            {
                Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"] = porcentaje;
                Actualizar_Promedios();
            }
        }
        public void Asistencias_Agregar()
        {
            DateTime fecha = DateTime.Now.Date;
            bool correcto = false;
            while (correcto == false)
            {
                if (!Asistencias.ContainsKey(fecha))
                {
                    correcto = true;
                    Asistencias.Add(fecha, false);
                    Asistencia_ObtenerValor();
                    Actualizar_Promedios();
                }
                else
                {
                    fecha = fecha.AddDays(1);
                }
            }


        }
        public void Asistencias_Eliminar(DateTime fecha)
        {
            if (Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"] >= 0)
            {
                Asistencias.Remove(fecha);
                Asistencia_ObtenerValor();
                Actualizar_Promedios();
            }
        }
        public void Asistencias_ActualizarFecha(DateTime fecha, DateTime fechaNueva)
        {
            if (Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"] >= 0 &&
                Asistencias.ContainsKey(fecha) && !Asistencias.ContainsKey(fechaNueva))
            {
                Asistencias.Add(fechaNueva, Asistencias[fecha]);
                Asistencias.Remove(fecha);
                Asistencia_ObtenerValor();
                Actualizar_Promedios();
            }
        }
        public void Asistencias_ActualizarValor(DateTime fecha, bool valor)
        {
            if (Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"] >= 0)
            {
                Asistencias[fecha] = valor;
                Asistencia_ObtenerValor();
                Actualizar_Promedios();
            }
        }





        protected void Calificaciones_Clase_Crear(string clase)
        {
            if (!calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(clase))
            {
                calificaciones_ClaseTipoActividad_ValorPorcentaje.Add(clase, new Dictionary<string, Dictionary<string, Dictionary<string, double>>>());
            }
        }
        protected void Calificaciones_Clase_Eliminar(string clase)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(clase))
            {
                calificaciones_ClaseTipoActividad_ValorPorcentaje.Remove(clase);
            }

        }
        protected void Calificaciones_Clase_ActualizarNombre(string nombreViejo, string nombreNuevo)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(nombreViejo) && !calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(nombreNuevo))
            {
                //crear clase
                calificaciones_ClaseTipoActividad_ValorPorcentaje.Add(nombreNuevo, new Dictionary<string, Dictionary<string, Dictionary<string, double>>>());
                //crear tipo
                foreach(var tipo in calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreViejo])
                {
                    calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreNuevo].Add(tipo.Key, new Dictionary<string, Dictionary<string, double>>());

                    // crear actividad
                    foreach (var actividad in calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreViejo][tipo.Key])
                    {
                        calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreNuevo][tipo.Key].Add(actividad.Key, new Dictionary<string, double>());
                        //crear parámetros de la actividad
                        double porcentaje = calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreViejo][tipo.Key][actividad.Key]["porcentaje"];
                        double calificacion = calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreViejo][tipo.Key][actividad.Key]["calificacion"];

                        calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreNuevo][tipo.Key][actividad.Key].Add("porcentaje", porcentaje);
                        calificaciones_ClaseTipoActividad_ValorPorcentaje[nombreNuevo][tipo.Key][actividad.Key].Add("calificacion", calificacion);
                    }
                }

                calificaciones_ClaseTipoActividad_ValorPorcentaje.Remove(nombreViejo);


            }
        }

        protected void Calificaciones_Tipo_Crear(string clase, string tipoNombre)
        {
            if (tipoNombre != "Asistencia")
            {
                if (!calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].ContainsKey(tipoNombre))
                {
                    calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].Add(tipoNombre, new Dictionary<string, Dictionary<string, double>>());

                }
            }
        }
        protected void Calificaciones_Tipo_Eliminar(string clase, string tipoNombre)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].ContainsKey(tipoNombre))
            {
                calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].Remove(tipoNombre);
            }
        }
        protected void Calificaciones_Tipo_ActualizarNombre(string clase, string tipoNombre, string nuevoNombre)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].ContainsKey(tipoNombre) && !calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].ContainsKey(nuevoNombre) && nuevoNombre != "Asistencia")
            {
                //crear tipo
                calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].Add(nuevoNombre, new Dictionary<string, Dictionary<string, double>>());
                //copia las actividades
                foreach (var actividad in calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][tipoNombre])
                {
                    calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][nuevoNombre].Add(actividad.Key, new Dictionary<string, double>());
                    //crear propiedades por actividad
                    double porcentaje = calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][tipoNombre][actividad.Key]["porcentaje"];
                    double calificacion = calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][tipoNombre][actividad.Key]["calificacion"];
                    calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][nuevoNombre][actividad.Key].Add("porcentaje", porcentaje);
                    calificaciones_ClaseTipoActividad_ValorPorcentaje[clase][nuevoNombre][actividad.Key].Add("calificacion", calificacion);
                }
                calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].Remove(tipoNombre);
            }
        }
        //Los métodos que faltan de actividades son métodos directamente públicos, por ello los puse más arriba y la estructura
        //de su nombre omite el prefijo "Calificaciones_".


        protected void PromediosTipo_Clase_Crear(string claseNombre)
        {
            if (!Promedios_ClaseTipos_CalificacionPorcentaje.ContainsKey(claseNombre))
            {
                Promedios_ClaseTipos_CalificacionPorcentaje.Add(claseNombre, new Dictionary<string, Dictionary<string, double>>());
            }
        }
        protected void PromediosTipo_Clase_Eliminar(string claseNombre)
        {
            if (!Promedios_ClaseTipos_CalificacionPorcentaje.ContainsKey(claseNombre))
            {
                Promedios_ClaseTipos_CalificacionPorcentaje.Remove(claseNombre);
            }
        }
        protected void PromediosTipo_Clase_ActualizarNombre(string claseNombre, string nuevoNombre)
        {
            if (Promedios_ClaseTipos_CalificacionPorcentaje.ContainsKey(claseNombre) && !Promedios_ClaseTipos_CalificacionPorcentaje.ContainsKey(nuevoNombre))
            {
                //crear clase
                Promedios_ClaseTipos_CalificacionPorcentaje.Add(nuevoNombre, new Dictionary<string, Dictionary<string, double>>());
                //crearTipos
                foreach (var tipo in Promedios_ClaseTipos_CalificacionPorcentaje[claseNombre])
                {
                    Promedios_ClaseTipos_CalificacionPorcentaje[nuevoNombre].Add(tipo.Key, new Dictionary<string, double>());
                    //agregar parámetros
                    double calificacion = Promedios_ClaseTipos_CalificacionPorcentaje[claseNombre][tipo.Key]["calificacion"];
                    double porcentaje = Promedios_ClaseTipos_CalificacionPorcentaje[claseNombre][tipo.Key]["porcentaje"];

                    Promedios_ClaseTipos_CalificacionPorcentaje[nuevoNombre][tipo.Key]["calificacion"] = calificacion;
                    Promedios_ClaseTipos_CalificacionPorcentaje[nuevoNombre][tipo.Key]["porcentaje"] = porcentaje;
                }

                Promedios_ClaseTipos_CalificacionPorcentaje.Remove(claseNombre);
            }
        }

        protected void PromediosTipo_Tipo_Crear(string claseNombre, string tipoNombre, double porcentaje)
        {
            if (tipoNombre != "Asistencia")
            {
                if (!Promedios_ClaseTipos_CalificacionPorcentaje[claseNombre].ContainsKey(tipoNombre))
                {
                    Promedios_ClaseTipos_CalificacionPorcentaje[claseNombre].Add(tipoNombre, new Dictionary<string, double>());
                    Promedios_ClaseTipos_CalificacionPorcentaje[claseNombre][tipoNombre]["porcentaje"] = porcentaje;
                    Promedios_ClaseTipos_CalificacionPorcentaje[claseNombre][tipoNombre]["calificacion"] = 0;
                    PromediosTipo_Tipo_ActualizarCalificacion();
                }
            }
        }
        protected void PromediosTipo_Tipo_Eliminar(string clase, string tipoNombre)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].ContainsKey(tipoNombre))
            {
                Promedios_ClaseTipos_CalificacionPorcentaje[clase].Remove(tipoNombre);
                PromediosTipo_Tipo_ActualizarCalificacion();
            }
        }
        protected void PromediosTipo_Tipo_ActualizarNombre(string clase, string tipoNombre, string nuevoTipoNombre)
        {

            if (nuevoTipoNombre != "Asistencia")
            {
                if (Promedios_ClaseTipos_CalificacionPorcentaje[clase].ContainsKey(tipoNombre) && !Promedios_ClaseTipos_CalificacionPorcentaje[clase].ContainsKey(nuevoTipoNombre))
                {
                    //crear nuevo tipo
                    Promedios_ClaseTipos_CalificacionPorcentaje[clase].Add(nuevoTipoNombre, new Dictionary<string, double>());
                    //asignar parametros
                    double porcentaje = Promedios_ClaseTipos_CalificacionPorcentaje[clase][tipoNombre]["porcentaje"];
                    double calificacion = Promedios_ClaseTipos_CalificacionPorcentaje[clase][tipoNombre]["calificacion"];

                    Promedios_ClaseTipos_CalificacionPorcentaje[clase][nuevoTipoNombre].Add("porcentaje", porcentaje);
                    Promedios_ClaseTipos_CalificacionPorcentaje[clase][nuevoTipoNombre].Add("calificacion", calificacion);

                    //eliminar viejo tipo
                    Promedios_ClaseTipos_CalificacionPorcentaje[clase].Remove(nuevoTipoNombre);
                }
            }

        }
        protected void PromediosTipo_Tipo_ActualizarPorcentaje(string clase, string tipoNombre, double porcentaje)
        {
            if (calificaciones_ClaseTipoActividad_ValorPorcentaje[clase].ContainsKey(tipoNombre))
            {
                Promedios_ClaseTipos_CalificacionPorcentaje[clase][tipoNombre]["porcentaje"] = porcentaje;
                PromediosTipo_Tipo_ActualizarCalificacion();
            }
        }



        protected void PromedioClase_Clase_Crear(string nombreClase)
        {
            if (!Promedio_Clase_PromedioPorcentaje.ContainsKey(nombreClase))
            {
                Promedio_Clase_PromedioPorcentaje.Add(nombreClase, new Dictionary<string, double>());
                Promedio_Clase_PromedioPorcentaje[nombreClase].Add("promedio", 0);
                Promedio_Clase_PromedioPorcentaje[nombreClase].Add("porcentaje", 1);
            }
        }
        protected void PromedioClase_Clase_Eliminar(string nombreClase)
        {
            if (Promedio_Clase_PromedioPorcentaje.ContainsKey(nombreClase))
            {
                Promedio_Clase_PromedioPorcentaje.Remove(nombreClase);
            }
        }
        protected void PromedioClase_Clase_ActualizarNombre(string nombreClase, string nuevoNombre)
        {
            if (Promedio_Clase_PromedioPorcentaje.ContainsKey(nombreClase) && !Promedio_Clase_PromedioPorcentaje.ContainsKey(nuevoNombre) &&
                nuevoNombre != "Asistencia")
            {
                Promedio_Clase_PromedioPorcentaje.Add(nuevoNombre, new Dictionary<string, double>());
                double promedio = Promedio_Clase_PromedioPorcentaje[nombreClase]["promedio"];
                double porcentaje = Promedio_Clase_PromedioPorcentaje[nombreClase]["porcentaje"];
                Promedio_Clase_PromedioPorcentaje[nuevoNombre]["promedio"] = promedio;
                Promedio_Clase_PromedioPorcentaje[nuevoNombre]["porcentaje"] = porcentaje;
                Promedio_Clase_PromedioPorcentaje.Remove(nombreClase);
            }
        }
        protected void PromedioClase_Clase_ActualizarPorcentaje(string nombreClase, double porcentaje)
        {
            if (Promedio_Clase_PromedioPorcentaje.ContainsKey(nombreClase) &&
                porcentaje >= 0)
            {
                Promedio_Clase_PromedioPorcentaje[nombreClase]["porcentaje"] = porcentaje;
            }
        }








        public void ActualizarNombreCompleto(List<string> nombres, List<string> apellidos)
        {
            ActualizarNombres(nombres);
            ActualizarApellidos(apellidos);
            CargarNombreCompleto();
        }
        public void ActualizarNombres(List<string> nombres)
        {
            if (nombres.Count() > Nombres.Count())
            {
                int diferencia = nombres.Count() - Nombres.Count();
                for (var i=0; i < diferencia; i++)
                {
                    Nombres.Add("");
                }
            }

            for (var contador = 0; contador < nombres.Count(); contador++)
            {
                Nombres[contador] = nombres[contador] ;
            }
        }
        public void ActualizarApellidos(List<string> apellidos)
        {
            if (apellidos.Count() > Apellidos.Count())
            {
                int diferencia = apellidos.Count() - Apellidos.Count();
                for (var i = 0; i < diferencia; i++)
                {
                    Apellidos.Add("");
                }
            }

            for (var contador = 0; contador < apellidos.Count(); contador++)
            {
                Apellidos[contador] = apellidos[contador];
            }
        }
        protected void CargarNombreCompleto()
        {
            NombreCompleto = "";
            foreach (var nombre in Nombres)
            {
                NombreCompleto += nombre;
                NombreCompleto += " ";
            }
            foreach (var apellido in Apellidos)
            {
                NombreCompleto += apellido;
                NombreCompleto += " ";
            }
        }

        public void Actualizar_Apodo(string apodo)
        {
            if (apodo != null)
            {
                Apodo = apodo;
            }
            else
            {
                Apodo = Nombres[0];
            }
        }        
        public void Actualizar_FechaNacimiento(DateTime fechaNacimiento)
        {
            FechaNacimiento = fechaNacimiento;
        }
        public void ObtenerEdad()
        {
            int diferencia = DateTime.Today.Year - FechaNacimiento.Year;

            //si el mes es menor restamos un año directamente
            if (DateTime.Today.Month < FechaNacimiento.Month)
            {
                --diferencia;
            }
            //preguntamos si el mes es el mismo y el dia de hoy es menor al de la fecha de nacimiento
            else if (DateTime.Today.Month == FechaNacimiento.Month && DateTime.Today.Day < FechaNacimiento.Day)
            {
                --diferencia;
            }

            Edad = diferencia;
        }
        public void Actualizar_Grupo(string grupo)
        {
            if (grupo != null)
            {
                Grupo = grupo;
            }
        }
        public void Actualizar_Nota(string nota)
        {
            if (nota != null)
            {
                Nota = nota;
            }
        }
        public void Actualizar_NumeroLista(int numero)
        {
            if (numero > 0 )
            {
                NumeroLista = numero;
            }
        }






    }
}
