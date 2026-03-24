package Auditoria;
import Domain.Paciente;
import Domain.Consulta;

public interface Auditoria {
    void registrarConsulta(Consulta consulta, Paciente paciente);
}
