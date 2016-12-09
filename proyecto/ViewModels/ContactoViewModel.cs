using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proyecto.ViewModels
{
    public class ContactoViewModel
    {
        [Required]
        public string Nombre { set; get; }

        [Required]
        [EmailAddress]
        public string Correo { set; get; }

        [Required]
        public string Mensaje { set; get; }
    }
}