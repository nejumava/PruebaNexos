using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ApiRest.Models;
using ConexionDatos;

namespace ApiRest.Controllers.WEB
{
    public class BusquedaController : Controller
    {
        private List<Campos> listCampos = new List<Campos>();
        IEnumerable<libro> libros;

        // Constructor
        public BusquedaController()
        {
            // Valores para la búsqueda
            listCampos.Add(new Campos { IdCampo = 1, Campo = "Por autor" });
            listCampos.Add(new Campos { IdCampo = 2, Campo = "Por titulo" });
            listCampos.Add(new Campos { IdCampo = 3, Campo = "Por año" });

            // Listado de libros inicial
            libros = Enumerable.Empty<libro>();
        }

        // GET: Buscar
        public ActionResult Buscar()
        {
            try
            {
                ViewBag.Campos = listCampos;
                ViewBag.libros = libros;
                return View();
            }
            catch (Exception)
            {
                throw new Exception("Error em método GET Buscar.");
            }
        }

        // POST: Buscar
        [HttpPost]
        public ActionResult Buscar(Busqueda busqueda)
        {
            try
            {
                ViewBag.Campos = listCampos;
                libros = Enumerable.Empty<libro>();
                ViewBag.libros = libros;

                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:6658/api/");
                        Task<HttpResponseMessage> response = null;

                        // Realiza la búsqueda por autor
                        if (busqueda.campo.Equals(1))
                        {
                            response = client.GetAsync("Libro/?nombre=" + busqueda.valor);
                        }

                        // Realiza la búsqueda por título
                        if (busqueda.campo.Equals(2))
                        {
                            response = client.GetAsync("Libro/?titulo=" + busqueda.valor);
                        }

                        // Realiza la búsqueda por año
                        if (busqueda.campo.Equals(3))
                        {
                            response = client.GetAsync("Libro/?anio=" + busqueda.valor);
                        }

                        response.Wait();

                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var read = result.Content.ReadAsAsync<IList<libro>>();
                            read.Wait();
                            libros = read.Result;
                            ViewBag.libros = libros;

                            if (libros.Count() == 0)
                            {
                                ModelState.AddModelError(string.Empty, "No hay resultados que mostrar");
                            }
                        }
                        else
                        {
                            libros = Enumerable.Empty<libro>();
                            ViewBag.libros = libros;
                            ModelState.AddModelError(string.Empty, "No hay resultados que mostrar");
                        }
                    }
                    return View();
                }
                else
                {
                    return View(busqueda);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en método POST Buscar.");
            }
        }
    }
}