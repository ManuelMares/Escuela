function CopiarLink(link) {
    //sólo se puede copiar al portapapeles desde un input tipo text. Por éso lo creo y lo destruyo sólo para copiar
    var texto = link

    $('#tablaRecursos').append(`<input type="text" value="${texto}" id="myInput">`);

    var copyText = document.getElementById("myInput");
    copyText.select();
    copyText.setSelectionRange(0, 99999)
    document.execCommand("copy");
    $('#myInput').remove();
}

function CopiarInfoRecurso(nombre, link, fecha) {
    //sólo se puede copiar al portapapeles desde un input tipo text. Por éso lo creo y lo destruyo sólo para copiar
    $('#tablaRecursos').append(`<input type="text" value="Nombre: ${nombre}, \r\n
    Link: ${link} \r\n
    Fecha : ${fecha} " id="myInput">`);
    var copyText = document.getElementById("myInput");
    copyText.select();
    copyText.setSelectionRange(0, 99999)
    document.execCommand("copy");
    $('#myInput').remove();
}



function openNav() {
  document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
  document.getElementById("mySidenav").style.width = "0";
}
