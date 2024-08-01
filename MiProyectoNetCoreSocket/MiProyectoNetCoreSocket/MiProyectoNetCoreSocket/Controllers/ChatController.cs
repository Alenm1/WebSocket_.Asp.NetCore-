using Microsoft.AspNetCore.Mvc;
using MiProyectoNetCoreSocket.Clases;
using MiProyectoNetCoreSocket.Models;

namespace MiProyectoNetCoreSocket.Controllers
{
    public class ChatController : Controller
    {
        private string _urlsocket;
        public ChatController(IConfiguration configuracion)
        {
            _urlsocket = configuracion["urlsocket"];
        }

        public IActionResult Index()
        {
            ViewBag.urlSocket = _urlsocket;
            return View();
        }

        public List<ChatCLS> listarChat(int pag)
        {
            List<ChatCLS> lista = new List<ChatCLS>();
            try
            {
                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    int inicio = 10 * pag;
                    lista = bd.Chats.OrderByDescending(p => p.Idmensaje).Skip(inicio).Take(10).
                        Select(p => new ChatCLS
                        {
                            mensaje = p.Mensaje,
                            nombreusuario = p.Nombreusuario
                        }).ToList();
                    return lista;
                }

            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        public int guardarMensaje(ChatCLS oChatCLS)
        {
            int rpta = 0;
            try
            {
                using(DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    Chat oChat = new Chat();
                    oChat.Mensaje = oChatCLS.mensaje;
                    oChat.Nombreusuario = oChatCLS.nombreusuario;
                    oChat.Bhabilitado = 1;
                    bd.Chats.Add(oChat);
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


    }
}