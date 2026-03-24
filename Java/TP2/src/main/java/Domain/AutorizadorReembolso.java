package Domain;

public interface AutorizadorReembolso {
    boolean isAutorizado(Consulta consulta, Paciente paciente);
}
