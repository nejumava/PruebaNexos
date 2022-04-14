using ConexionDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiRest.Controllers
{
    public class LibroController : ApiController
    {
        private NexusEntities dbContext = new NexusEntities();
        private static int MAXIMO_PERMITIDO = 3;

        // Creación de un nuevo libro
        [HttpPost]
        public IHttpActionResult crear([FromBody] libro l)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Máximo permitido
                    var maxPermitido = dbContext.libroes.Count() < MAXIMO_PERMITIDO;

                    if (maxPermitido)
                    {
                        var autorExiste = dbContext.autors.Count(a => a.nombre.Contains(l.nombre_autor)) > 0;

                        if (autorExiste)
                        {
                            autor autor = dbContext.autors.FirstOrDefault(a => a.nombre == l.nombre_autor);
                            l.autor_id = autor.id;
                            dbContext.libroes.Add(l);
                            dbContext.SaveChanges();

                            return Ok(l);
                        }
                        else
                        {
                            return Content(HttpStatusCode.BadRequest, "El autor no esta registrado");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, "No es posible registrar el libro, se alcanzo el máximo permitido");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en creación de libro.");
            }
        }

        // Busqueda de libro por autor
        [HttpGet]
        public IEnumerable<libro> LibrosPorAutor(string nombre)
        {
            try
            {
                var autorExiste = dbContext.autors.Count(a => a.nombre.Contains(nombre)) > 0;
                if (autorExiste)
                {
                    return dbContext.libroes.Where(l => l.autor.nombre.Contains(nombre)).ToList();
                }
                else
                {
                    return new List<libro>();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en búsqueda de libro por autor.");
            }

        }

        // Busqueda de libro por titulo
        [HttpGet]
        public IEnumerable<libro> LibrosPorTitulo(string titulo)
        {
            try
            {
                return dbContext.libroes.Where(l => l.titulo.Contains(titulo)).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error en búsqueda de libro por título.");
            }
        }

        // Busqueda de libro por año
        [HttpGet]
        public IEnumerable<libro> LibrosPorAnio(int anio)
        {
            try
            {
                return dbContext.libroes.Where(l => l.anio == anio).ToList();
            }
            catch (Exception)
            {

                throw new Exception("Error en la búsqueda de libros por año.");
            }
        }
    }
}
