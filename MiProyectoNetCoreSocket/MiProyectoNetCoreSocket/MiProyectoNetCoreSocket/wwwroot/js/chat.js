var socket;
var chat;
var contador = 0;
window.onload = function () {
	var urlsocket = get("urlSocket")
	socket = new WebSocket(urlsocket);
	listar(contador);
	var chat = document.getElementById("chat_s");
	chat.addEventListener("scroll", event => {
		if (chat.scrollTop == 0) {
			contador = contador + 1;
			listar(contador)
		}
	})
	socket.onmessage = function (rpta) {
		var data = rpta.data;
		if (data.includes("_")) {
			//[nombreusuario,mensaje]
			var array = data.split("_")
			var mensaje = array[1]
			var nombreusuario = array[0]
			var cadena = `    <div class="chat chat_other"><div class="chat_message">${mensaje}</div><div class="chat_name">${nombreusuario}</div></div>
						`
			chat.insertAdjacentHTML("beforeend", cadena)
			chat.scrollTop = chat.scrollHeight;

		}

	}
}

function listar(cont) {

	fetchGet("Chat/listarChat/?pag=" + cont, "json", function (rpta) {
		chat = document.getElementById("chat_s");
		var contenido = "";
		var scrollActual = chat.scrollHeight
		for (var i = rpta.length - 1; i >= 0; i--) {
			var obj = rpta[i]
			contenido += `<div class="chat chat_other">
									<div class="chat_message">${obj.mensaje}</div>
									<div class="chat_name">${obj.nombreusuario}</div>
						  </div>`
		}
		chat.insertAdjacentHTML("afterbegin", contenido)
		if (cont == 0)
			chat.scrollTop = chat.scrollHeight;
		else
			chat.scrollTop = chat.scrollHeight - scrollActual

	})

}


function Enviar() {
	var chat = document.getElementById("chat_s");
	var nombreusuario = get("txtnombreusuario")
	var mensaje = get("txtmensaje")
	if (nombreusuario == "") {
		Error("Debe ingresar el nombre del usuario que enviara el mensaje");
		return;
	}
	if (mensaje == "") {
		Error("Debe escribir un mensaje");
		return;
	}

	var frm = new FormData();
	frm.append("mensaje", mensaje)
	frm.append("nombreusuario", nombreusuario)

	fetchPost("Chat/guardarMensaje", "text", frm, function (rpta) {
		if (rpta == 1) {
			socket.send(nombreusuario + "_" + mensaje)
			set("txtmensaje", "")
		}
	})


}