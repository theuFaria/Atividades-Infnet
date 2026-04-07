using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RotaCerta.Data;
using RotaCerta.Models;
using RotaCerta.Services;

namespace RotaCerta.Controllers
{
    public class PacoteTuristicoController : Controller
    {
        private readonly RotaCertaContext _context;

        public PacoteTuristicoController(RotaCertaContext context)
        {
            _context = context;
        }

        private List<SelectListItem> GetDestinos()
        {
           return _context.Destinos
                .Select(d => new SelectListItem()
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                }).ToList();
        } 

        // GET: PacoteTuristico
        public async Task<IActionResult> Index()
        {
            return View(await _context.PacoteTuristicos
                .Include(p => p.Destino)
                .Where(p => p.IsDeleted == false).ToListAsync());
        }

        // GET: PacoteTuristico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteTuristico = await _context.PacoteTuristicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacoteTuristico == null)
            {
                return NotFound();
            }

            return View(pacoteTuristico);
        }    

        // GET: PacoteTuristico/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.DestinoId = GetDestinos();

            return View();
        }

        // POST: PacoteTuristico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Titulo,DataIda,DataRetorno,CapacidadeMaxima,PrecoPorPessoa,DestinoId")]
            PacoteTuristico pacoteTuristico)
        {
            if (ModelState.IsValid)
            {
                if (pacoteTuristico.DataIda < DateTime.Today | pacoteTuristico.DataRetorno < DateTime.Today)
                {
                    TempData["MensagemDeErro"] = "A datas não podem ser anteriores a data atual.";
                    return View();
                }

                if (pacoteTuristico.DataRetorno < pacoteTuristico.DataIda)
                {
                    TempData["MensagemDeErro"] = "“A data de retorno não pode ser anterior à data de ida.";
                    return View();
                }

                RegistroService registro = new RegistroService();

                pacoteTuristico.CapacidadeDisponivel = pacoteTuristico.CapacidadeMaxima;

                pacoteTuristico.Destino = _context.Destinos.FirstOrDefault(d => d.Id == pacoteTuristico.DestinoId
                ) ?? throw new Exception("Destino não encontrado");
                
                _context.Add(pacoteTuristico);
                await _context.SaveChangesAsync();

                registro.ChamarRegistro($"Pacote turístico ({pacoteTuristico.Id}) criado com sucesso - {DateTime.Now}");

                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.DestinoId = GetDestinos();
            return View(pacoteTuristico);
        }

        // GET: PacoteTuristico/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteTuristico = await _context.PacoteTuristicos.FindAsync(id);
            if (pacoteTuristico == null)
            {
                return NotFound();
            }

            ViewBag.DestinoId = GetDestinos();

            return View(pacoteTuristico);
        }

        // POST: PacoteTuristico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Titulo,DataIda,DataRetorno,CapacidadeMaxima,PrecoPorPessoa,DestinoId")]
            PacoteTuristico pacoteTuristico)
        {
            if (id != pacoteTuristico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (pacoteTuristico.DataIda < DateTime.Today | pacoteTuristico.DataRetorno < DateTime.Today)
                {
                    TempData["MensagemDeErro"] = "A datas não podem ser anteriores a data atual.";
                    return View();
                }

                if (pacoteTuristico.DataRetorno < pacoteTuristico.DataIda)
                {
                    TempData["MensagemDeErro"] = "“A data de retorno não pode ser anterior à data de ida.";
                    return View();
                }

                try
                {
                    pacoteTuristico.CapacidadeDisponivel =
                        pacoteTuristico.CapacidadeMaxima - _context.Reservas
                            .Include(r => r.PacoteTuristico)
                            .Where(r => r.IsConfirmada)
                            .Where(r => r.PacoteTuristicoId == pacoteTuristico.Id)
                            .Sum(r => r.QuantidadeDePessoas);

                    _context.Update(pacoteTuristico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacoteTuristicoExists(pacoteTuristico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(pacoteTuristico);
        }

        // GET: PacoteTuristico/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteTuristico = await _context.PacoteTuristicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacoteTuristico == null)
            {
                return NotFound();
            }

            return View(pacoteTuristico);
        }

        // POST: PacoteTuristico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacoteTuristico = await _context.PacoteTuristicos.FindAsync(id);

            if (pacoteTuristico != null)
            {
                pacoteTuristico.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacoteTuristicoExists(int id)
        {
            return _context.PacoteTuristicos.Any(e => e.Id == id);
        }
    }
}