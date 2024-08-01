using Microsoft.AspNetCore.Mvc;
using MiProyectoNetCoreSocket.Clases;
using MiProyectoNetCoreSocket.Models;
using System.Collections.Generic;
using System.Linq;

namespace MiProyectoNetCoreSocket.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Lista todas las personas y además filtra
        public List<PersonaCLS> listarPersonas(string nombre)
        {
            List<PersonaCLS> lista = new List<PersonaCLS>();
            try
            {
                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    if (string.IsNullOrEmpty(nombre))
                    {
                        lista = (from persona in bd.Personas
                                 select new PersonaCLS
                                 {
                                     Iidpersona = persona.Iidpersona,
                                     Numeroidentificacion = persona.Numeroidentificacion,
                                     Nombre = persona.Nombre,
                                     Appaterno = persona.Appaterno,
                                     Apmaterno = persona.Apmaterno,
                                     Iidsexo = persona.Iidsexo,
                                     Correo = persona.Correo
                                 }).ToList();
                    }
                    else
                    {
                        lista = (from persona in bd.Personas
                                 where persona.Nombre.Contains(nombre) ||
                                       persona.Appaterno.Contains(nombre) ||
                                       persona.Apmaterno.Contains(nombre)
                                 select new PersonaCLS
                                 {
                                     Iidpersona = persona.Iidpersona,
                                     Numeroidentificacion = persona.Numeroidentificacion,
                                     Nombre = persona.Nombre,
                                     Appaterno = persona.Appaterno,
                                     Apmaterno = persona.Apmaterno,
                                     Iidsexo = persona.Iidsexo,
                                     Correo = persona.Correo
                                 }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (puedes añadir logging aquí)
            }
            return lista;
        }

        // Recuperar persona por id
        public PersonaCLS recuperarPersona(int id)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();
            try
            {
                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    oPersonaCLS = (from persona in bd.Personas
                                   where persona.Iidpersona == id
                                   select new PersonaCLS
                                   {
                                       Iidpersona = persona.Iidpersona,
                                       Numeroidentificacion = persona.Numeroidentificacion,
                                       Nombre = persona.Nombre,
                                       Appaterno = persona.Appaterno,
                                       Apmaterno = persona.Apmaterno,
                                       Iidsexo = persona.Iidsexo,
                                       Correo = persona.Correo
                                   }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (puedes añadir logging aquí)
            }
            return oPersonaCLS;
        }

        // Eliminar persona (deshabilitar)
        public int eliminarPersona(int id)
        {
            int rpta = 0;
            try
            {
                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    Persona oPersona = bd.Personas.FirstOrDefault(p => p.Iidpersona == id);
                    if (oPersona != null)
                    {
                        bd.Personas.Remove(oPersona);
                        bd.SaveChanges();
                        rpta = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (puedes añadir logging aquí)
                rpta = 0;
            }
            return rpta;
        }

        // Insertar o actualizar persona
        public int guardarPersona(PersonaCLS oPersonaCLS)
        {
            int rpta = 0;
            try
            {
                using (DbAab78eBdreportesContext bd = new DbAab78eBdreportesContext())
                {
                    if (oPersonaCLS.Iidpersona == 0)
                    {
                        Persona oPersona = new Persona
                        {
                            Numeroidentificacion = oPersonaCLS.Numeroidentificacion,
                            Nombre = oPersonaCLS.Nombre,
                            Appaterno = oPersonaCLS.Appaterno,
                            Apmaterno = oPersonaCLS.Apmaterno,
                            Iidsexo = oPersonaCLS.Iidsexo,
                            Correo = oPersonaCLS.Correo
                        };
                        bd.Personas.Add(oPersona);
                        bd.SaveChanges();
                        rpta = 1;
                    }
                    else
                    {
                        Persona oPersona = bd.Personas.FirstOrDefault(p => p.Iidpersona == oPersonaCLS.Iidpersona);
                        if (oPersona != null)
                        {
                            oPersona.Numeroidentificacion = oPersonaCLS.Numeroidentificacion;
                            oPersona.Nombre = oPersonaCLS.Nombre;
                            oPersona.Appaterno = oPersonaCLS.Appaterno;
                            oPersona.Apmaterno = oPersonaCLS.Apmaterno;
                            oPersona.Iidsexo = oPersonaCLS.Iidsexo;
                            oPersona.Correo = oPersonaCLS.Correo;
                            bd.SaveChanges();
                            rpta = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (puedes añadir logging aquí)
                rpta = 0;
            }
            return rpta;
        }
    }
}
