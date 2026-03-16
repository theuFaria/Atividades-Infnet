using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppTwoPointsHospital.Models;

public class EstoqueController : Controller
{
    private readonly Contexto _contexto;

    public EstoqueController(Contexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]

    //Inicializa a página HTML 
    public IActionResult Consultar()
    {
        return View();
    }

    //Recebe o nome do Medicamento, quando digitado no formulário
    [HttpPost]
    public IActionResult Consultar(string nomeMedicamento)
    {
        if (string.IsNullOrEmpty(nomeMedicamento)) return View();

        //Chama o metódo já instaciado onde os medicamentos já estão cadastrados
        Estoque estoque = new Estoque();
        //Consulta os medicamentos já cadastrados
        Medicamento remedio = estoque.ConsultarMedic(nomeMedicamento);

        if (remedio == null)
        {
            //Mensagem informando sobre o status do medicamento
            ViewBag.Mensagem = $"O medicamento {nomeMedicamento}, não está disponivel na unidade";
        }
        else
        {
            if (remedio.LoteAtual.ConsultarDisponibilidade())
            {
                ViewBag.Mensagem = $"O medicamento {remedio.Nome}, está disponivel na unidade";
            }
            else
            {
                ViewBag.Mensagem = $"O medicamento {remedio.Nome}, está sem disponivel na unidade";
            }
        }

        return View();
    }

    [HttpGet]
    public IActionResult Movimentar()
    {
        Estoque estoque = new Estoque();
        ViewBag.ListaMedicamentos = new SelectList(Estoque.ListaMedicamentos, "Nome", "Nome");
        return View();
    }

    [HttpPost]
    public IActionResult Movimentar(string nomeM, string tipoMovimentacao, int quantidade)
    {
        Estoque estoque = new Estoque();
        Medicamento remedio = estoque.ConsultarMedic(nomeM);


        if (remedio == null)
        {
            ViewBag.Mensagem = $"Medicamento {nomeM} não encontrado. Faça o Cadastro do medicamento primeiro.";
            ViewBag.ListaMedicamentos = new SelectList(Estoque.ListaMedicamentos, "Nome", "Nome");
            return View();
        }

        if (tipoMovimentacao == "Entrada")
        {
            remedio.LoteAtual.AdicionarMedicamento(quantidade);
            ViewBag.Mensagem = $"Adicionado {quantidade} unidades do medicamento {nomeM}";
        }
        else if (tipoMovimentacao == "Saida")

        {
            if (remedio.LoteAtual.Quantidade >= quantidade)
            {
                remedio.LoteAtual.RetirarMedicamento(quantidade);
                ViewBag.Mensagem = $"Retirado {quantidade} unidades do medicamento {nomeM}";
            }
            else
            {
                ViewBag.Mensagem =
                    $"Quantidade insuficiente para retirada, estão disponiveis apenas {remedio.LoteAtual.Quantidade} de .";
            }
        }

        ViewBag.ListaMedicamentos = new SelectList(Estoque.ListaMedicamentos, "Nome", "Nome");
        return View();
    }

    // Get -> Vai para a página Index
    [HttpGet]
    public IActionResult Listar()
    {
        Estoque estoque = new Estoque();
        return View(Estoque.ListaMedicamentos);
    }

    [HttpGet]
    public IActionResult Index()
    {
        Estoque estoque = new Estoque();

        var dados = new ViewModel();

        dados.TotalMedicamentos = Estoque.ListaMedicamentos.Count;
        dados.TotalUsuarios = _contexto.Usuarios.Count();

        return View(dados);
    }
    
    
}