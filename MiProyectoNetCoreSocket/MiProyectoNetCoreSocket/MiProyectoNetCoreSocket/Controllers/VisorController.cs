using Microsoft.AspNetCore.Mvc;
using MiProyectoNetCoreSocket.Clases;
using io = System.IO; //File
namespace MiProyectoNetCoreSocket.Controllers
{
    public class VisorController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private string _urlsocket;

        //File
        public VisorController(IWebHostEnvironment env, IConfiguration configuracion)
        {
            _env = env;
            _urlsocket = configuracion["urlsocket"];
        }

        public IActionResult Index()
        {
            ViewBag.urlSocket = _urlsocket;
            return View();
        }

        public List<ArchivoCLS> listaArchivo()
        {
            List<ArchivoCLS> lista;
            try
            {

                lista = new List<ArchivoCLS>();
                string rutaCompleta = Path.Combine(_env.ContentRootPath, "wwwroot/archivos");
                DirectoryInfo di = new DirectoryInfo(rutaCompleta);
                var archivos = di.GetFiles()
                                        .OrderBy(f => f.LastWriteTime)
                                        .Select(f => f.FullName)
                                        .ToList();
                string cabeceraArchivo = "";
                foreach (string archivo in archivos)
                {
                    switch (io.Path.GetExtension(archivo).Replace(".", ""))
                    {
                        case "jpg": cabeceraArchivo = "data:image/jpg;base64,"; break;
                        case "png": cabeceraArchivo = "data:image/png;base64,"; break;
                        case "jpeg": cabeceraArchivo = "data:image/jpeg;base64,"; break;
                    }
                    lista.Add(new ArchivoCLS
                    {
                        nombrearchivo = io.Path.GetFileName(archivo),
                        database = cabeceraArchivo + Convert.ToBase64String(io.File.ReadAllBytes(archivo))
                    });
                }

            }
            catch (Exception ex)
            {
                lista = new List<ArchivoCLS>();
            }
            return lista;
        }


        public int eliminarArchivo(string nombrearchivo)
        {
            int rpta = 0;
            try
            {
                string ruta = Path.Combine(_env.ContentRootPath, "wwwroot/archivos");
                string rutaCompleta = io.Path.Combine(ruta, nombrearchivo);
                if (io.File.Exists(rutaCompleta))
                {
                    io.File.Delete(rutaCompleta);
                    rpta = 1;
                }
            }
            catch (Exception ex)
            {
                rpta = 0;
            }
            return rpta;
        }

        public int guardarArchivo(IFormFile fotoEnviar)
        {
            int rpta = 0;
            try
            {

                string ruta = Path.Combine(_env.ContentRootPath, "wwwroot/archivos");
                if (fotoEnviar != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fotoEnviar.CopyTo(ms);
                        string nombreFoto = fotoEnviar.FileName;
                        string rutaCompleta = io.Path.Combine(ruta, nombreFoto);
                        if (!io.File.Exists(rutaCompleta))
                        {
                            byte[] buffer = ms.ToArray();
                            io.File.WriteAllBytes(rutaCompleta, buffer);
                        }

                        rpta = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                rpta = 0;
            }
            return rpta;
        }




    }
}
