namespace WebAppTwoPointsHospital.Models;

public class Lote
{
    public Medicamento Medicamento { get; set; }
    public string NumeroLote { get; set; }
    public int Quantidade { get; set; }

    public Lote(string numeroLote, int quantidade)
    {
        NumeroLote = numeroLote;
        Quantidade = quantidade;
    }


    public bool ConsultarDisponibilidade()
    {
        return Quantidade > 0;
    }

    public void AdicionarMedicamento(int quantidadeMedicamento)
    {
        Quantidade += quantidadeMedicamento;
    }

    public void RetirarMedicamento(int quantidade)
    {
        Quantidade -= quantidade;
    }
}