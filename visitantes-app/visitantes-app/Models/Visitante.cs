using System;
using System.Collections.Generic;

namespace visitantes_app.Models
{
    public partial class Visitante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Cedula { get; set; } = null!;
    }
}
