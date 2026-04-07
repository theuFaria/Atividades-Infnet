namespace RotaCerta.Services;

public class CapacidadeService
{
    public delegate void CapacityReachedHandler(string msg);

    public event CapacityReachedHandler CapacityReached;

    public void DispararEvento()
    {
        CapacityReached?.Invoke("Não há mais reservas disponíveis para esse pacote");
    }
}