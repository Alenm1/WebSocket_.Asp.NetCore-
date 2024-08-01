
var socket;
window.onload = function () {
    var urlsocket = get("urlSocket")
    socket = new WebSocket(urlsocket);
    socket.onmessage = function (rpta) {
        var data = rpta.data
        if (data.includes("*")) {
            var valores = data.split("*")
            var base = valores[0]
            var nombrearchivo = valores[1]
            //Convierto de base64 a File
            var archivoFile = convertirBase64ToFile(base, nombrearchivo)
            preview(archivoFile)
        } else if (data.startsWith("remove")) {
            var id = data.replace("remove", "");
            var col = document.getElementById(id);
            col.parentNode.removeChild(col);

        }

    }
    listarArchivos();
}

function listarArchivos() {

    fetchGet("Visor/listaArchivo", "json", function (data) {
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var fotoFile = convertirBase64ToFile(obj.database, obj.nombrearchivo)
            preview(fotoFile, 0)
        }
    })


}


var imagenes = {};
document.addEventListener('dragover', function (event) { event.preventDefault(); }, false);
//si los suelta en el document (por error) no hacer nada
document.addEventListener('drop', function (event) { event.preventDefault(); });
document.getElementById('to_upload').addEventListener('drop', readFiles, false);
document.getElementById('imagenes').addEventListener('change', readFiles, false);


function readFiles(event) {
    var archivos = event.target.files || event.dataTransfer.files;
    [].forEach.call(archivos, previewData);
}

function previewData(file) {
    //Para imagenes tenemos que pasar en formato base64
    convertirFileToBase64(file, function (base) {
        socket.send(base + "*" + file.name)
    })
}

function preview(file, insertar = 1) {
    if (imagenes[file.name] === undefined) {
        var reader = new FileReader();
        reader.addEventListener("load", function () {
            var col = document.createElement('div'); //image+info container
            var preview = document.createElement('div'); //div to show the image or icon
            var fileInfo = document.createElement('div'); //to show the name and button to remove
            fileInfo.setAttribute('class', 'dd-file-info');
            preview.setAttribute('class', 'dd-thumbnail');

            if (/\.(jpe?g|png|gif)$/i.test(file.name)) {
                preview.setAttribute('style', 'background-image:url("' + this.result + '")');
            }
            else {
                var icon = document.createElement('i');
                if (/\.(xls?x|csv)$/i.test(file.name)) {
                    icon.setAttribute('class', 'fa fa-file-excel-o');
                }
                if (/\.(doc?x)$/i.test(file.name)) {
                    icon.setAttribute('class', 'fa fa-file-word-o');
                }
                if (/\.(ppt?x)$/i.test(file.name)) {
                    icon.setAttribute('class', 'fa file-powerpoint-o');
                }
                //txt
                if (/\.(txt)$/i.test(file.name)) {
                    icon.setAttribute('class', 'fa file-text');
                }
                if (/\.(pdf)$/i.test(file.name)) {
                    icon.setAttribute('class', 'fa file-pdf-o');
                }
                preview.appendChild(icon);
            }

            col.setAttribute('data-ts-file', file.name);
            col.id = file.name
            col.appendChild(preview);
            var btnRemove = document.createElement('button');
            btnRemove.addEventListener('click', function (x) {
                fetchGet("Visor/eliminarArchivo/?nombrearchivo=" + col.id, "text", function (rpta) {

                    if (rpta == 1) {
                        Exito("Se elimino el archivo");
                        socket.send("remove" + col.id);
                    }

                })


            }, false);
            var spanTitle = document.createElement('span');
            spanTitle.appendChild(document.createTextNode(file.name));
            var btnIcon = document.createElement('i');
            btnIcon.setAttribute('class', 'fa fa-times-circle');
            btnRemove.appendChild(btnIcon);
            fileInfo.appendChild(spanTitle);
            fileInfo.appendChild(btnRemove);
            col.appendChild(fileInfo);
            document.getElementById('to_upload').getElementsByTagName('div')[0].appendChild(col);
            imagenes[file.name] = file;
            if (insertar == 1) {
                var frm = new FormData();
                frm.append("fotoEnviar", file)
                fetchPost("Visor/guardarArchivo", "text", frm, function (rpta) {
                    if (rpta == 1) {
                        Exito("Se guardo el archivo");

                    }
                })
            }
        }, false);
    }

    reader.readAsDataURL(file);
}
