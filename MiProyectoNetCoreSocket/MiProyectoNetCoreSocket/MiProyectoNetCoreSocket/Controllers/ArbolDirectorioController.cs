using Microsoft.AspNetCore.Mvc;
using MiProyectoNetCoreSocket.Clases;
using io = System.IO;
namespace MiProyectoNetCoreSocket.Controllers
{
    public class ArbolDirectorioController : Controller
    {
        private readonly IWebHostEnvironment _env;

        private string _urlsocket;

        //File
        public ArbolDirectorioController(IWebHostEnvironment env, IConfiguration configuracion)
        {
            _env = env;
            _urlsocket = configuracion["urlsocket"];
        }

        public IActionResult Index()
        {
            ViewBag.urlSocket = _urlsocket;
            return View();
        }

        public List<ArbolCLS> listarDirectorios()
        {
            List<ArbolCLS> listaArbol = new List<ArbolCLS>();
            try
            {
                string ruta = Path.Combine(_env.ContentRootPath, "wwwroot/documentos");
                if (Directory.Exists(ruta))
                {
                    string[] directorios = Directory.GetDirectories(ruta, "*.*", SearchOption.AllDirectories);
                    foreach (string dir in directorios)
                    {
                        string rutaAcortada = dir.Replace(ruta + "\\", "");
                        listaArbol.Add(new ArbolCLS
                        {
                            rutaCompleta = rutaAcortada,
                            nombredirectorio = new DirectoryInfo(dir).Name,
                            listaRutaarchivos = io.Directory.GetFiles(dir).Select(p => p.Replace(ruta + "\\", "")).ToList(),
                            listaNombrearchivos = io.Directory.GetFiles(dir).Select(p => new FileInfo(p).Name).ToList()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return listaArbol;
            }
            return listaArbol;
        }
        public FileResult descargarArchivo(string ruta)
        {

            string rutaCompleta = Path.Combine(_env.ContentRootPath, "wwwroot/documentos", ruta);
            byte[] buffer = io.File.ReadAllBytes(rutaCompleta);
            return File(buffer, System.Net.Mime.MediaTypeNames.Application.Octet);

        }

    }
}
