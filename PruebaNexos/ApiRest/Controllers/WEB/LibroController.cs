using ConexionDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ApiRest.Controllers.WEB
{
    public class LibroController : Controller
    {
        // GET: Libro
        public ActionResult Registrar()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw new Exception("Error en método GET Registrar.");
            }
        }

        // POST: Libro
        [HttpPost]
        public ActionResult Registrar(libro l)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Consumo de la API
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:6658/api/Libro");
                        var post = client.PostAsJsonAsync<libro>("libro", l);
                        post.Wait();

                        var result = post.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Buscar", "Busqueda");
                        }
                        else
                        {
                            //Si no se realiza bien la acción
                            ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                            return View(l);
                        }
                    }
                }
                else
                {
                    return View(l);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en método POST Registrar.");
            }
        }
    }
}