using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using visitantes_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace visitantes_app.Controllers
{
    public class VisitantesController : Controller
    {
        private readonly visitantesdbContext context;

        public VisitantesController(visitantesdbContext _context)
        {
            context = _context;
        }

        public IActionResult Visitantes()
        {

            IEnumerable<Visitante> listVisitantes = context.Visitantes.ToList();
            ViewBag.visit = listVisitantes;
            IEnumerable<Historial> listHistorial = context.Historials.ToList();
            ViewBag.historial = listHistorial;
            return View();
        }

        // Crear Visitantes

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Visitante visitante) 
        {
            if (ModelState.IsValid)
            {
                var _visitante = context.Visitantes.Where(s =>
                s.Id == visitante.Id);
                // Si _visitante es diferente de null es porque ya existe un visitante con ese id o cedula.
                if (!_visitante.Any())
                {
                    context.Visitantes.Add(visitante);
                    context.SaveChanges();

                    // Crear movimiento en la tabla del histórico
                    Historico historico = new Historico();
                    historico.VisitanteId = visitante.Id;
                    historico.FechaModif = DateTime.Now;
                    historico.Descripcion = "Agregar";
                    context.Historicos.Add(historico);

                    // Crear historial de visitas del visitante
                    Historial historial = new Historial();
                    historial.VisitanteId = visitante.Id;
                    historial.FechaInicio = DateTime.Now;
                    context.Historials.Add(historial);

                    // Guardar Cambios
                    context.SaveChanges();
                    return RedirectToAction("Visitantes");
                }
            }
            return View("Visitantes");
        }

        public IActionResult Form(int? id)
        {
            // En caso que el id no sea null, ni 0. Se busca el visitante.
            var visitante = context.Visitantes.Find(id);

            if (visitante == null)
            {
                return View();
            }

            return View(visitante);
        }

        // Editar Visitante

        // POST Visitante

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Visitante visitante)
        {
            if (ModelState.IsValid)
            {
                context.Visitantes.Update(visitante);

                // Crear movimiento en la tabla del histórico
                Historico historico = new Historico();
                historico.VisitanteId = visitante.Id;
                historico.FechaModif = DateTime.Now;
                historico.Descripcion = "Editar";
                context.Historicos.Add(historico);

                context.SaveChanges();
                return RedirectToAction("Visitantes");
            }
            return View("Visitantes");
        }

        // Vista parcial de la tabla de visitantes.
        public IActionResult tabla()
        {
            IEnumerable<Visitante> listVisitantes = context.Visitantes.ToList();
            ViewBag.visit = listVisitantes;
            IEnumerable<Historial> listHistorial = context.Historials.ToList();
            ViewBag.historial = listHistorial;
            return PartialView();
        }

        // Salida hace referencia a la salida del visitante. Con solo dar clic en la tabla, se confirma la salida de este.
        public IActionResult Salida(int? id)
        {
            var historial = context.Historials.Where(s => s.VisitanteId == id).FirstOrDefault();
            if (historial != null)
            {
                historial.FechaSalida = DateTime.Now;
                context.Historials.Update(historial);

                // Crear movimiento en la tabla del histórico
                Historico historico = new Historico();
                historico.HistorialId = historial.Id;
                historico.FechaModif = DateTime.Now;
                historico.Descripcion = "Salida";
                context.Historicos.Add(historico);

                context.SaveChanges();
                return RedirectToAction("Visitantes");
            }
            return RedirectToAction("Visitantes");
        }


        // GET: Historial de visitas
        public IActionResult Historial()
        {
            IEnumerable<Historial> listHistorial = context.Historials.ToList();
            ViewBag.historial = listHistorial;
            IEnumerable<Visitante> listVisitante = context.Visitantes.ToList();
            ViewBag.visitante = listVisitante;
            return View(); 
        }

        // GET: Historial de movimientos y cambios.
        public IActionResult Movimientos()
        {
            IEnumerable<Historico> listHistorico = context.Historicos.ToList();
            ViewBag.historicos = listHistorico;
            IEnumerable<Historial> listHistorial = context.Historials.ToList();
            ViewBag.historial = listHistorial;
            IEnumerable<Visitante> listVisitante = context.Visitantes.ToList();
            ViewBag.visitante = listVisitante;
            IEnumerable<Evento> listEvento = context.Eventos.ToList();
            ViewBag.evento = listEvento;
            return View();
        }
    }
}
