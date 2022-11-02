using Microsoft.AspNetCore.Mvc;
using visitantes_app.Models;

namespace visitantes_app.Controllers
{
    public class EventosController : Controller
    {
        private readonly visitantesdbContext context;

        public EventosController(visitantesdbContext _context)
        {
            context = _context;
        }

        public IActionResult Eventos()
        {
            IEnumerable<Evento> eventos = context.Eventos.ToList();
            ViewBag.eventos = eventos;

            return View();
        }

        public IActionResult Form()
        {
            IEnumerable<Visitante> visitantes = context.Visitantes.ToList();
            ViewBag.visitantes = visitantes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Evento evento, int[] visitantes)
        {
            evento.Estado = 1;
            context.Eventos.Add(evento);
            context.SaveChanges();

            foreach(var item in visitantes)
            {
                VisitanteEvento visitanteEvento = new VisitanteEvento();
                visitanteEvento.EventoId = evento.Id;
                visitanteEvento.VisitanteId = item;
                context.VisitanteEventos.Add(visitanteEvento);
                context.SaveChanges();
            }
            // Crear movimiento en la tabla del histórico
            Historico historico = new Historico();
            historico.EventoId = evento.Id;
            historico.FechaModif = DateTime.Now;
            historico.Descripcion = "Agendar";
            context.Historicos.Add(historico);
            context.SaveChanges();

            return RedirectToAction("Eventos");
        }

        public IActionResult Remove(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var _evento = context.Eventos.Find(id);

            if(_evento != null)
            {
                _evento.Estado = 0;
                context.Eventos.Update(_evento);
                context.SaveChanges();

                // Crear movimiento en la tabla del histórico
                Historico historico = new Historico();
                historico.EventoId = _evento.Id;
                historico.FechaModif = DateTime.Now;
                historico.Descripcion = "Eliminar";
                context.Historicos.Add(historico);
                context.SaveChanges();

                return RedirectToAction("Eventos");
            }

            return View("Eventos");
        }

        public IActionResult Data(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var evento = context.Eventos.Find(id);

            if(evento != null)
            {
                IEnumerable<Visitante> visitantes = context.Visitantes.ToList();
                ViewBag.visitantes = visitantes;
                IEnumerable<VisitanteEvento> visitantesEvento = context.VisitanteEventos.ToList();
                ViewBag.visitantesEvento = visitantesEvento;
                return View(evento);
            }
            
            return View("Eventos");
        }
    }
}
