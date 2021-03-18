using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escuela.Data
{
    public class Grupo
    {
        public string Id { get; }
        public string Nombre { get; set; }
        public string Maestro { get; }
        public int? Generacion { get; set; }
        public string Grado { get; set; }

        //Estas variables son creadas (si son listas, también se inicializan en el constructor), pero sus valores se asignarán después
        public double Promedio { get; private set; }
        public List<string> Ordenamientos { get; set; }
        public List<Alumno> Alumnos { get; set; }
        public List<Clase> Clases { get; set; }
        public List<Recurso> Recursos { get; set; }
        public List<string> Recursos_Carpetas { get; set; }
        public List<string> Recursos_Etiquetas{ get; set; }
        public Dictionary<string, Dictionary<string, List<Actividad>>> Actividades { get; set; } 
                            //clase             {{ tipo,   Actividad}}
        public List<string> Colores { get; protected set; }


        //Constructor
        public Grupo(string nombre, string maestro, int? generacion, int grado)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Nombre = nombre;
            this.Maestro = maestro;
            this.Grado = grado.ToString();
            this.Generacion = generacion == null ? DateTime.Now.Year : generacion;

            this.Ordenamientos = new List<string>();
            this.Alumnos = new List<Alumno>() { };
            this.Actividades = new Dictionary<string, Dictionary<string, List<Actividad>>>();
            this.Clases = new List<Clase>() { };
            this.Colores = new List<string>() { };
            this.Recursos = new List<Recurso>() { };
            this.Recursos_Carpetas = new List<string>() { };
            this.Recursos_Etiquetas= new List<string>() { };

            CargarColores();
            CargarOrdenamientos();
        }

        protected void CargarOrdenamientos()
        {
            Ordenamientos.Add("Número de lista Ascendente"); 
            Ordenamientos.Add("Apodo Ascendente");
            Ordenamientos.Add("Apodo descendente");
            Ordenamientos.Add("Nombres ascendente");
            Ordenamientos.Add("Nombres descendente");
            Ordenamientos.Add("Apellidos ascendente");
            Ordenamientos.Add("Apellidos descendente");
            Ordenamientos.Add("Promedio Ascendente");
            Ordenamientos.Add("Promedio descendente");
        }
        protected void CargarColores()
        {
            //rojo
            Colores.Add("#f93838");
            Colores.Add("#f91818");
            Colores.Add("#cb0202");
            Colores.Add("#a61717");
            Colores.Add("#7d0606");
            //Orange
            Colores.Add("#e87645");
            Colores.Add("#e26028");
            Colores.Add("#b9410d");
            Colores.Add("#963911");
            //Amarillo
            Colores.Add("#e6b33a");
            Colores.Add("#f0ab09");
            Colores.Add("#cb9415");
            Colores.Add("#a37813");
            Colores.Add("#c8bd0e");
            //Verde1
            Colores.Add("#74d934");
            Colores.Add("#4dba08");
            Colores.Add("#498e1e");
            Colores.Add("#2f6f07");
            //Verde2
            Colores.Add("#38d23a");
            Colores.Add("#0cbd0e");
            Colores.Add("#219022");
            Colores.Add("#0c690d");
            //Azul 1
            Colores.Add("#31d0a3");
            Colores.Add("#0db887");
            Colores.Add("#158a68");
            Colores.Add("#086b4e");
            //Azul 2
            Colores.Add("#39b6de");
            Colores.Add("#0e96c2");
            Colores.Add("#1c7c9c");
            Colores.Add("#0c536b");
            //Azul 3
            Colores.Add("#306fd1");
            Colores.Add("#0b4db3");
            Colores.Add("#1b4a91");
            Colores.Add("#092c61");
            //Morado
            Colores.Add("#8935dd");
            Colores.Add("#6610bd");
            Colores.Add("#60249c");
            Colores.Add("#3b0b6b");
            //Rosa
            Colores.Add("#c332aa");
            Colores.Add("#b40e97");
            Colores.Add("#961e81");
            Colores.Add("#6f0a5d");
        }
        public void Actualizar_Nombre(string nombre)
        {
            if (nombre !=null && nombre != "")
            {
                Nombre = nombre;
            }
        }
        public void Actualizar_Grado(string grado)
        {
            if (grado != null && grado != "")
            {
                Grado = grado;
            }
        }
        public void Actualizar_Generacion(int generacion)
        {
            if (generacion > 0)
            {
                Generacion = generacion;
            }
        }
        public void Actualizar_PromedioGrupo()
        {
            double contador = 0;
            double suma = 0;
            foreach (var alumno in Alumnos)
            {
                suma += alumno.PromedioGeneral;
                contador++;
            }
            if (contador>0)
            {
                Promedio = suma / contador;
            }
        }



        //ACTUALIZAR CALIFICACIÓN
        protected void Controlador_ActualizarPromedios()
        { 
            //PROMEDIOS POR ALUMNO
            foreach (var alumno in Alumnos)
            {
                //PROMEDIOS POR ALUMNO
                alumno.Actualizar_Promedios();
            }


            //PROMEDIOS POR ACTIVIDAD (GRUPO.ACTIVIDADES.PROMEDIO)
            //primero se crea un diccionario auxiliar que guarde las actividades de cada tipo según su nombre, y con ello sus calificaciones y contadores.
            //el primer foreach crea los espacios en el diccionario por actividad.
            //el segundo foreach suma las calif de cada alumno al espacio correspondiente del diccionario.
            //el tercer foreach toma el diccionario creado y reparte sus valores para cada actividad, pues ya están completos.
            Dictionary<string, Dictionary<string, Dictionary<string, double>>> promediosAlumno_Tipo_Actividad = new Dictionary<string, Dictionary<string, Dictionary<string, double>>>() { };
            //       { tipo,           {actividad,         {<suma, double>,
            //                                              <contador, double> }}}
            foreach (var clase in Actividades)
            {
                promediosAlumno_Tipo_Actividad = new Dictionary<string, Dictionary<string, Dictionary<string, double>>>() { };
                foreach (var tipo in clase.Value)
                {
                    promediosAlumno_Tipo_Actividad.Add(tipo.Key, new Dictionary<string, Dictionary<string, double>>() { });
                    foreach (var actividad in tipo.Value)
                    {
                        if (!promediosAlumno_Tipo_Actividad[tipo.Key].ContainsKey(actividad.Nombre))
                        {
                            promediosAlumno_Tipo_Actividad[tipo.Key].Add(actividad.Nombre, new Dictionary<string, double>() { { "suma", 0 }, { "contador", 0 } });
                        }
                    }
                }
            }

            foreach (var alumno in Alumnos)
            {
                foreach (var clase in alumno.calificaciones_ClaseTipoActividad_ValorPorcentaje)
                {
                    foreach (var tipo in clase.Value)
                    {
                        foreach (var actividad in tipo.Value)
                        {
                            if (actividad.Value.ContainsKey("calificacion") && actividad.Value["calificacion"].ToString("F6") != null &&
                                actividad.Value["calificacion"]>0)
                            {
                                promediosAlumno_Tipo_Actividad[tipo.Key][actividad.Key]["suma"] += actividad.Value["calificacion"];
                                promediosAlumno_Tipo_Actividad[tipo.Key][actividad.Key]["contador"]++;
                            }
                        }
                    }
                }
            }

            foreach (var clase in Actividades)
            {
                foreach (var tipo in clase.Value)
                {
                    foreach (var actividad in tipo.Value)
                    {
                        double sumaActividad = 0;
                        int contadorActividad = 0;
                        foreach (var alumno in Alumnos)
                        {
                            if (alumno.calificaciones_ClaseTipoActividad_ValorPorcentaje.ContainsKey(clase.Key) &&
                                alumno.calificaciones_ClaseTipoActividad_ValorPorcentaje[clase.Key].ContainsKey(tipo.Key) &&
                                alumno.calificaciones_ClaseTipoActividad_ValorPorcentaje[clase.Key][tipo.Key].ContainsKey(actividad.Nombre) &&
                                alumno.calificaciones_ClaseTipoActividad_ValorPorcentaje[clase.Key][tipo.Key][actividad.Nombre].ContainsKey("calificacion") &&
                                alumno.calificaciones_ClaseTipoActividad_ValorPorcentaje[clase.Key][tipo.Key][actividad.Nombre]["calificacion"].ToString("F6") != null &&
                                alumno.calificaciones_ClaseTipoActividad_ValorPorcentaje[clase.Key][tipo.Key][actividad.Nombre]["calificacion"]>0)
                            {
                                sumaActividad += alumno.calificaciones_ClaseTipoActividad_ValorPorcentaje[clase.Key][tipo.Key][actividad.Nombre]["calificacion"];
                                contadorActividad++;
                            }
                        }

                        actividad.ActualizarPromedio(sumaActividad/ contadorActividad);
                        //if (promediosAlumno_Tipo_Actividad[tipo.Key][actividad.Nombre]["contador"] >0)
                        //{
                          //  actividad.ActualizarPromedio(promediosAlumno_Tipo_Actividad[tipo.Key][actividad.Nombre]["suma"] / promediosAlumno_Tipo_Actividad[tipo.Key][actividad.Nombre]["contador"]);
                        //}
                    }
                }
            }


            //PROMEDIOS POR TIPO (CLASE.PROMEDIOSTIPO)
            foreach (var clase in Clases)
            {
                //Se crea un diccionario que, para cada tipo, obtenga el promedio correspondiente por alumno y ya todo junto, se pasa a clase
                //el primer foreach crea los espacios en el diccionario para cada tipo
                //el segundo foreach da, a cada tipo, el valor correspondiente por alumno. Los valores dados se van sumando
                //el tercer toma los valores del diccionario, ya completo, para calcular los promedios para la clase.

                Dictionary<string, Dictionary<string, double>> PromedioTipo = new Dictionary<string, Dictionary<string, double>>() { };
                //{tipo         { <suma, double>,
                //                     <contador, double> }}

                foreach (var tipo in clase.PromediosTipo)
                {
                    PromedioTipo.Add(tipo.Key, new Dictionary<string, double>() { { "suma", 0 }, { "contador", 0 } });
                }

                foreach (var tipo in clase.PromediosTipo)
                {
                    foreach (var alumno in Alumnos)
                    {
                        if (alumno.Promedios_ClaseTipos_CalificacionPorcentaje[clase.Nombre][tipo.Key].ContainsKey("calificacion") &&
                            alumno.Promedios_ClaseTipos_CalificacionPorcentaje[clase.Nombre][tipo.Key]["calificacion"].ToString("F6") != null &&
                            alumno.Promedios_ClaseTipos_CalificacionPorcentaje[clase.Nombre][tipo.Key]["calificacion"] > 0)
                        {
                            PromedioTipo[tipo.Key]["suma"] +=
                            alumno.Promedios_ClaseTipos_CalificacionPorcentaje[clase.Nombre][tipo.Key]["calificacion"];
                            PromedioTipo[tipo.Key]["contador"]++;
                        }
                    }
                }

                foreach (var tipo in clase.PromediosTipo)
                {
                    if (PromedioTipo[tipo.Key]["contador"] > 0)
                    {
                        tipo.Value["promedio"] = PromedioTipo[tipo.Key]["suma"] / PromedioTipo[tipo.Key]["contador"];
                    }
                }
            }

            //PROMEDIOS POR CLASE
            foreach (var clase in Clases)
            {
                clase.ActualizarPromedio();
            }

            //PROMEDIOS POR GRUPOS
            Actualizar_PromedioGrupo();

        }





        //CONTROLADOR RECURSOS
        public void Controlador_Recurso_Crear(string nombre, string descripcion, string link, List<string> carpetas,
            List<string> etiquetas, DateTime fecha, List<string> actividadesId, string claseIndice, string tipoIndice)
        {
            //Recursos
            Recursos.Add(new Recurso(nombre, descripcion, link, fecha, carpetas, etiquetas));

            //Actividades
            string IdRecurso = Recursos[Recursos.IndexOf(Recursos.Where(p => p.Nombre == nombre).FirstOrDefault())].Id;
            foreach (var actividadId in actividadesId)
            {
                int indice = Actividades[claseIndice][tipoIndice].IndexOf(Actividades[claseIndice][tipoIndice].Where(p => p.Id == actividadId).FirstOrDefault());
                Actividades[claseIndice][tipoIndice][indice].RecursosId_Agregar( new List<string>() { IdRecurso });
            }
        }
        public void Controlador_Recurso_Crear(string nombre, string descripcion, string link, List<string> carpetas,
            List<string> etiquetas, DateTime fecha)
        {
            Recursos.Add(new Recurso(nombre, descripcion, link, fecha, carpetas, etiquetas));
        }
        public void Controlador_Recurso_Eliminar(string recursoId)
        {
            int index = Recursos.IndexOf(Recursos.Where(p => p.Id == recursoId).FirstOrDefault());
            //Recursos
            Recursos.RemoveAt(index);

            //Actividades
            foreach (var clase in Actividades)
            {
                foreach (var tipo in clase.Value)
                {
                    List<int> actividadesContienen = new List<int>() { };
                    int contador = 0;
                    foreach (var actividad in tipo.Value)
                    {
                        if (actividad.RecursosId.Contains(recursoId))
                        {
                            actividadesContienen.Add(contador);
                        }
                        contador++;
                    }
                    if (actividadesContienen.Count>0)
                    {
                        foreach (var indice in actividadesContienen)
                        {
                            tipo.Value[indice].RecursosId.Remove(recursoId);
                        }
                    }
                }
            }
        }
        public void Controlador_Recurso_Actualizar(string originalId, string nombre, string descripcion, string link, List<string> carpetas,
            List<string> etiquetas, DateTime fecha)
        {
            int index = Recursos.IndexOf(Recursos.Where(p => p.Id == originalId).FirstOrDefault());
            Recursos[index].Actualizar_Nombre(nombre);
            Recursos[index].Actualizar_Link(link);
            Recursos[index].Actualizar_Fecha(fecha);
            Recursos[index].Actualizar_Etiquetas(etiquetas);
            Recursos[index].Actualizar_Descripcion(descripcion);
            Recursos[index].Actualizar_Carpetas(carpetas);
            Recursos.Add(new Recurso(nombre, descripcion, link, fecha, carpetas, etiquetas));
        }


        //CONTROLADOR ALUMNO
        public void Controlador_Alumno_Crear(List<string> nombres, List<string> apellidos, string apodo, DateTime fechaNacimiento, string grupo,
            string nota, int numeroLista)
        {
            Alumnos.Add(new Alumno(nombres, apellidos, apodo, fechaNacimiento, grupo,
                nota, numeroLista));
            int index = Alumnos.IndexOf(Alumnos.Where(p => p.Nombres == nombres).FirstOrDefault());

            //AGREGAR CLASES
            foreach (var clase in Clases)
            {
                Alumnos[index].Clase_Crear(clase.Nombre);
            }

            //AGREGAR TIPOS
            if (Alumnos.Count>0 && Alumnos[0].Promedios_ClaseTipos_CalificacionPorcentaje != null)
            {
                foreach (var clase in Alumnos[0].Promedios_ClaseTipos_CalificacionPorcentaje)
                {
                    if (clase.Value != null)
                    {
                        foreach (var tipo in clase.Value)
                        {
                            if (tipo.Value != null)
                            {
                                Dictionary<string, double> diccionario = new Dictionary<string, double>() { };
                                diccionario.Add( tipo.Key, tipo.Value["porcentaje"]);
                                Alumnos[index].Tipos_Crear(clase.Key, diccionario);
                            }
                        }
                    }
                }
            }

            // AGREGAR ACTIVIDADES 
            foreach (var clase in Actividades)
            {
                foreach (var tipo in clase.Value)
                {
                    foreach (var actividad in tipo.Value)
                    {
                        Alumnos[index].Actividad_Crear(clase.Key, tipo.Key, actividad.Nombre, actividad.Porcentaje);
                    }
                }
            }

            //AGREGAR ASISTENCIAS
            if (Alumnos.Count()>0)
            {
                int numFecha = 0;
                foreach (var fecha in Alumnos[0].Asistencias)
                {
                    Alumnos[index].Asistencias_Agregar();
                    DateTime fechaVieja = DateTime.Now.Date;
                    fechaVieja.AddDays(numFecha);
                    Alumnos[index].Asistencias_ActualizarFecha(fechaVieja, fecha.Key);
                    numFecha++;
                }

                Alumnos[index].Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"] = Alumnos[0].Asistencias_TotalEfectivaPorcentajeCalificacion["porcentaje"];
            }
            //ACTUALIZAR NÚMEROS DE LISTA
            OrdenarAlumnos("Apellidos descendente");
            int lista = 0;
            foreach (var alumno in Alumnos)
            {
                alumno.Actualizar_NumeroLista(lista);
                lista++;
            }

            Controlador_ActualizarPromedios();
        }
        public void Controlador_Alumno_AumentarNumeroLista(int numeroLista)
        {
            int index = Alumnos.IndexOf(Alumnos.Where(p => p.NumeroLista == numeroLista).FirstOrDefault());
            Alumnos[index].Actualizar_NumeroLista(numeroLista +1 );

            int index_masUno = Alumnos.IndexOf(Alumnos.Where(p => p.NumeroLista == (numeroLista+1)).FirstOrDefault());
            Alumnos[index_masUno].Actualizar_NumeroLista(numeroLista);

            OrdenarAlumnos("Número de lista Ascendente");
        }
        public void Controlador_Alumno_DisminuirNumeroLista(int numeroLista)
        {
            int index = Alumnos.IndexOf(Alumnos.Where(p => p.NumeroLista == numeroLista).FirstOrDefault());
            Alumnos[index].Actualizar_NumeroLista(numeroLista - 1);

            int index_menosUno = Alumnos.IndexOf(Alumnos.Where(p => p.NumeroLista == (numeroLista - 1)).FirstOrDefault());
            Alumnos[index_menosUno].Actualizar_NumeroLista(numeroLista);

            OrdenarAlumnos("Número de lista Ascendente");
        }
        public void Controlador_Alumno_Eliminar(string alumnoId)
        {
            int index = Alumnos.IndexOf(Alumnos.Where(p => p.Id == alumnoId).FirstOrDefault());
            Alumnos.RemoveAt(index);
        }
        public void Controlador_Alumno_ActualizarPropiedades(Alumno alumnoActualizado, string alumnoId)
        {
            int index = Alumnos.IndexOf(Alumnos.Where(p => p.Id == alumnoId).FirstOrDefault());
            Alumnos[index].ActualizarNombres( alumnoActualizado.Nombres);
            Alumnos[index].ActualizarApellidos (alumnoActualizado.Apellidos);
            Alumnos[index].Actualizar_Apodo (alumnoActualizado.Apodo);
            Alumnos[index].Actualizar_FechaNacimiento (alumnoActualizado.FechaNacimiento);
            Alumnos[index].Actualizar_Grupo (alumnoActualizado.Grupo);
            Alumnos[index].Actualizar_Nota (alumnoActualizado.Nota);
        }


        //CONTROLADOR CLASE
        public void Controlador_Clase_Crear(string claseNombre, string claseColor, int claseGeneracion, Horario claseHorario)
        {
            //Agregar en clases
            Clases.Add(new Clase (claseNombre, claseColor, claseHorario, claseGeneracion));
            //Agregar en Actividades
            Actividades.Add(claseNombre, new Dictionary<string, List<Actividad>>());

            //Agregar en Alumno
            foreach(var alumno in Alumnos)
            {
                alumno.Clase_Crear(claseNombre);
            }
            Controlador_ActualizarPromedios();
        }
        public void Controlador_Clase_Eliminar(string claseNombre)
        {
            //Eliminar en clases
            int index = Clases.IndexOf(Clases.Where(p => p.Nombre == claseNombre).FirstOrDefault());
            Clases.RemoveAt(index);

            //Eliminar en actividades
            Actividades.Remove(claseNombre);

            //Eliminar en Alumno
            foreach (var alumno in Alumnos)
            {
                alumno.Clase_Eliminar(claseNombre);
            }
            Controlador_ActualizarPromedios();
        }
        public void Controlador_Clase_ActualizarPropiedades(int index, string claseNombreNuevo, string claseColor, int claseGeneracion,
            Horario claseHorario, double clasePorcentaje)
        {
            string claseNombre = Clases[index].Nombre;
            //Actualizar en clases
            Clases[index].ActualizarNombre(claseNombreNuevo);
            Clases[index].ActualizarColor(claseColor);
            Clases[index].ActualizarHorario(claseHorario);
            Clases[index].ActualizarGeneracion(claseGeneracion);
            Clases[index].ActualizarClasePorcentaje(clasePorcentaje);


            //Actualizar en Actividades
            if (Actividades.ContainsKey(claseNombre) && !Actividades.ContainsKey(claseNombreNuevo))
            {
                Actividades.Add(claseNombreNuevo, new Dictionary<string, List<Actividad>>());
                foreach (var tipo in Actividades[claseNombre])
                {
                    Actividades[claseNombreNuevo].Add(tipo.Key, new List<Actividad>());
                    foreach (var actividad in tipo.Value)
                    {
                        Actividades[claseNombreNuevo][tipo.Key].Add(actividad);
                    }
                }
                Actividades.Remove(claseNombre);
            }
            
            //Actualizar en alumnos
            foreach (var alumno in Alumnos)
            {
                alumno.Clase_ActualizarNombre(claseNombre, claseNombreNuevo);
                alumno.Clase_ActualizarPorcentaje(claseNombre, clasePorcentaje);
            }


            Controlador_ActualizarPromedios();
        }


        //CONTROLADOR PARÁMETROS DE EVALUACION
        public void Controlador_EvaluacionParametro_Crear(string claseNombre, Dictionary<string, double> parametros)
        {
            //Alumnos
            foreach (var alumno in Alumnos)
            {
                alumno.Tipos_Crear(claseNombre, parametros);
            }
            //Clases
            foreach (var parametro in parametros)
            {
                int index = Clases.IndexOf(Clases.Where(p => p.Nombre == claseNombre).FirstOrDefault());
                Clases[index].PromediosTipo_Crear(parametro.Key, 0, parametro.Value);
            }
            //Actividades
            foreach (var parametro in parametros)
            {
                Actividades_Parametro_Crear(claseNombre, parametro.Key);
            }

            Controlador_ActualizarPromedios();
        }
        public void Controlador_EvaluacionParametro_Eliminar(string claseNombre, List<string> parametros)
        {
            //Alumnos
            foreach (var alumno in Alumnos)
            {
                alumno.Tipos_Eliminar(claseNombre, parametros);
            }
            //Clases
            foreach (var parametro in parametros)
            {
                int index = Clases.IndexOf(Clases.Where(p => p.Nombre == claseNombre).FirstOrDefault());
                Clases[index].PromediosTipo_Eliminar(parametro);
            }
            //Actividades

            foreach (var parametro in parametros)
            {
                Actividades_Parametro_Eliminar(claseNombre, parametro);
            }

            Controlador_ActualizarPromedios();
        }
        public void Controlador_EvaluacionParametro_ActualizarPorcentajeNombre(string claseNombre, Dictionary<string, Dictionary<string, double>> parametros)
        {
            //<viejoNombre, <nuevoNombre, valor>>
            foreach (var nombres in parametros)
            {
                foreach (var parametro in nombres.Value)
                {
                    //Alumnos
                    foreach (var alumno in Alumnos)
                    {
                        alumno.Tipos_ActualizarNombre(claseNombre, new Dictionary<string, string>() { { nombres.Key, parametro.Key } } );
                        alumno.Tipos_ActualizarPorcentajes(claseNombre, new Dictionary<string, double>() { { parametro.Key, parametro.Value } });
                    }
                    //Clases
                    int index = Clases.IndexOf(Clases.Where(p => p.Nombre == claseNombre).FirstOrDefault());
                    Clases[index].PromediosTipo_ActualizaNombre(nombres.Key, parametro.Key);
                    Clases[index].PromediosTipo_ActualizaPorcentaje(nombres.Key, parametro.Value);
                    //Actividades
                    Actividades_Parametro_ActualizarNombre(claseNombre, nombres.Key, parametro.Key);
                }
            } 
            Controlador_ActualizarPromedios();
        }


        //CONTROLADOR ACTIVIDADES
        public void Controlador_Actividad_Crear(string claseNombre, string actividadNombre, string actividadTipo, string actividadInstruccion,
            DateTime actividadFechaAsignacion, DateTime actividadFechaEntrega, string actividadNota,
            double actividadPorcentaje, List<string> actividadRecursos)
        {
            //Crear en Actividades
            List<string> ListaTipos = new List<string>();
            int index = Clases.IndexOf(Clases.Where(p => p.Nombre == claseNombre).FirstOrDefault());
            foreach (var tipo in Clases[index].PromediosTipo)
            {
                ListaTipos.Add(tipo.Key);
            }
            Actividades[claseNombre][actividadTipo].Add(new Actividad(actividadNombre, actividadTipo, actividadInstruccion,
                actividadFechaAsignacion, actividadFechaEntrega, actividadNota, actividadPorcentaje, actividadRecursos, ListaTipos));

            //Crear en Alumnos
            foreach (var alumno in Alumnos)
            {
                alumno.Actividad_Crear(claseNombre, actividadTipo, actividadNombre, actividadPorcentaje);
            }
            Controlador_ActualizarPromedios();
        }
        public void Controlador_Actividad_Eliminar(string claseNombre, string tipo, string actividadNombre)
        {
            //Eliminar en Actividades
            int index = Actividades[claseNombre][tipo].IndexOf(Actividades[claseNombre][tipo].Where(p => p.Nombre == actividadNombre).FirstOrDefault());
            Actividades[claseNombre][tipo].RemoveAt(index);

            //Eliminar en Alumnos
            foreach (var alumno in Alumnos)
            {
                alumno.Actividad_Eliminar(actividadNombre, tipo, actividadNombre);
            }
            Controlador_ActualizarPromedios();
        }
        public void Controlador_Actividad_ActualizarDatos(string claseNombre, string actividadNombreViejo, string actividadnombreNuevo, 
            string actividadTipoViejo, string actividadTipoNuevo, string actividadInstruccion, DateTime actividadFechaAsignacion, DateTime actividadFechaEntrega, 
            string actividadNota, double actividadPorcentaje)
        {
            //Crear en Actividades
            int index = Actividades[claseNombre][actividadTipoViejo].IndexOf(Actividades[claseNombre][actividadTipoViejo].Where(p => p.Nombre == actividadNombreViejo).FirstOrDefault());

            Actividades[claseNombre][actividadTipoViejo][index].ActualizarNombre(actividadnombreNuevo);
            Actividades[claseNombre][actividadTipoViejo][index].ActualizarInstruccion(actividadInstruccion);
            Actividades[claseNombre][actividadTipoViejo][index].ActualizarNota(actividadNota);
            Actividades[claseNombre][actividadTipoViejo][index].ActualizarPorcentaje(actividadPorcentaje);
            Actividades[claseNombre][actividadTipoViejo][index].ActualizarTipoActividad(actividadTipoNuevo);
            Actividades[claseNombre][actividadTipoViejo][index].ActualizarFechaAsignacion(actividadFechaAsignacion);
            Actividades[claseNombre][actividadTipoViejo][index].ActualizarFechaEntrega(actividadFechaEntrega);


            //Crear en Alumnos
            foreach (var alumno in Alumnos)
            {
                alumno.Actividad_ActualizarNombre(claseNombre, actividadTipoNuevo, actividadNombreViejo, actividadnombreNuevo);
                alumno.Actividad_ActividadActualizarPorcentaje(claseNombre, actividadTipoNuevo, actividadNombreViejo, actividadPorcentaje);
            }
            Controlador_ActualizarPromedios();
        }
        public void Controlador_Actividad_ActualizarCalificacion(string claseNombre, string tipoNombre, string actividadNombre, double calificacion, string alumnoId)
        {
            //Alumno
            int index = Alumnos.IndexOf(Alumnos.Where(p => p.Id == alumnoId).FirstOrDefault());
            Alumnos[index].Actividad_ActualizarCalificacion(claseNombre, tipoNombre, actividadNombre, calificacion);
            Controlador_ActualizarPromedios();
        }
        public void Controlador_Actividad_ActualizarPorcentaje(string claseNombre, string tipoNombre, string actividadNombre, double porcentaje)
        {
            //Alumno
            foreach (var alumno in Alumnos)
            {
                alumno.Actividad_ActividadActualizarPorcentaje(claseNombre, tipoNombre, actividadNombre, porcentaje);
                int index = Actividades[claseNombre][tipoNombre].IndexOf(Actividades[claseNombre][tipoNombre].Where(p => p.Nombre == actividadNombre).FirstOrDefault());
                Actividades[claseNombre][tipoNombre][index].ActualizarPorcentaje(porcentaje);
            }
            Controlador_ActualizarPromedios();
        }













        protected void Actividades_Parametro_Crear(string clase, string parametroNuevo)
        {
            Actividades[clase].Add(parametroNuevo, new List<Actividad>());
            foreach (var tipo in Actividades[clase])
            {
                foreach (var actividad in tipo.Value)
                {
                    actividad.TiposActividadOpciones_Agregar(parametroNuevo);
                }
            }
        }
        protected void Actividades_Parametro_Eliminar(string clase, string parametroEliminar)
        {
            Actividades[clase].Remove(parametroEliminar);   
            foreach (var tipo in Actividades[clase])
            {
                foreach (var actividad in tipo.Value)
                {
                    actividad.TiposActividadOpciones_Eliminar(parametroEliminar);
                }
            }

            //eliminar y no perder los datos
            //if (Actividades[clase][parametroEliminar].Count()>0)
            //{
            //    Actividades[clase].Add("Actividad", new List<Actividad>());
            //}
            //foreach (var tipo in Actividades[clase])
            //{
            //   foreach (var actividad in tipo.Value)
            //  {
            //      actividad.TiposActividadOpciones_Eliminar(parametroEliminar);
            //      if (actividad.TipoActividad == "Actividad")
            //      {
            //          Actividades[clase]["Actividad"].Add(actividad);
            //          Actividades[clase][parametroEliminar].Remove(actividad);
            //      }
            //  }
            //}
            // Actividades[clase].Remove(parametroEliminar);

        }
        protected void Actividades_Parametro_ActualizarNombre(string claseNombre, string tipoViejo, string tipoNuevo)
        {
            //actualiza los parámetros en todas las actividades
            foreach (var tipo in Actividades[claseNombre])
            {
                foreach (var actividad in tipo.Value)
                {
                    actividad.TiposActividadOpciones_ActualizarNombre(tipoViejo, tipoNuevo);
                }
            }
            //actualiza las listas de clase>tipo>actividad
            Actividades[claseNombre].Add(tipoNuevo, new List<Actividad>());
            foreach (var actividad in Actividades[claseNombre][tipoViejo])
            {
                Actividades[claseNombre][tipoNuevo].Add(actividad);
            }
            Actividades[claseNombre].Remove(tipoViejo);
        }










        public Clase ClonarClase(Clase clase)
        {
            Clase clasecopia = new Clase(clase.Nombre, clase.Color, clase.Horario, clase.Generacion);
            clasecopia.ActualizarClasePorcentaje(clase.ClasePorcentaje);
            foreach (var parametro in clase.PromediosTipo)
            {
                clasecopia.PromediosTipo_Crear(parametro.Key, parametro.Value["promedio"], parametro.Value["porcentaje"]);
            }
            clasecopia.ActualizarPromedio();
            return clasecopia;
        }

        public Actividad ClonarActividad(Actividad actividad)
        {
            Actividad actividadcopia = new Actividad(actividad.Nombre, actividad.TipoActividad, actividad.Instruccion,
                actividad.FechaAsignacion, actividad.FechaEntrega, actividad.Nota, actividad.Porcentaje, actividad.RecursosId, actividad.TipoActividadOpciones);
            actividadcopia.ActualizarPromedio(actividad.Promedio);
            return actividadcopia;
        }

        public List<Recurso> ClonarRecursosDeActividad(List<string> recursosIds)
        {
            List<Recurso> listaRecursos = new List<Recurso>();
            foreach (var id in recursosIds)
            {
                int index = Recursos.IndexOf(Recursos.Where(p => p.Id == id).FirstOrDefault());
                Recurso recurso = new Recurso(
                    Recursos[index].Nombre, Recursos[index].Descripcion, Recursos[index].Link, Recursos[index].Fecha,
                    new List<string>() { }, new List<string>() { }
                    );
                foreach(var etiqueta in Recursos[index].Etiquetas)
                {
                    recurso.Etiquetas.Add(etiqueta);
                }
                foreach (var carpeta in Recursos[index].Carpetas)
                {
                    recurso.Carpetas.Add(carpeta);
                }
                recurso.Id = Recursos[index].Id;
                listaRecursos.Add(recurso);
            }

            return listaRecursos;
        }

        public Alumno ClonarAlumno(Alumno alumno)
        {
            Alumno alumnoCopia = new Alumno(alumno.Nombres, alumno.Apellidos, alumno.Apodo, alumno.FechaNacimiento, 
                alumno.Grupo, alumno.Nota, alumno.NumeroLista);
            return alumnoCopia;
        }



        public void OrdenarAlumnos(string parametro)
        {   //ESTE MÉTODO CON LINQ USA ORDERBY Y CREA UNA NEUVA LISTA
            //List<Alumno> listOrdenada = lista.OrderBy(o => o.Nombres[0]).ToList();

            //LO MISMO PERO ORDEN INVERSO
            //List<Alumno> listOrdenada = lista.OrderByDescending(o => o.Nombres[0]).ToList();

            //SORT ORDENA TU LISTA EN LUGAR DE CREAR UNA NUEVA


            if (parametro == "Nombres ascendente") Alumnos.Sort((x, y) => x.Nombres[0].CompareTo(y.Nombres[0]));
            if (parametro == "Nombres descendente") Alumnos.Sort((x, y) => y.Nombres[0].CompareTo(x.Nombres[0]));
            if (parametro == "Apellidos ascendente") Alumnos.Sort((x, y) => x.Apellidos[0].CompareTo(y.Apellidos[0]));
            if (parametro == "Apellidos descendente") Alumnos.Sort((x, y) => y.Apellidos[0].CompareTo(x.Apellidos[0]));
            if (parametro == "Número de lista Ascendente") Alumnos.Sort((x, y) => x.NumeroLista.CompareTo(y.NumeroLista));
            if (parametro == "Número de lista descendente") Alumnos.Sort((x, y) => y.NumeroLista.CompareTo(x.NumeroLista));
            if (parametro == "Promedio Ascendente") Alumnos.Sort((x, y) => x.PromedioGeneral.CompareTo(y.PromedioGeneral));
            if (parametro == "Promedio descendente") Alumnos.Sort((x, y) => y.PromedioGeneral.CompareTo(x.PromedioGeneral));
            if (parametro == "Apodo Ascendente") Alumnos.Sort((x, y) => x.Apodo.CompareTo(y.Apodo));
            if (parametro == "Apodo descendente") Alumnos.Sort((x, y) => y.Apodo.CompareTo(x.Apodo));



        }




        //Faltan asistencias y recursos














    }
}
