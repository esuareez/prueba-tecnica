using System;
using System.Collections.Generic;

namespace visitantes_app.Models
{
    public partial class Historico
    {
        public int Id { get; set; }
        public int? VisitanteId { get; set; }
        public int? EventoId { get; set; }
        public int? HistorialId { get; set; }
        public DateTime FechaModif { get; set; }
        public string? Descripcion { get; set; }
    }
}
