using System;
using System.Collections.Generic;

namespace visitantes_app.Models
{
    public partial class Evento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int? Estado { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
