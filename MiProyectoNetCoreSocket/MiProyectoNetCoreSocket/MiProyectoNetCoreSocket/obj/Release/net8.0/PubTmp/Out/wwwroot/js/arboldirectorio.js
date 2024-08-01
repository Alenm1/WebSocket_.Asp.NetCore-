var socket;
window.onload = function () {
	var urlsocket = get("urlSocket")
	socket = new WebSocket(urlsocket);
	socket.onmessage = function (rpta) {
		var data = rpta.data
		if (data.startsWith("subir")) {
			var data = data.replace("subir", "")
			//file.name + "_" + nombreRuta
			var array = data.split("_")
			var nombrearchivo = array[0]
			var rutaarchivo = array[1]
			var contenido = "";
			contenido += `<li class="pintar" style="cursor:pointer;background-color:white" title="${nombrearchivo}"
									ondblclick="Descargar(this)" oncontextmenu="MenuContextualArchivo(event,this)"
							id="${rutaarchivo}\\${nombrearchivo}" >`
			contenido += nombrearchivo
			contenido += `</li>`
			var idexiste = document.getElementById(rutaarchivo + "\\" + nombrearchivo);
			if (idexiste == null || idexiste == undefined)
				document.getElementById(rutaarchivo).insertAdjacentHTML("beforeend", contenido)
		} else if (data.startsWith("rmv")) {
			var rutaArchivo = data.replace("rmv", "")
			var liClick = document.getElementById(rutaArchivo);
			if (liClick != undefined && liClick != null) {
				var urlPadre = liClick.parentNode;
				if (nombreRutaArchivo == rutaArchivo) {
					nombreRutaArchivo = "";
				}
				urlPadre.removeChild(liClick)
			}

		}
		else if (data.startsWith("updatedirectory")) {
			var data = data.replace("updatedirectory", "")
			var array = data.split("_")
			var rutadirectorio = array[0];
			var nombredirectorio = array[1];
			var idli = "dir_" + rutadirectorio;
			var li = document.getElementById(idli)
			li.childNodes[0].textContent = nombredirectorio;
			//  excel\\archivo
			var pos = rutadirectorio.lastIndexOf("\\")
			//rutaPadre-> excel
			var rutaPadre = rutadirectorio.substring(0, pos);
			var nombredirectorioanterior = rutadirectorio.substring(pos + 1);
			var nuevaRuta = "";
			if (rutaPadre != "") {
				nuevaRuta = rutaPadre + "\\" + nombredirectorio;
			} else {
				nuevaRuta = nombredirectorio;
			}
			actualizarIdArchivos(rutadirectorio, nuevaRuta);
			li.title = nombredirectorio;
			li.id = "dir_" + nuevaRuta;
			li.childNodes[1].id = nuevaRuta;

			if (nombreRuta == rutadirectorio) {
				nombreRuta = rutaPadre + "\\" + nombredirectorio;
				var textoescrito = get("txtnombredirectorio")
				if (textoescrito == nombredirectorioanterior) {
					set("txtnombredirectorio", nombredirectorio)
				}
			}
		}
		else if (data.startsWith("upd")) {
			var data = data.replace("upd", "")
			var array = data.split("_")
			var nombrerutaarchivoLocal = array[0];
			var nombrearchivo = array[1];
			var li = document.getElementById(nombrerutaarchivoLocal)
			li.innerText = nombrearchivo;
			li.title = nombrearchivo;
			var pos = nombrerutaarchivoLocal.lastIndexOf("\\")
			//rutaPadre-> excel
			var rutaPadre = nombrerutaarchivoLocal.substring(0, pos);
			li.id = rutaPadre + "\\" + nombrearchivo

			if (nombreRutaArchivo == nombrerutaarchivoLocal) {
				nombreRutaArchivo = rutaPadre + "\\" + nombrearchivo
			}

		} else if (data.startsWith("createDirectory")) {
			var data = data.replace("createDirectory", "")
			var array = data.split("_")
			var nombreruta = array[0];
			var nombredirectorio = array[1];
			var rutaCompleta = nombreruta + "\\" + nombredirectorio
			var ul = document.getElementById(nombreruta)
			var contenido = "";
			contenido += `<ul>`
			contenido += ` <li class="pintar" oncontextmenu="MenuContextual(event,this)" title="${nombredirectorio}"
								id="dir_${rutaCompleta}"  >${nombredirectorio}`
			contenido += `<ul style="display:block" id="${rutaCompleta}">`

			contenido += `</ul>`
			contenido += `</li>`
			contenido += `</ul>`
			var idexiste = document.getElementById("dir_" + rutaCompleta);
			if (idexiste == null || idexiste == undefined)
				ul.insertAdjacentHTML("beforeend", contenido);
			ArbolDinamico()

		} else if (data.startsWith("deletedirectory")) {
			var nombredirectorio = data.replace("deletedirectory", "")
			var ul = document.getElementById(nombredirectorio)
			var ulPadre = ul.parentNode.parentNode
			if (ulPadre != null && ulPadre != undefined) {
				var ulPadrePadre = ulPadre.parentNode;
				if (nombreRuta == nombredirectorio) {
					nombreRuta = "";
					set("txtnombredirectorio", "")
				}
				ulPadrePadre.removeChild(ulPadre)
			}

		}
	}
	listarArbol();
}

function listarArbol() {

	fetchGet("ArbolDirectorio/listarDirectorios", "json", function (rpta) {

		var contenido = `<ul class="directory-list" id="directory-list">`;
		contenido += `</ul>`
		document.getElementById("directlist").innerHTML = contenido;
		var obj;
		var cantidadArchivos;
		for (var i = 0; i < rpta.length; i++) {
			obj = rpta[i]
			cantidadArchivos = obj.listaNombrearchivos.length;
			if (!obj.rutaCompleta.includes("\\")) {
				contenido = "";
				contenido += `<li id="dir_${obj.rutaCompleta}">`
				contenido += obj.nombredirectorio
				contenido += `<ul>`
				for (var j = 0; j < cantidadArchivos; j++) {
					contenido += `<li style="cursor:pointer" title="${obj.listaNombrearchivos[j]}" ondblclick="Descargar(this)" 
							id="${obj.listaRutaarchivos[j]}" >`
					contenido += obj.listaNombrearchivos[j]
					contenido += `</li>`
				}
				contenido += `</ul>`
				contenido += `</li>`
				document.getElementById("directory-list").insertAdjacentHTML("beforeend", contenido)
			}
		}
		ArbolDinamico()
	})

}

function Descargar(li) {
	var ruta = li.id;
	var frm = new FormData();
	frm.append("ruta", ruta)
	fetchPost("ArbolDirectorio/descargarArchivo", "blob", frm, function (rpta) {
		var url = URL.createObjectURL(rpta);
		var a = document.createElement("a");
		a.href = url;
		a.download = li.title;
		a.click();
	})
}

function ArbolDinamico() {
	// obtener todas las carpetas en nuestra .directory-list
	var allFolders = $(".directory-list li > ul");
	allFolders.each(function () {

		// añadir la clase folder al <li> padre
		var folderAndName = $(this).parent();
		folderAndName.addClass("folder");

		// hacer una copia de seguridad de este <ul> interno
		var backupOfThisFolder = $(this);
		// luego eliminarlo
		$(this).remove();
		// añadir una etiqueta <a> a lo que queda, es decir, el nombre de la carpeta
		folderAndName.wrapInner("<a href='#' />");
		// luego poner el <ul> interno de vuelta
		folderAndName.append(backupOfThisFolder);

		// ahora añadir un slideToggle al <a> que acabamos de añadir
		folderAndName.find("a").click(function (e) {
			$(this).siblings("ul").slideToggle("slow");
			e.preventDefault();
		});

	});
}


