using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RotaCerta.Models;

namespace RotaCerta.Controllers;

public class AnotacaoController : Controller
{
    private readonly string _caminho;

    private readonly UserManager<ApplicationUser> _userManager;

    public AnotacaoController(IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
    {
        _caminho = Path.Combine(env.WebRootPath, "Files");
        _userManager = userManager;
    }

    private List<string> GetArquivos()
    {
        return Directory.GetFiles(_caminho, "*.txt")
            .Select(Path.GetFileName)
            .ToList();
    }

    // GET - Vai para ViewNotes
    [HttpGet]
    public IActionResult ViewNotes()
    {
        List<string> arquivos = GetArquivos();

        return View(new Anotacao() { Arquivos = arquivos });
    }
    
    
    //POST - Cria a anotação
    [HttpPost]
    public IActionResult ViewNotes(Anotacao anotacao)
    {
        if (!ModelState.IsValid)
        {
            return View(anotacao);
        }

        //Arquivo sempre termina com txt
        if (!anotacao.Titulo.EndsWith(".txt"))
        {
            anotacao.Titulo += ".txt";
        }

        string caminhoCompleto = Path.Combine(_caminho, Path.GetFileName(anotacao.Titulo));

        System.IO.File.WriteAllText(caminhoCompleto, anotacao.Descricao);

        TempData["Alert"] = "Anotação criada com sucesso!";

        return RedirectToAction("ViewNotes");
    }
    
    //GET - Vai para ViewNote
    [HttpGet]
    public IActionResult ViewNote(string fileName)
    {
        string caminhoCompleto = Path.Combine(_caminho, Path.GetFileName(fileName));
        
        string conteudo = System.IO.File.ReadAllText(caminhoCompleto);

        var anotacao = new Anotacao
        {
            Titulo = fileName,
            Descricao = conteudo,
            Arquivos = GetArquivos()
        };

        return View("ViewNote", anotacao);
    }
}