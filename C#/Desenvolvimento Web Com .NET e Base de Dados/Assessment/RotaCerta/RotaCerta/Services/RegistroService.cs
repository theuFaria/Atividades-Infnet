namespace RotaCerta.Services;

public class RegistroService
{
    private Action<string> Registrar { get; set; }
    private List<string> LogsEmMemoria { get; set; } = new List<string>();

    private void LogToConsole(string msg)
    {
        Console.WriteLine(msg);
    }

    private void LogToFile(string msg)
    {
        File.AppendAllText(
            "C:\\Documentos_fora_do_oneDrive\\Estudos\\Facul\\BackEnd\\CSharp\\projetos\\RotaCerta\\RotaCerta\\wwwroot\\Files\\logs.txt",
            msg + Environment.NewLine);
    }

    private void LogToMemory(string msg)
    {
        LogsEmMemoria.Add(msg);
    }

    public RegistroService()
    {
        Registrar = LogToConsole;
        Registrar += LogToFile;
        Registrar += LogToMemory;
    }

    public void ChamarRegistro(string msg)
    {
        Registrar(msg);
    }
}