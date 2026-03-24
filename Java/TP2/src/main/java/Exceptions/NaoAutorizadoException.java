package Exceptions;

public class NaoAutorizadoException extends Exception {

    public NaoAutorizadoException() {
        super("Nao autorizado");
    }

    @Override
    public String getMessage() {
        return "Domain.Consulta não autorizada";
    }
}
