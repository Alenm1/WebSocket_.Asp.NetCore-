using Microsoft.AspNetCore.Mvc;
using MiProyectoNetCoreSocket.Clases;
using MiProyectoNetCoreSocket.Models;

namespace MiProyectoNetCoreSocket.Controllers
{
    public class ProductoController : Controller
    {
        private string _urlsocket;
        public ProductoController(IConfiguration configuracion)
        {
            _urlsocket = configuracion["urlsocket"];
        }

        public IActionResult Index()
        {
            ViewBag.urlSocket = _urlsocket;
            return View();
        }

        //Lista todo y ademas filtra
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
                                     preciocadena = "S/. "+((decimal)producto.Precio).ToString(),
                                     stockcadena = ((int)producto.Stock).ToString()+" vacantes ",
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
                                     stockcadena = ((int)producto.Stock).ToString() + " vacantes ",
                                 }).ToList();
                    return lista;
                }


            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        //Recuperar producto

        public ProductoCLS recuperarProducto(int id)
        {
            ProductoCLS oProductoCLS;
            try
            {
                //.jpg .png .jpeg ..........
                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    oProductoCLS = (from producto in bd.Productos
                                    where producto.Iidproducto == id
                                    select new ProductoCLS
                                    {
                                        iidproducto = producto.Iidproducto,
                                        description = producto.Descripcion,
                                        precio = (decimal)producto.Precio,
                                        stock = (int)producto.Stock,
                                        foto = producto.Nombrefoto == null ? "" :
                                           "data:image/" + Path.GetExtension(producto.Nombrefoto).Replace(".", "") + ";base64,"
                                           + Convert.ToBase64String(producto.Foto)
                                    }).First();
                }


            }
            catch (Exception ex)
            {

                oProductoCLS = new ProductoCLS();
            }
            return oProductoCLS;

        }

        //Eliminar producto
        public int eliminarProducto(int id)
        {
            //Error
            int rpta = 0;
            try
            {

                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    Producto oProducto = bd.Productos.Where(p => p.Iidproducto == id).First();
                    oProducto.Bhabilitado = 0;
                    bd.SaveChanges();
                    rpta = 1;
                }


            }
            catch (Exception ex)
            {
                rpta = 0;
            }
            return rpta;
        }

        //Inserta o Actualize
        public int guardarProducto(ProductoCLS oProductoCLS, IFormFile fotoEnviar)
        {
            //Error
            int rpta = 0;
            try
            {
                int iidproducto = oProductoCLS.iidproducto;
                byte[] foto = new byte[0];
                string nombrefoto = "";
                if (fotoEnviar != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fotoEnviar.CopyTo(ms);
                        nombrefoto = fotoEnviar.FileName;
                        foto = ms.ToArray();
                    }
                }

                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    //Insertar
                    if (iidproducto == 0)
                    {
                        Producto oProducto = new Producto();
                        oProducto.Descripcion = oProductoCLS.description;
                        oProducto.Precio = oProductoCLS.precio;
                        oProducto.Stock = oProductoCLS.stock;
                        oProducto.Bhabilitado = 1;
                        if (nombrefoto != "")
                        {
                            oProducto.Foto = foto;
                            oProducto.Nombrefoto = nombrefoto;
                        }

                        bd.Productos.Add(oProducto);
                        bd.SaveChanges();
                        //Exito
                        rpta = 1;

                    }
                    //Actualizar
                    else
                    {
                        Producto oProducto = bd.Productos.Where(p => p.Iidproducto == iidproducto).First();
                        oProducto.Descripcion = oProductoCLS.description;
                        oProducto.Precio = oProductoCLS.precio;
                        oProducto.Stock = oProductoCLS.stock;
                        if (nombrefoto != "")
                        {
                            oProducto.Foto = foto;
                            oProducto.Nombrefoto = nombrefoto;
                        }

                        bd.SaveChanges();
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


