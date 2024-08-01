var socket;
window.onload = function () {
    var urlsocket = get("urlSocket")
    socket = new WebSocket(urlsocket);
    socket.onmessage = function (rpta) {
        var data = rpta.data
        if (data == "eliminarproducto" || data == "guardarproducto") {
            listarCarrito()
        }
    }
    listarCarrito();
}

function Buscar() {
    listarCarrito()
}

function listarCarrito() {
    var descripcion = get("txtdescripcion")
    var url = descripcion == "" ? "Carrito/listarProductos" : "Carrito/listarProductos/?descripcion=" + descripcion
    fetchGet(url, "json", function (data) {
        var contenido = "";
        contenido += pintarCarrito(data)
        document.getElementById("divCarrito").innerHTML = contenido;
    })
}

function pintarCarrito(data) {
    var contenido = `<div class="row">`;
    for (var i = 0; i < data.length; i++) {
        var obj = data[i];
        contenido += `
            <div class='col-md-4 mt-2 mb-2 mr-2'>
                <div class="card" style="width: 20rem;">
                    <img style="height:200px;object-fit:contain" src="${obj.foto}" class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title text-truncate mb-3">${obj.description}</h5>
                        <p class="card-text d-flex justify-content-between">
                            <b class='h4'>${obj.preciocadena}</b>
                            <b class='h4'>${obj.stockcadena}</b>
                        </p>
                        <div class="text-center">
                            <button class="btn btn-primary" onclick="abrirVentanaModal('${obj.description}')">Inscribirse</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
    contenido += `</div>`;
    return contenido;
}

function abrirVentanaModal(description) {
    document.getElementById("modalDescription").innerText = description;
    var modal = new bootstrap.Modal(document.getElementById('nombreModal'), {});
    modal.show();
}

function traerDatos() {
    let dni = document.getElementById("dni").value;
    fetch("https://apiperu.dev/api/dni/" + dni + "?api_token=7c22a230e94af4dc17f27a9c7836a5705fa37fb10d2ddee4ca6e31c933c70752")
        .then((response) => response.json())
        .then((datos) => {
            console.log(datos.data);
            document.getElementById("nombre").value = datos.data.nombres;
            document.getElementById("apellido").value = datos.data.apellido_paterno + " " + datos.data.apellido_materno;
        })
        .catch(error => console.error('Error:', error));
}

document.getElementById("buscarDniBtn").addEventListener("click", traerDatos);

function enviarNombre() {
    var nombre = document.getElementById('nombre').value; // Usar el nombre ya obtenido
    var apellido = document.getElementById('apellido').value; // Usar el apellido ya obtenido
    var modal = bootstrap.Modal.getInstance(document.getElementById('nombreModal'));
    modal.hide();
    limpiarModal();
    mostrarAlertaRegistro();
}

function mostrarAlertaRegistro() {
    var exitoModal = new bootstrap.Modal(document.getElementById('exitoModal'), {});
    exitoModal.show();
}

function limpiarModal() {
    document.getElementById("dni").value = "";
    document.getElementById("nombre").value = "";
    document.getElementById("apellido").value = "";
}

