using System;
using System.Collections.Generic;

namespace visitantes_app.Models
{
    public partial class Historial
    {
        public int Id { get; set; }
        public int VisitanteId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaSalida { get; set; }
    }
}
