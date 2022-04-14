using ConexionDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ApiRest.Controllers.WEB
{
    public class AutorController : Controller
    {
        // GET: Autor
        public ActionResult Registrar()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw new Exception("Error en método GET Registrar");
            }
        }

        // POST: Autor
        [HttpPost]
        public ActionResult Registrar(autor a)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Consumo de la API
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:6658/api/Autor");
                        var post = client.PostAsJsonAsync<autor>("autor", a);
                        post.Wait();

                        var result = post.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Buscar", "Busqueda");
                        }
                    }

                    //Si no se realiza bien la acción
                    ModelState.AddModelError(string.Empty, "No fue posible guardar el autor");
                    return View(a);
                }
                else
                {
                    return View(a);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en método POST Registrar");
            }
        }
    }
}