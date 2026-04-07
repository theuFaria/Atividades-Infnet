using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RotaCerta.Data;
using RotaCerta.Models;
using RotaCerta.Services;

namespace RotaCerta.Controllers;

public class ReservaController : Controller
{
    private readonly RotaCertaContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReservaController(RotaCertaContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    //GET - Vai para página Index
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    //GET - Vai para página Lista
    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> Lista()
    {
        var user = await _userManager.GetUserAsync(User);

        return View(_context.Reservas
            .Where(r => r.UserId == user.Id)
            .Where(r => r.IsConfirmada)
            .Include(r => r.PacoteTuristico)
            .ToList());
    }
    
    // GET - Vai para a página Create
    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public IActionResult Create()
    {
        ViewBag.PacoteTuristicoId = _context.PacoteTuristicos
            .Where(p => p.IsDeleted == false)
            .Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Titulo
            })
            .ToList();
        return View();
    }

    private List<SelectListItem> GetPacotesTuristicos()
    {
        return _context.PacoteTuristicos
            .Where(p => p.IsDeleted == false)
            .Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Titulo
            }).ToList();
    }

    //POST - Cria RESERVA
    [HttpPost]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> Create(
        [Bind("PacoteTuristicoId,Nome,QuantidadeDePessoas")]
        Reserva reserva)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.PacoteTuristicoId = GetPacotesTuristicos();
            return View(reserva);
        }

        // Preenche o objeto pacoteTuristico da reserva
        reserva.PacoteTuristico = _context.PacoteTuristicos.FirstOrDefault(p => p.Id == reserva.PacoteTuristicoId) ??
                                  throw new Exception("PacoteTuristico not found");

        //Verifica se existem vagas disponíveis no pacote
        CapacidadeService service = new CapacidadeService();

        service.CapacityReached += (msg) => { TempData["MensagemDeErro"] = msg; };

        if (reserva.PacoteTuristico.CapacidadeDisponivel <= 0)
        {
            service.DispararEvento();
            ViewBag.PacoteTuristicoId = GetPacotesTuristicos();
            return View(reserva);
        }

        //OBS futuramente impedir a possivel tentativa de reserva em pacotes cujo a capacidadeDisponivel for igual a 0.

        //Preenche o UserId e o User
        var user = await _userManager.GetUserAsync(User);
        reserva.User = user;
        reserva.UserId = user.Id;

        reserva.DataReserva = DateTime.Now;
        
        // Coloca o valor total e o valor por pessoa da reserva.
        Func<int, decimal, decimal> calcularTotal = (quantidade, preco) => quantidade * preco;

        reserva.ValorTotal = calcularTotal(reserva.QuantidadeDePessoas, reserva.PacoteTuristico.PrecoPorPessoa);

        TempData["PrecoSemDesconto"] = $"{reserva.ValorTotal}";

        //Aplica desconto de 10% sobre o preço total da reserva
        DescontoService descontoService = new DescontoService();
        reserva.ValorTotal = descontoService.AplicarDesconto(reserva.ValorTotal);

        reserva.PacoteTuristico.CapacidadeDisponivel -= reserva.QuantidadeDePessoas;

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        return RedirectToAction("FinalizarReserva", new { id = reserva.Id });
    }

    //GET - Vai para valor Final
    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public IActionResult FinalizarReserva(int id)
    {
        var reserva = _context.Reservas
                          .Include(r => r.PacoteTuristico)
                          .FirstOrDefault(r => r.Id == id) ??
                      throw new Exception("Reserva not found");

        return View(reserva);
    }

    //POST - Finaliza a Reserva
    [HttpPost]
    [Authorize(Roles = "User, Admin")]
    public IActionResult FinalizarReserva(Reserva res)
    {
        var reserva = _context.Reservas
                          .Include(r => r.PacoteTuristico)
                          .FirstOrDefault(r => r.Id == res.Id) ??
                      throw new Exception("Reserva not found");

        reserva.IsConfirmada = true;


        _context.SaveChanges();

        RegistroService service = new RegistroService();
        service.ChamarRegistro($"Reserva ({reserva.Id}) foi gerada com sucesso - {DateTime.Now}");

        return RedirectToAction("Index");
    }

    //GET - Vai para Deletar 
    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public IActionResult Deletar(int? id)
    {
        if (id == null)
        {
            return new NotFoundResult();
        }

        var reserva = _context.Reservas.Find(id);

        if (reserva == null)
        {
            return new NotFoundResult();
        }

        return View(reserva);
    }

    //POST - Deleta usuário
    [HttpPost]
    [Authorize(Roles = "User, Admin")]
    public IActionResult DeletarReserva(int id)
    {
        var reserva = _context.Reservas.Find(id);

        if (reserva == null)
        {
            return new NotFoundResult();
        }

        _context.Reservas.Remove(reserva);
        _context.SaveChanges();

        return RedirectToAction("Lista");
    }

    //GET - Vai para detalhes
    [HttpGet]
    [Authorize(Roles = "User, Admin")]
    public IActionResult Detalhes(int id)
    {
        var reserva = _context.Reservas.Find(id);

        return View(reserva);
    }
}