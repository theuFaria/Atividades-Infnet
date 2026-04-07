namespace RotaCerta.Services;

public class DescontoService
{
    public delegate decimal CalcularDesconto(decimal valor);

    public decimal AplicarDesconto(decimal valor)
    {
        return valor *  0.9m;
    }
}