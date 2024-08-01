var socket;

window.onload = function () {
	var urlsocket = get("urlSocket")
	socket = new WebSocket(urlsocket);
	socket.onopen = function () {
		//Exito("Nos conectamos al socket")
	}
	socket.onclose = function () {
		Error("Se cerro la conexiòn");
	}
	socket.onerror = function () {
		Error("Ocurrio un error en el socket");
	}
	socket.onmessage = function (rpta) {
		var mensaje = rpta.data
		if (mensaje == "eliminarproducto" || mensaje == "guardarproducto")
		BuscarDatos(indicePagina, indiceBloque);
	}

	listarProductos();
	previewImage("fotoEnviar", "imgFoto")

}

function listarProductos() {
	pintar({
		url: "Producto/listarProductos",
		cabeceras: ["Descripcion Producto", "Precio Producto", "Vacantes"], //"Vacantes"
		propiedades: ["description", "preciocadena", "stockcadena"],
		editar: true,
		eliminar: true,
		propiedadId: "iidproducto"
	});
}

function BuscarDatos(indPag = 0, indBloq = 0) {
	var frmBusqueda = document.getElementById("frmBusqueda");
	var frm = new FormData(frmBusqueda);
	fetchPost("Producto/listarProductos", "json", frm, function (rpta) {
		indicePagina = indPag;
		indiceBloque = indBloq;
		document.getElementById("divContenedorTabla").innerHTML = generarTabla(rpta)
		configurarPaginacion();
	})
}

function Editar(id) {
	document.getElementById("btnNuevo").click();
	document.getElementById("lblTitulo").innerHTML = "Editar Producto";

	Limpiar()
	recuperarGenerico("Producto/recuperarProducto/?id=" + id, "frmProducto")
}

function Nuevo() {
	//LimpiarDatos("frmProducto")
	//document.getElementById("imgFoto").src = "";
	Limpiar()
	document.getElementById("lblTitulo").innerHTML = "Agregar Producto";
}

function Limpiar() {
	LimpiarDatos("frmProducto")
	document.getElementById("imgFoto").src = "";
}

function Eliminar(id) {
	//alert(id)
	Confirmacion("Confirmación", "¿Desea eliminar el curso?", function () {
		//Se ejecuta esto

		fetchGet("Producto/eliminarProducto/?id=" + id, "text", function (rpta) {
			if (rpta == 1) {
				socket.send("eliminarproducto")
				Exito("Se elimino correctamente");
				BuscarDatos(indicePagina, indiceBloque);
			} else {
				Error("Ocurrio un error");
			}
		})

	})


}

function Guardar() {
	var frmProducto = document.getElementById("frmProducto");
	var frm = new FormData(frmProducto);

	Confirmacion("Confirmación", "¿Desea guardar el curso?", function () {
		fetchPost("Producto/guardarProducto", "text", frm, function (rpta) {
			if (rpta == 1) {
				socket.send("guardarproducto")
				Exito("Se guardo correctamente");
				document.getElementById("btnCancelar").click();
				BuscarDatos(indicePagina, indiceBloque);
			} else {
				Error("Ocurrio un error");
			}
		})

	})
}
