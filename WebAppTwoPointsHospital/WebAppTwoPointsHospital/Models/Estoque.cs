namespace WebAppTwoPointsHospital.Models
{
    public class Estoque
    {
        public static List<Medicamento> ListaMedicamentos { get; set; } = new List<Medicamento>();

        public Estoque()
        {
            if (ListaMedicamentos.Count == 0)
            {
                CadastrarMedicamento("Dipirona", "00001", 50);
                CadastrarMedicamento("Paracetamol", "00002", 50);
                CadastrarMedicamento("Amoxicilina", "00003", 50);
                CadastrarMedicamento("Fluoxetina", "00004", 50);
                CadastrarMedicamento("Insulina", "00005", 50);
                CadastrarMedicamento("Atenolol", "00006", 50);
            }
        }

        public void CadastrarMedicamento(string nome, string numeroLote, int quantidadeI)
        {
            Lote novoLote = new Lote(numeroLote, quantidadeI);
            Medicamento novoMedicamento = new Medicamento(nome, novoLote);
            ListaMedicamentos.Add(novoMedicamento);
        }

        public Medicamento ConsultarMedic(string NomeM)
        {
            return ListaMedicamentos.FirstOrDefault(mbox => mbox.Nome.Equals(NomeM));
        }

        public int ObterQuantidadeDeMedicamentos()
        {
            return ListaMedicamentos.Count;
        }
        
    }
}