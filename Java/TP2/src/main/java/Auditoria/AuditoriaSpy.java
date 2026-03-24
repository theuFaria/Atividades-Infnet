package Auditoria;
import Domain.Consulta;
import Domain.Paciente;

public class AuditoriaSpy implements Auditoria {

    public Paciente getUultimoPaciente() {
        return uultimoPaciente;
    }

    public boolean isFoiChamado() {
        return foiChamado;
    }

    public Consulta getUltimaConsulta() {
        return ultimaConsulta;
    }

    private boolean foiChamado = false;
    private Consulta ultimaConsulta;
    private Paciente uultimoPaciente;

    public void resetar() {
        foiChamado = false;
        uultimoPaciente = null;
        ultimaConsulta = null;
    }

    @Override
    public void registrarConsulta(Consulta consulta, Paciente paciente) {
        ultimaConsulta = consulta;
        uultimoPaciente = paciente;
        foiChamado = true;
    }
}
