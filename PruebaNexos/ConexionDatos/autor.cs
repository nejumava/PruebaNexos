//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConexionDatos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class autor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public autor()
        {
            this.libroes = new HashSet<libro>();
        }
    
        public int id { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [Required]
        [DisplayName("Fecha de Nacimiento")]
        public System.DateTime fecha_nacimiento { get; set; }

        [Required]
        [DisplayName("Ciudad")]
        public string ciudad { get; set; }

        [Required]
        [DisplayName("Email")]
        public string email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<libro> libroes { get; set; }
    }
}
