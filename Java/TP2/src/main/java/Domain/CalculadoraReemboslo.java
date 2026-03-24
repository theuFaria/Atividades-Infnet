package Domain;

import Auditoria.Auditoria;
import Auditoria.AuditoriaSpy;
import Exceptions.NaoAutorizadoException;

public class CalculadoraReemboslo {

    private final Auditoria auditoria;
    private final double valorMaximo = 150;

    public CalculadoraReemboslo(AuditoriaSpy auditoria) {
        this.auditoria = auditoria;
    }

    public double calcularReembolso(Consulta consulta, Paciente paciente, PlanoDeSaude planoDeSaude) {
        double reembolso = consulta.getValor() * planoDeSaude.obterPorcentualDeCobertura(paciente);
        auditoria.registrarConsulta(consulta, paciente);

        if (reembolso > valorMaximo) {
            return valorMaximo;
        } else {
            return reembolso;
        }
    }

    public double calcularReembolsoSeAutorizado(
            Consulta consulta, Paciente paciente, PlanoDeSaude planoDeSaude, AutorizadorReembolso autorizacao
    ) throws NaoAutorizadoException {
        if (autorizacao.isAutorizado(consulta, paciente)) {
            return calcularReembolso(consulta, paciente, planoDeSaude);
        } else {
            throw new NaoAutorizadoException();
        }
    }

}
