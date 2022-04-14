using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    public class Busqueda
    {
        [Required]
        [DisplayName("Valor")]
        public string valor { get; set; }

        [Required]
        [DisplayName("Campo")]
        public int campo { get; set; }
    }
}