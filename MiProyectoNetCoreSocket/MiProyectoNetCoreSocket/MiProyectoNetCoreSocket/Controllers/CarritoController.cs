using Microsoft.AspNetCore.Mvc;
using MiProyectoNetCoreSocket.Clases;
using MiProyectoNetCoreSocket.Models;

namespace MiProyectoNetCoreSocket.Controllers
{
    public class CarritoController : Controller
    {
        private string _urlsocket;
        public CarritoController(IConfiguration configuracion)
        {
            _urlsocket = configuracion["urlsocket"];
        }

        public IActionResult Index()
        {
            ViewBag.urlSocket = _urlsocket;
            return View();
        }

        public List<ProductoCLS> listarProductos(string descripcion)
        {
            List<ProductoCLS> lista = new List<ProductoCLS>();
            try
            {

                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    if (descripcion == null || descripcion == "")
                        lista = (from producto in bd.Productos
                                 where producto.Bhabilitado == 1
                                 select new ProductoCLS
                                 {
                                     iidproducto = producto.Iidproducto,
                                     description = producto.Descripcion,
                                     preciocadena = "S/. " + ((decimal)producto.Precio).ToString(),
                                     stockcadena = ((int)producto.Stock).ToString() + " vacant. ",
                                     //foto = producto.Nombrefoto == null ? "/img/noimagen.jpg" :
                                     foto = producto.Nombrefoto == null ? "/img/imgno.png" :
                                      "data:image/" + Path.GetExtension(producto.Nombrefoto).Replace(".", "") + ";base64,"
                                           + Convert.ToBase64String(producto.Foto)
                                 }).ToList();
                    else
                        lista = (from producto in bd.Productos
                                 where producto.Bhabilitado == 1
                                 && producto.Descripcion.Contains(descripcion)
                                 select new ProductoCLS
                                 {
                                     iidproducto = producto.Iidproducto,
                                     description = producto.Descripcion,
                                     preciocadena = "S/. " + ((decimal)producto.Precio).ToString(),
                                     stockcadena = ((int)producto.Stock).ToString() + " unid ",
                                     foto = producto.Nombrefoto == null ? "/img/imgno.png" :
                                      "data:image/" + Path.GetExtension(producto.Nombrefoto).Replace(".", "") + ";base64,"
                                           + Convert.ToBase64String(producto.Foto)
                                 }).ToList();
                    return lista;
                }


            }
            catch (Exception ex)
            {
                return lista;
            }
        }
    }
}
