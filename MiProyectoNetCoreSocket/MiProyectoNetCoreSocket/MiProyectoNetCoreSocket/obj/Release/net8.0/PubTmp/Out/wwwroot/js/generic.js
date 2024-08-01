
function get(idcontrol) {
    return document.getElementById(idcontrol).value;
}
function getI(idcontrol) {
    return document.getElementById(idcontrol).innerHTML;
}
function set(idcontrol, valor) {
    if (document.getElementById(idcontrol))
        document.getElementById(idcontrol).value = valor;
}
function setI(idcontrol, valor) {
    if (document.getElementById(idcontrol))
        document.getElementById(idcontrol).innerHTML = valor;
}
function setC(selector, valor = true) {
    if (document.querySelector(selector))
        document.querySelector(selector).checked = valor;
}
function setN(namecontrol, valor, idformulario) {
    if (idformulario == undefined) {
        document.getElementsByName(namecontrol)[0].value = valor;
    }
    else {
        document.querySelector("#" + idformulario + " [name='" + namecontrol + "']").value = valor;
    }
}
function setSRC(namecontrol, valor, idformulario) {
    if (idformulario == undefined) {
        document.getElementsByName(namecontrol)[0].src = valor;
    }
    else {
        document.querySelector("#" + idformulario + " [name='" + namecontrol + "']").src = valor;
    }
}
function recuperarGenerico(url, idformulario) {
    var elementosName = document.querySelectorAll("#" + idformulario + " [name]");
    var nombrename;
    fetchGet(url, "json", function (data) {
        for (var i = 0; i < elementosName.length; i++) {
            nombrename = elementosName[i].name;
            if ((elementosName[i].type != undefined && elementosName[i].type == "text")
                || elementosName[i].tagName.toUpperCase() == "TEXTAREA" ||
                elementosName[i].tagName.toUpperCase() == "SELECT" || elementosName[i].type == "number") {

                setN(nombrename, data[nombrename], idformulario)
            } else if (elementosName[i].tagName.toUpperCase() == "IMG") {
                elementosName[i].style.visibility = "visible";
                setSRC(nombrename, data[nombrename], idformulario)
            } else if (elementosName[i].type != undefined && elementosName[i].type == "radio") {
                setC("#" + idformulario + " [type='radio'][name='" + nombrename + "'][value='" + data[nombrename] + "']")
            } else if (elementosName[i].type != undefined && elementosName[i].type == "checkbox") {
                var name = nombrename.replace("[]", "");
                var valores = data[name]
                if (typeof (valores) == "object") {
                    var valor;
                    for (var j = 0; j < valores.length; j++) {
                        valor = valores[j];
                        setC("#" + idformulario + " [type='checkbox'][name='" + nombrename + "'][value='" + valor + "']")
                    }
                } else {
                    setC("#" + idformulario + " [type='checkbox'][name='" + nombrename + "'][value='" + valores + "']")
                }
            }
        }
    });
}

function validarKeyPress(idformulario) {

    var elementosNames = document.querySelectorAll("#" + idformulario + " [name]");
    var control, nombreclases, clases, cantidad;
    var resultado;
    for (var i = 0; i < elementosNames.length; i++) {
        control = elementosNames[i];
        control.onkeypress = function (e) {
            nombreclases = e.target.className;
            clases = nombreclases.split(" ");
            resultado = clases.filter(p => p == "sl")
            if (resultado.length > 0) {
                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                if (!/^[a-zA-Z]+$/.test(cadena)) {
                    e.preventDefault();
                }
            }
            resultado = clases.filter(p => p == "slcenb")
            if (resultado.length > 0) {
                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                if (!/^[a-zA-Z ]+$/.test(cadena)) {
                    e.preventDefault();
                }
            }
            resultado = clases.filter(p => p == "sn")
            if (resultado.length > 0) {

                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                if (!/^[0-9]+$/.test(cadena)) {
                    e.preventDefault();
                }

            }
            resultado = clases.filter(p => p == "snslcenb")
            if (resultado.length > 0) {

                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                if (!/^[a-zA-Z0-9(),;: ]+$/.test(cadena)) {
                    e.preventDefault();
                }
            }
            resultado = clases.filter(p => p.includes("max-"))
            if (resultado.length > 0) {
                var nombreClaseConMax = resultado[0]
                var valorMaximo = nombreClaseConMax.replace("max-", "") * 1
                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                var longitudTexto = cadena.length;
                if (longitudTexto > valorMaximo) {
                    e.preventDefault();
                }


            }
        }

    }
}



function ValidarDatos(idformulario) {
    var error = "";
    var contenedorchecks = document.querySelectorAll("#" + idformulario + " [class*='ob-']")
    var contenedor;
    for (var i = 0; i < contenedorchecks.length; i++) {
        contenedor = contenedorchecks[i];
        var numero = contenedor.className.replace("ob-", "") * 1;
        var marcados = 0;
        var hijos = contenedor.children;
        var hijo;
        for (var j = 0; j < hijos.length; j++) {
            hijo = hijos[j];
            if (hijo.type == "radio" && hijo.checked == true) marcados++;
        }
        if (marcados == 0) {
            error = "Debe seleccionar un valor";
            return error;
        }
    }
    var elementosNames = document.querySelectorAll("#" + idformulario + " [name]");
    var control, nombreclases, clases, cantidad;
    var resultado;
    for (var i = 0; i < elementosNames.length; i++) {
        control = elementosNames[i];
        if (control.parentNode.style.display != "none") {
            nombreclases = control.className;
            clases = nombreclases.split(" ");
            //Obligatorios
            resultado = clases.filter(p => p == "ob")
            if (resultado.length > 0) {
                if (control.tagName.toUpperCase() == "INPUT" || control.tagName.toUpperCase() == "TEXTAREA") {
                    if (control.value.trim() == "") {
                        error = "Debe ingresar el campo " + control.name;
                        return error;
                    }
                } else if (control.tagName.toUpperCase() == "SELECT") {
                    if (control.selectedIndex == 0) {
                        error = "Debe ingresar el campo " + control.name;
                        return error;
                    }
                } else if (control.tagName.toUpperCase() == "IMG") {
                    if (control.src == "" || control.src == window.location.href) {
                        error = "Debe ingresar la imagen ";
                        return error;
                    }
                }

            }
            resultado = clases.filter(p => p.includes("max-"))
            if (resultado.length > 0) {
                var nombreClaseConMax = resultado[0]
                var valorMaximo = nombreClaseConMax.replace("max-", "") * 1
                var longitudTexto = control.value.length
                if (longitudTexto > valorMaximo) {
                    error = "El campo  " + control.name + " su longitud maxima es " + valorMaximo +
                        " y usted a escrito una cadena con longitud " + longitudTexto;
                    return error;
                }
            }
            resultado = clases.filter(p => p.includes("min-"))
            if (resultado.length > 0) {
                var nombreClaseConMin = resultado[0]
                var valorMinimo = nombreClaseConMin.replace("min-", "") * 1
                var longitudTexto = control.value.length
                if (longitudTexto < valorMinimo) {
                    error = "El campo  " + control.name + " su longitud minima es " + valorMinimo +
                        " y usted a escrito una cadena con longitud " + longitudTexto;
                    return error;
                }
            }
            resultado = clases.filter(p => p == "sl")
            if (resultado.length > 0) {
                if (!/^[a-zA-ZÀ-ÿ]+$/.test(control.value)) {
                    error = "El campo " + control.name + " solo debe tener letras minusculas o mayusculas ";
                    return error
                }
            }
            resultado = clases.filter(p => p == "slcenb")
            if (resultado.length > 0) {
                if (!/^[a-zA-ZÀ-ÿ,; ]+$/.test(control.value)) {
                    error = "El campo " + control.name + " solo debe tener letras minusculas , mayusculas o espacio en blanco ";
                    return error
                }
            }
            resultado = clases.filter(p => p == "sn")
            if (resultado.length > 0) {
                if (!/^[0-9]+$/.test(control.value)) {
                    error = "El campo " + control.name + " solo debe tener numeros del 0 al 9 ";
                    return error
                }
            }
            resultado = clases.filter(p => p == "snslcenb")
            if (resultado.length > 0) {
                if (!/^[a-zA-Z0-9À-ÿ,;(): ]+$/.test(control.value)) {
                    error = "El campo " + control.name + " solo debe tener numeros , letras o espacios en blanco ";
                    return error
                }
            }
        }
    }
    return error;
}

function getN(namecontrol) {
    return document.getElementsByName(namecontrol)[0].value
}

function Error(titulo = "Error", texto = "Ocurrio un error") {
    if (titulo != "No transport could be initialized successfully. Try specifying a different transport or none at all for auto initialization."
        && titulo != "Error during negotiation request." && titulo != "Error parsing negotiate response."
    )
        Swal.fire({
            icon: 'error',
            title: titulo,
            text: texto
        })
}

function Confirmacion(titulo = "Confirmacion", texto = "Desea guardar los cambios?", callback) {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    })
}

function Exito(titulo = "Se guardo correctamente") {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: titulo,
        showConfirmButton: false,
        timer: 1500
    })
}

function LimpiarDatos(idformulario) {
    var elementosName = document.querySelectorAll("#" + idformulario + " [name]");
    var elementoActual;
    var elementoName;
    for (var i = 0; i < elementosName.length; i++) {
        elementoActual = elementosName[i]
        elementoName = elementoActual.name;
        if (elementoActual.tagName.toUpperCase() == "SELECT") {
            document.getElementById(elementoActual.id).selectedIndex = 0;
        }
        else if (elementoActual.tagName.toUpperCase() == "IMG") {
            setSRC(elementoName, "", idformulario)
        }
        else if ((elementoActual.tagName.toUpperCase() == "INPUT" && elementoActual.type.toUpperCase() != "RADIO")
            || (elementoActual.tagName.toUpperCase() == "TEXTAREA")) {
            setN(elementoName, "", idformulario);
        }

    }
    var radios = document.querySelectorAll("#" + idformulario + " [type*='radio']");
    for (var i = 0; i < radios.length; i++) {
        radios[i].checked = false;
    }
    var checboks = document.querySelectorAll("#" + idformulario + " [type*='checkbox']");
    for (var i = 0; i < checboks.length; i++) {
        checboks[i].checked = false;
    }
   
}

function setURL(url) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz
        + url

    return urlCompleta;
}

async function fetchGet(url, tiporespuesta, callback, retorno = false) {
    document.getElementById("divLoading").style.display = "block";
    var res
    var res1;
    try {
        var raiz = document.getElementById("hdfOculto").value;
        //http://localhost........
        var urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz
            + url
        res = await fetch(urlCompleta)
        res1 = res.clone();
        if (tiporespuesta == "json")
            res = await res.json();
        else if (tiporespuesta == "text")
            res = await res.text();

        document.getElementById("divLoading").style.display = "none";
        if (retorno == false || retorno == null)
            callback(res)
        else
            return res;
    } catch (e) {
        console.log(e)
        document.getElementById("divLoading").style.display = "none";
        var rpta = await res1.text();
        if (rpta != "")
            callback(rpta)
        else {
            alert("Ocurrion un error");
            return rpta;
        }

    }
}
function llenarCombo(data, idcontrol, propiedadId, propiedadNombre, textoprimeraopcion = "--Seleccione--", valueprimeraopcion = "") {

    var contenido = "";
    var objActual;

    contenido += "<option value='" + valueprimeraopcion + "'>" + textoprimeraopcion + "</option>"
    for (var i = 0; i < data.length; i++) {
        objActual = data[i];
        contenido += "<option value='" + objActual[propiedadId] + "'>" + objActual[propiedadNombre] + "</option>"
    }
    if (typeof (idcontrol) == "string")
        setI(idcontrol, contenido)
    else {
        for (var j = 0; j < idcontrol.length; j++) {
            setI(idcontrol[j], contenido);
        }
    }
}




async function fetchPost(url, tiporespuesta, frm, callback) {
    try {
        var raiz = document.getElementById("hdfOculto").value;
        document.getElementById("divLoading").style.display = "block";
        //http://localhost........
        var urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url
        var res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });
        if (tiporespuesta == "json")
            res = await res.json();
        else if (tiporespuesta == "text")
            res = await res.text();
        else if (tiporespuesta == "blob")
            res = await res.blob();
        callback(res)
        document.getElementById("divLoading").style.display = "none";

    } catch (e) {
        console.log(e)
        alert("Ocurrion un error");
        document.getElementById("divLoading").style.display = "none";
    }
}




var objConfiguracionGlobal;
var objBusquedaGlobal;
var objFormularioGlobal;
var dataCompleta;
function pintar(objConfiguracion) {

    var contenido = "";


    if (objConfiguracion != null) {
        if (objConfiguracion.divContenedorTabla == undefined)
            objConfiguracion.divContenedorTabla = "divContenedorTabla"
        if (objConfiguracion.divPintado == undefined)
            objConfiguracion.divPintado = "divTabla"
        if (objConfiguracion.editar == undefined)
            objConfiguracion.editar = false
        if (objConfiguracion.eliminar == undefined)
            objConfiguracion.eliminar = false
        if (objConfiguracion.propiedadId == undefined)
            objConfiguracion.propiedadId = ""

        if (objConfiguracion.type == undefined)
            objConfiguracion.type = ""
        if (objConfiguracion.columnreadonly == undefined)
            objConfiguracion.columnreadonly = []
        if (objConfiguracion.idtabla == undefined)
            objConfiguracion.idtabla = "tabla"


    }

    objConfiguracionGlobal = objConfiguracion;


    if (objConfiguracion != null) {
        fetchGet(objConfiguracion.url, "json", function (res) {

            dataCompleta = res;
            //........................................................
            contenido += `<div class='table-responsive' id = "${objConfiguracionGlobal.divContenedorTabla}">`;
            contenido += generarTabla(dataCompleta)
            contenido += "</div>"
            setI(objConfiguracion.divPintado, contenido)
            configurarPaginacion();
            manejoCheck();




        })
    }




}
var filasChecks = []
function manejoCheck() {
    if (objConfiguracionGlobal.check) {
        var checks = document.getElementsByClassName("Check");
        var nChecks = checks.length;
        var fila;
        var seleccionado;
        var pos;
        for (var i = 0; i < nChecks; i++) {
            checks[i].onchange = function () {
                seleccionado = this.checked;
                cod = this.className.replace("Check ", "") * 1;
                if (seleccionado) {
                    idsChecks.push(cod);
                    filasChecks.push(buscarCodigo(cod));
                }
                else {
                    pos = idsChecks.indexOf(cod);
                    if (pos > -1) {
                        idsChecks.splice(pos, 1);
                        filasChecks.splice(pos, 1);
                    }
                }
            }
        }
        var chkCabecera = document.getElementById("chkCabecera");
        if (chkCabecera != null) {
            chkCabecera.onchange = function () {
                var seleccionado = this.checked;
                if (!seleccionado) {
                    idsChecks = [];
                    filasChecks = [];
                }
                var fila;
                var cod;
                for (var i = 0; i < nChecks; i++) {
                    checks[i].checked = seleccionado;
                    if (seleccionado) {
                        fila = checks[i].parentNode.parentNode;
                        cod = fila.children[0].children[0].className.replace("Check ", "") * 1;
                        idsChecks.push(cod);
                        filasChecks.push(buscarCodigo(cod));
                    }
                }
            }
        }
    }
}

function buscarCodigo(id) {
    var fila = dataCompleta.filter(p => p[objConfiguracionGlobal.propiedadId] == id)[0]
    return fila;
}




var indicePagina = 0;
var paginasBloque = 3;
var indiceBloque = 0;
var registrosPagina = 6;
var idsChecks = [];
function generarTabla(res) {
    dataCompleta = res;
    var inicio = indicePagina * registrosPagina;
    var fin = inicio + registrosPagina;
    var contenido = "";
    contenido += `<table id='tablita' class='table'>`;
    contenido += "<thead>";
    contenido += "<tr>";
    if (objConfiguracionGlobal != undefined && objConfiguracionGlobal.check == true) {
        contenido += "<td style='width:30px'>";
        contenido += "<input id='chkCabecera";
        contenido += "' type='checkbox'/>";
        contenido += "</td>";
    }
    for (var i = 0; i < objConfiguracionGlobal.cabeceras.length; i++) {
        contenido += "<td>" + objConfiguracionGlobal.cabeceras[i] + "</td>";
    }
    if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true) {
        contenido += "<td>Operaciones</td>";
    }
    contenido += "</tr>";
    contenido += "</thead>"
    var nregistros = res.length;
    var obj;
    var propiedadActual;
    var existeIdCheck = false;
    contenido += "<tbody id='tbody'>";
    for (var i = inicio; i < fin; i++) {
        if (nregistros - 1 >= i) {
            obj = res[i]
            contenido += `<tr ${objConfiguracionGlobal != null && objConfiguracionGlobal.cursor != undefined ?
                "style='cursor:pointer'" : ''}

                        ${objConfiguracionGlobal != null && objConfiguracionGlobal.rowClickRecuperar != undefined ?
                    `onclick='rowClickRecuperarGenerico(${obj[objConfiguracionGlobal.propiedadId]})'
                    style='cursor:pointer'` : ""}

                        ${objConfiguracionGlobal != null && objConfiguracionGlobal.rowClick != undefined ?
                    `onclick='rowClickEvent(${JSON.stringify(obj)})'` : ""}
                  >`;
            if (objConfiguracionGlobal != undefined && objConfiguracionGlobal.check == true) {
                existeIdCheck = (idsChecks.indexOf(obj[objConfiguracionGlobal.propiedadId]) > -1);
                contenido += `<td><input type='checkbox'
                                 ${existeIdCheck == true ? "checked" : ""}
                                class='Check ${obj[objConfiguracionGlobal.propiedadId]}'
                             name="${objConfiguracionGlobal.propiedadId}[]"
                        value="${obj[objConfiguracionGlobal.propiedadId]}" /></td> `
            }
            for (var j = 0; j < objConfiguracionGlobal.propiedades.length; j++) {
                propiedadActual = objConfiguracionGlobal.propiedades[j]
                if (objConfiguracionGlobal.columnimg != undefined && objConfiguracionGlobal.columnimg.includes(propiedadActual)) {
                    contenido += "<td><img src='" + obj[propiedadActual] + "' style='width:100px;height:100px' /></td>";

                } else
                    contenido += "<td><span >" + obj[propiedadActual] + "</span></td>";
            }
            if (objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true
            ) {
                contenido += "<td>";
                if (objConfiguracionGlobal.editar == true) {
                    contenido += `
                        <button class='btn btn-primary' onclick='CallbackRecuperar(${obj[objConfiguracionGlobal.propiedadId]})'>
                            <svg xmlns = "http://www.w3.org/2000/svg" width = "16" height = "16" fill = "currentColor" class="bi bi-pen-fill" viewBox = "0 0 16 16" >
                                <path d="m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001z" />
                            </svg >
                        </button>
                    `
                }
                if (objConfiguracionGlobal.eliminar == true) {
                    contenido += `
                        <button class='btn btn-danger' onclick='CallbackEliminar(${obj[objConfiguracionGlobal.propiedadId]})'>
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                              <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z"/>
                            </svg>
                        </button>
                       `
                }
                contenido += "</td>"
            }

            contenido += "</tr>";
        }
    }
    contenido += "</tbody>"
    contenido += "<tfoot id='tdPagina'>";
    contenido += "</tfoot>";

    contenido += "</table>";
    return contenido;
}


function previewImage(idfupfoto, idimagen) {
    var fupFoto = document.getElementById(idfupfoto);
    var imgFoto = document.getElementById(idimagen);
    fupFoto.onchange = function () {
        //Primero
        var file = fupFoto.files[0];
        //Leer el archivo (imagen)
        var reader = new FileReader();
        //Cuando termina de leer entra al onloadend
        reader.onloadend = function () {
            imgFoto.src = reader.result;
        }
        reader.readAsDataURL(file)

    }
}

function configurarPaginacion() {
    var nRegistros = dataCompleta.length;
    var totalPaginas = Math.floor(nRegistros / registrosPagina);
    if (nRegistros % registrosPagina > 0) totalPaginas++;
    var html = "<tr><td colspan='2'>";
    if (totalPaginas > 1) {
        var totalRegistros = dataCompleta.length;
        var registrosBloque = registrosPagina * paginasBloque;
        var totalBloques = Math.floor(totalRegistros / registrosBloque);
        if (totalRegistros % registrosBloque > 0) totalBloques++;
        if (indiceBloque > 0) {
            html += "<button class='m-1 btn btn-danger Pag ";
            html += " Pagina' data-pag='-1'>";
            html += "<<";
            html += "</button>";
            html += "<button class='m-1 btn btn-danger Pag ";
            html += " Pagina' data-pag='-2'>";
            html += "<";
            html += "</button>";
        }
        var inicio = indiceBloque * paginasBloque;
        var fin = inicio + paginasBloque;
        for (var j = inicio; j < fin; j++) {
            if (j < totalPaginas) {
                html += "<button class='m-1  Pag ";
                html += " ";
                if (indicePagina == j) html += "btn btn-primary";
                else html += "btn btn-outline-primary";
                html += "' data-pag='";
                html += j;
                html += "'>";
                html += (j + 1);
                html += "</button> ";
            }
            else break;
        }
        if (indiceBloque < (totalBloques - 1)) {
            html += "<button class='m-1 btn btn-success Pag ";
            html += " Pagina' data-pag='-3'>";
            html += ">";
            html += "</button>";
            html += "<button class='m-1 btn btn-success Pag ";
            html += " Pagina' data-pag='-4'>";
            html += ">>";
            html += "</button>";
        }
        html += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        html += "</td></tr>";
    }
    var tdPagina = document.getElementById("tdPagina");
    if (tdPagina != null) {
        tdPagina.innerHTML = html;
        configurarEventosPagina();
    }
}

function configurarEventosPagina() {
    var paginas = document.getElementsByClassName("Pag ");
    var nPaginas = paginas.length;
    for (var j = 0; j < nPaginas; j++) {
        paginas[j].onclick = function () {
            paginar(+this.getAttribute("data-pag"));
        }
    }
}

function paginar(indice) {
    if (indice > -1) {
        indicePagina = indice;
        indiceBloque = Math.floor(indicePagina / paginasBloque);
    }
    else {
        var totalRegistros = dataCompleta.length;
        var registrosBloque = registrosPagina * paginasBloque;
        var totalBloques = Math.floor(totalRegistros / registrosBloque);
        if (totalRegistros % registrosBloque > 0) totalBloques++;
        switch (indice) {
            case -1: //Primer Bloque
                indiceBloque = 0;
                indicePagina = 0;
                break;
            case -2: //Bloque Anterior
                indiceBloque--;
                indicePagina = indiceBloque * paginasBloque;
                break;
            case -3: //Bloque Siguiente
                indiceBloque++;
                indicePagina = indiceBloque * paginasBloque;
                break;
            case -4: //Ultimo Bloque
                indiceBloque = (totalBloques - 1);
                indicePagina = indiceBloque * paginasBloque;
                break;
        }
    }
    var contenido = generarTabla(dataCompleta)
    document.getElementById(objConfiguracionGlobal.divContenedorTabla).innerHTML = contenido;
    configurarPaginacion()
    manejoCheck()
}



function CallbackEliminar(id) {
    Eliminar(id)
}

function CallbackRecuperar(id) {
    Editar(id)
}

function InicializarPaginacion() {
    indicePagina = 0;
    indiceBloque = 0;
}

function convertirBase64ToFile(dataurl, filename) {

    var arr = dataurl.split(','),
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]),
        n = bstr.length,
        u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime });
}

function convertirFileToBase64(file, callback) {
    if (file) {
        var filereader = new FileReader();
        filereader.readAsDataURL(file);
        filereader.onload = function (evt) {
            var base64 = evt.target.result;
            callback(base64)
        }
    }
}

