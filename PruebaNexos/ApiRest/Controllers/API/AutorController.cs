using ConexionDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiRest.Controllers
{
    public class AutorController : ApiController
    {
        private NexusEntities dbContext = new NexusEntities();

        // Creación de un nuevo autor
        [HttpPost]
        public IHttpActionResult crear([FromBody] autor a)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.autors.Add(a);
                    dbContext.SaveChanges();

                    return Ok(a);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en creación de autor");
            }
        }
    }
}
