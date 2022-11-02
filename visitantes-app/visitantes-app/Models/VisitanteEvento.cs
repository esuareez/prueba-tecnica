using System;
using System.Collections.Generic;

namespace visitantes_app.Models
{
    public partial class VisitanteEvento
    {
        public int Id { get; set; }
        public int? VisitanteId { get; set; }
        public int? EventoId { get; set; }
    }
}
