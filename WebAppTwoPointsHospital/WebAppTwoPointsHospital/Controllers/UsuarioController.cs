using Microsoft.AspNetCore.Mvc;
using WebAppTwoPointsHospital.Models;

namespace WebAppTwoPointsHospital.Controllers;

public class UsuarioController : Controller
{
    private readonly Contexto _context;

    public UsuarioController(Contexto context)
    {
        _context = context;
    }

    // Get -> Va para página Login
    public IActionResult Login()
    {
        return View();
    }

    // Get -> Vai para a página Cadastro
    [HttpGet]
    public IActionResult Cadastro()
    {
        return View();
    }

    //Get -> Vai para a página Usuários
    [HttpGet]
    public IActionResult Usuarios()
    {
        List<Usuario> usuarios = _context.Usuarios.ToList();
        return View(usuarios);
    }

    // Get -> Vai para a página Detalhes
    [HttpGet]
    public IActionResult Detalhes(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id); 
        
        return View(usuario);
    }

    //Get -> Vai para a página Deletar
    [HttpGet]
    public IActionResult Deletar(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        return View(usuario);
    }

    // POST -> Cadastra Usuário
    [HttpPost]
    public IActionResult Cadastro(Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        Usuario novoUsuario = new Usuario
        {
            Nome = usuario.Nome,
            Cpf = usuario.Cpf,
            Email = usuario.Email,
            Senha = usuario.Senha
        };

        _context.Usuarios.Add(novoUsuario);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }

    //Post -> Valida Login
    [HttpPost]
    public IActionResult Login(string email, string senha)
    {
        var user = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);

        if (user == null)
        {
            ModelState.AddModelError("", "Email ou senha inválidos");
            return View();
        }
        
        return RedirectToAction("Index", "Estoque");
    }

    public IActionResult DeletarUsuario(int id)
    {
        var user = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        _context.Usuarios.Remove(user);
        _context.SaveChanges();

        return RedirectToAction("Usuarios", "Usuario");
    }
    
}