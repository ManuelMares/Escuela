// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Escuela.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using Escuela;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Manuel\source\repos\Escuela\Escuela\_Imports.razor"
using Escuela.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Manuel\source\repos\Escuela\Escuela\Pages\Maestro_VisorRecursos.razor"
using Escuela.Data;

#line default
#line hidden
#nullable disable
    public partial class Maestro_VisorRecursos : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 370 "C:\Users\Manuel\source\repos\Escuela\Escuela\Pages\Maestro_VisorRecursos.razor"
       
    ///Un elemento se puede agregar a más de una carpeta y puede tener múltiples etiquetas
    ///Cada vez que se agrega una etiquea o carpeta, estos nombres se agregarán a las listas Carpetas y Etiquetas
    ///Los recursos se pueden filtrar por carpetas y etiquetas, y además de poder introducir los nombres manualmente
    ///Debe proporcionarse una lista de las Carpetas y Etiquetas disponibles. Por éso dichas listas
    ///
    ///Cada recurso tiene también las listas de Carpetas y Etiquetas, pero no son iguales a las antes mencionadas
    ///Sino que como pueden pertenecer a más de una lista o más de una carpeta, así que ellos mismos saben a cuáles


    public bool Bandera = false;
    public string Ver = "";
    public bool Modal = false;
    public bool Modal_CrearRecurso = false;
    public bool Modal_Noticia = false;
    public Recurso recursoModal;
    public string tipoFiltro;
    public Recurso nuevoRecurso = new Recurso("", "", "", Fecha, new List<string>() { }, new List<string>() { });
    public int cantidad_NuevasCarpetas = 0;
    public int cantidad_NuevasEtiquetas = 0;
    public List<string> nuevasEtiquetas = new List<string>() { };
    public List<string> nuevasCarpetas = new List<string>() { };

    public string modalNoticia_Titulo = "";
    public string modalNoticia_Mensaje = "";


    public static DateTime Fecha = new DateTime(2021, 01, 25);

    public List<Recurso> Recursos = new List<Recurso>();
    public List<Recurso> RecursosFiltrados = new List<Recurso>();
    public string filtroNombre;

    private static string biology = "Biology";
    private static string grammar = "Grammar";
    private static string spelling = "Spelling";
    private static string listening = "Listening";
    private static string quiz = "Quiz";
    private static string homework = "Homework";
    private static string seesaw = "Seesaw";
    private static string schoolastic = "Schoolastic";
    private static List<string> listEti = new List<string>() { "Biology", "Grammar", "quiz" };
    private static List<string> listCarp = new List<string>() { "Biology" };
    private static List<string> filtroEtiquetas = new List<string>() { };
    private static List<string> filtroCarpetas = new List<string>() { };
    private List<string> Carpetas = new List<string>() { biology, grammar, spelling, listening };
    private List<string> Etiquetas = new List<string>() { biology, grammar, spelling, listening, quiz, homework, seesaw, schoolastic };


    protected override async Task OnInitializedAsync()
    {
        CargarRecursos();

        Bandera = true;
    }

    private void CargarRecursos()
    {
        Recursos.Add(new Recurso("recurso1", "recurso1", "Link 1", Fecha, listEti, listCarp));
        Recursos.Add(new Recurso("recurso2", "recurso2", "Link 1", Fecha, new List<string>() { biology, grammar, homework, listening }, listCarp));
        Recursos.Add(new Recurso("recurso3", "recurso3", "Link 1", Fecha, new List<string>() { listening, quiz, schoolastic, grammar }, listCarp));
        Recursos.Add(new Recurso("recurso5", "recurso5", "Link 1", Fecha, new List<string>() { listening, spelling, homework, seesaw }, listCarp));
        Recursos.Add(new Recurso("recurso6", "recurso6", "Link 1", Fecha, new List<string>() { biology, quiz, grammar, seesaw }, listCarp));
        Recursos.Add(new Recurso("recurso7", "recurso7", "Link 1", Fecha, new List<string>() { biology, quiz, homework, spelling }, listCarp));
        Recursos.Add(new Recurso("recurso8", "recurso8", "Link 1", Fecha, new List<string>() { listening, grammar, homework, schoolastic }, listCarp));
        Recursos.Add(new Recurso("recurso9", "recurso9", "Link 1", Fecha, new List<string>() { listening, quiz, homework, schoolastic }, listCarp));
        Recursos.Add(new Recurso("recurso10", "recurso10", "Link 1", Fecha, new List<string>() { spelling, grammar, listening, seesaw }, listCarp));
        Recursos.Add(new Recurso("recurso11", "recurso11", "Link 1", Fecha, new List<string>() { grammar, quiz, homework, schoolastic }, listCarp));
        foreach (var elemento in Carpetas)
        {
            filtroCarpetas.Add(elemento);
        }
    }

    private void VerCarpetas()
    {
        Ver = "carpetas";
    }

    private void VerRecursos()
    {
        Ver = "recursos";
        RecursosFiltrados.Clear();
        foreach (var recurso in Recursos)
        {
            RecursosFiltrados.Add(recurso);
        }
    }


    private void AgregarEtiqueta(string etiq, object casilla)
    {
        if ((bool)casilla)
        {
            if (!filtroEtiquetas.Contains(etiq))
            {
                filtroEtiquetas.Add(etiq);
            }

        }
        else
        {
            if (filtroEtiquetas.Contains(etiq))
            {
                filtroEtiquetas.Remove(etiq);
            }
        }

    }


    private void AgregarCarpeta(string carp, object casilla)
    {
        if ((bool)casilla)
        {
            if (!filtroCarpetas.Contains(carp))
            {
                filtroCarpetas.Add(carp);
            }

        }
        else
        {
            if (filtroCarpetas.Contains(carp))
            {
                filtroCarpetas.Remove(carp);
            }
        }

    }




    private void ElegirFiltro(string filtro)
    {
        tipoFiltro = filtro;
    }

    private void AplicarFiltros()
    {
        RecursosFiltrados.Clear();
        foreach (var recurso in Recursos)
        {

            foreach (var elemento in filtroEtiquetas)
            {
                if (recurso.Etiquetas.Contains(elemento))
                {
                    RecursosFiltrados.Add(recurso);
                }
            }


            foreach (var elemento in filtroCarpetas)
            {
                if (recurso.Carpetas.Contains(elemento) && !RecursosFiltrados.Contains(recurso))
                {
                    RecursosFiltrados.Add(recurso);
                }
            }


        }
    }

    private void filtrarPorNombre()
    {
        RecursosFiltrados.Clear();
        if (filtroNombre != null)
        {
            foreach (var recurso in Recursos)
            {
                if (recurso.Nombre.Equals(filtroNombre, StringComparison.InvariantCultureIgnoreCase))
                {
                    RecursosFiltrados.Add(recurso);
                }
            }
        }


    }



    #region //MOSTRAR MODALES

    //Agregar nuevo recurso
    private void Activar_ModalCrearRecurso()
    {
        Modal_CrearRecurso = true;
    }


    //estos dos métodos agregan las etiquetas del checkbox
    private void AgregarEtiqueta_NuevoRecurso(string etiqueta)
    {
        nuevoRecurso.Etiquetas.Add(etiqueta);
    }

    private void AgregarCarpeta_NuevoRecurso(string carpeta)
    {
        nuevoRecurso.Carpetas.Add(carpeta);
    }


    //Estos dos métodos agregan las nuevas etiquetas creadas a una lista de etiquetas
    private void AgregarEtiquetaNueva_NuevoRecurso(string etiqueta)
    {
    }

    private void AgregarCarpetaNueva_NuevoRecurso(string carpeta)
    {
    }


    //Estos dos métodos son solo CONTADORES para la cantidad de nuevas etiquetas
    private void AgregarNuevaEtiqueta()
    {
        cantidad_NuevasEtiquetas++;
        nuevasEtiquetas.Add("");
        //int contador = (nuevasEtiquetas.Count - 1);
        //string href = "NuevaEtiquetaFocus" + contador;
        //EnfocarEtiqueta();
    }

    private void AgregarNuevaCarpeta()
    {
        cantidad_NuevasCarpetas++;
        nuevasCarpetas.Add("");
    }



    private void AgregarEtiquetaViejoRecurso(string id)
    {
        int index = Recursos.FindIndex(r => r.Id == id);
        Recursos[index].Etiquetas.Add("");
    }

    private void AgregarCarpetaViejoRecurso(string id)
    {
        int index = Recursos.FindIndex(r => r.Id == id);
        Recursos[index].Carpetas.Add("");
    }




    /// <summary>
    /// este método:
    /// * asigna las etiquetas y carpetas creadas al nuevo elemento recurso
    /// * agregar las NUEVAS etiquetas y carpetas a sus listas generales
    /// * valida que no haya parámetros vacíos
    /// * agrega el nuevo recurso a la lista de recursos
    /// * cierra el modal
    /// * reinicia el recurso y las listas de nuevas etiquetas y carpetas
    /// * actualiza la interfaz
    /// </summary>
    private void CrearNuevoRecurso()
    {
        bool validado = false;
        List<string> agregarEtiquetas = new List<string>() { };
        List<string> agregarCarpetas = new List<string>() { };
        foreach (var elemento in nuevasEtiquetas)
        {
            bool igual = false;
            if (elemento != "")
            {
                foreach (var elem in Etiquetas)
                {
                    if (elem == elemento)
                    {
                        igual = true;
                    }
                }
            }

            if (igual == false)
            {
                agregarEtiquetas.Add(elemento);
            }

            nuevoRecurso.Etiquetas.Add(elemento);

        }

        foreach (var elemento in nuevasCarpetas)
        {
            bool igual = false;
            if (elemento != "")
            {
                foreach (var elem in Carpetas)
                {
                    if (elem == elemento)
                    {
                        igual = true;
                    }
                }
            }
            if (igual == false)
            {
                agregarCarpetas.Add(elemento);
            }

            nuevoRecurso.Carpetas.Add(elemento);
        }

        foreach (var elemento in agregarEtiquetas)
        {
            Etiquetas.Add(elemento);
        }

        foreach (var elemento in agregarCarpetas)
        {
            Carpetas.Add(elemento);
        }

        if (nuevoRecurso.Nombre != null && nuevoRecurso.Nombre != "")
        {
            //if (nuevoRecurso.Tipo != null && nuevoRecurso.Tipo != "")
            //{
                //Usa .any() para validar que haya etiquetas y carpetas
                //validado = true;
            //}

        }

        if (validado == true)
        {
            Recursos.Add(nuevoRecurso);
            Modal_CrearRecurso = false;
            nuevoRecurso = new Recurso("", "", "", Fecha, new List<string>() { }, new List<string>() { });
            cantidad_NuevasEtiquetas = 0;
            cantidad_NuevasCarpetas = 0;
            nuevasEtiquetas = new List<string>() { };
            nuevasCarpetas = new List<string>() { };
            StateHasChanged();
            modalNoticia_Titulo = "Se ha creado un recurso";
            modalNoticia_Mensaje = nuevoRecurso.Nombre;
            MostrarNotificacion();
        }


    }


    //Mostrar Recurso
    private void MostrarModal(Recurso recurs)
    {
        Modal = true;
        recursoModal = recurs;
    }

    private void OcultarModalRecurso(string id)
    {
        Modal = false;
        int elementosEliminados = 0;
        int parametroAgregado = 0;
        int index = Recursos.FindIndex(r => r.Id == id);
        elementosEliminados += Recursos[index].Etiquetas.RemoveAll(r => r == "");
        elementosEliminados += Recursos[index].Carpetas.RemoveAll(c => c == "");
        foreach (var etiqNueva in Recursos[index].Etiquetas)
        {
            int coindicidencia = 0;
            foreach(var etiqVieja in Etiquetas)
            {
                if(etiqNueva == etiqVieja)
                {
                    coindicidencia++;
                }
            }
            if(coindicidencia == 0)
            {
                Etiquetas.Add(etiqNueva);
                parametroAgregado++;
            }
        }

        foreach (var carpNueva in Recursos[index].Carpetas)
        {
            int coindicidencia = 0;
            foreach (var carpVieja in Carpetas)
            {
                if (carpNueva == carpVieja)
                {
                    coindicidencia++;
                }
            }
            if (coindicidencia == 0)
            {
                Carpetas.Add(carpNueva);
                parametroAgregado++;
            }
        }

        InvokeAsync(() => StateHasChanged());

        if (elementosEliminados > 0)
        {
            modalNoticia_Titulo = "Se ha actualizado un recurso";
            modalNoticia_Mensaje = Recursos[index].Nombre;
            MostrarNotificacion();
        }
        else
        {
            if (parametroAgregado > 0)
            {
                modalNoticia_Titulo = "Se ha actualizado un recurso";
                modalNoticia_Mensaje = Recursos[index].Nombre;
                MostrarNotificacion();
            }
        }
    }


    private void OcultarModal()
    {
        Modal = false;
        Modal_CrearRecurso = false;

        nuevoRecurso = new Recurso("", "", "", Fecha, new List<string>() { }, new List<string>() { });
        nuevasCarpetas = new List<string>() { };
        nuevasEtiquetas = new List<string>() { };
    }

    #endregion





    async Task CopiarLink(string link)
    {
        string texto = link;
        await JsRuntime.InvokeVoidAsync("CopiarLink", texto);

        modalNoticia_Titulo = "Se ha copiado el link de un recurso: ";
        modalNoticia_Mensaje += link;
        MostrarNotificacion();
    }

    async Task CopiarInfoRecurso(Recurso recurso)
    {
        string nombre = recurso.Nombre;
        string link = recurso.Link;
        string fecha = recurso.Fecha.ToString("dd/MM/yyyy");
        await JsRuntime.InvokeVoidAsync("CopiarInfoRecurso", nombre, link, fecha);

        modalNoticia_Titulo = "Se ha copiado la descripcion de un recurso: ";
        modalNoticia_Mensaje = nombre;
        modalNoticia_Mensaje += "  ";
        modalNoticia_Mensaje += link;
        modalNoticia_Mensaje += "  ";
        modalNoticia_Mensaje += fecha;
        MostrarNotificacion();
    }



    private void EliminarRecurso(string id)
    {
        int index = Recursos.FindIndex(r => r.Id == id);
        string nombre = Recursos[index].Nombre;
        Recursos.RemoveAll(r => r.Id == id);
        RecursosFiltrados.RemoveAll(r => r.Id == id);
        InvokeAsync(() => StateHasChanged());
        modalNoticia_Titulo = "Se ha eliminado un recurso";
        modalNoticia_Mensaje = nombre;
        MostrarNotificacion();
    }

    private void EliminarRecursoViejo(string id)
    {
        int index = Recursos.FindIndex(r => r.Id == id);
        string nombre = Recursos[index].Nombre;
        OcultarModalRecurso(id);
        Recursos.RemoveAll(r => r.Id == id);
        RecursosFiltrados.RemoveAll(r => r.Id == id);
        modalNoticia_Titulo = "Se ha eliminado un recurso";
        modalNoticia_Mensaje = nombre;
        MostrarNotificacion();
        InvokeAsync(() => StateHasChanged());
    }

    private void EliminarEtiqueta(int contador, string id)
    {

        int index = Recursos.FindIndex(r => r.Id == id);

        int Eliminar = 0;
        foreach(var recurso in Recursos)
        {
            foreach(var etiquetaVerificar in recurso.Etiquetas)
            {
                if(etiquetaVerificar == Recursos[index].Etiquetas[contador])
                {
                    Eliminar++;
                }
            }
        }

        if(Eliminar == 1)
        {
            int indexEtiquetas = Etiquetas.FindIndex(r => r == Recursos[index].Etiquetas[contador]);
            Etiquetas.RemoveAt(indexEtiquetas);
        }

        Recursos[index].Etiquetas.RemoveAt(contador);

    }

    private void EliminarCarpeta(int contador, string id)
    {
        int index = Recursos.FindIndex(r => r.Id == id);

        int Eliminar = 0;
        foreach (var recurso in Recursos)
        {
            foreach (var carpetaVerificar in recurso.Carpetas)
            {
                if (carpetaVerificar == Recursos[index].Carpetas[contador])
                {
                    Eliminar++;
                }
            }
        }

        if (Eliminar == 1)
        {
            int indexCarpetas = Carpetas.FindIndex(r => r == Recursos[index].Carpetas[contador]);
            Carpetas.RemoveAt(indexCarpetas);
        }


        Recursos[index].Carpetas.RemoveAt(contador);


    }

    private void EliminarNuevaEtiqueta(int contador)
    {
        cantidad_NuevasEtiquetas = cantidad_NuevasEtiquetas - 1;
        nuevasEtiquetas.RemoveAt(contador);
    }



    public async void MostrarNotificacion()
    {
        renderizarNoticia();
        int Time = 4;
        while (Time > 0)
        {
            Time--;
            StateHasChanged();
            await Task.Delay(1000);
        }
        desrenderizarNoticia();
    }

    public void renderizarNoticia()
    {
        Modal_Noticia = true;
        InvokeAsync(() => StateHasChanged());
    }

    public void desrenderizarNoticia()
    {
        Modal_Noticia = false;
        InvokeAsync(() => StateHasChanged());
    }



#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JsRuntime { get; set; }
    }
}
#pragma warning restore 1591