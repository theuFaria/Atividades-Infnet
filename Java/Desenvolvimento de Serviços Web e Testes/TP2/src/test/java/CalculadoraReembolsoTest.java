import Auditoria.AuditoriaSpy;
import Domain.*;
import Exceptions.NaoAutorizadoException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class CalculadoraReembolsoTest {

    private Consulta consulta;
    private Paciente paciente;
    private CalculadoraReemboslo calculadoraReembolso;
    private final AuditoriaSpy auditoria = new AuditoriaSpy();
    private AutorizadorReembolso autorizadorReembolsoMock;

    private PlanoDeSaude criarPlanoDeSaudeStub(double porcentagem) {
        return new PlanoDeSaude() {
            @Override
            public double obterPorcentualDeCobertura(Paciente paciente) {
                return porcentagem;
            }
        };
    }

    private Consulta criarConsultaStub(double valor) {
        Consulta consulta = new Consulta() {};
        consulta.setValor(valor);
        return consulta;
    }

    private void assertDoubleEquals(double esperado, double atual) {
        assertEquals(esperado, atual, 0.01);
    }

    @BeforeEach
    void setUp() {
        consulta = new Consulta();
        paciente = new Paciente();
        calculadoraReembolso = new CalculadoraReemboslo(auditoria);
        autorizadorReembolsoMock = mock(AutorizadorReembolso.class);
    }

    @Test
    @DisplayName("Deve calcular o reembolso para uma consulta de 200 reais com 70% de cobertura")
    void deveCalcularReembolso() {
        //Arrange
        Consulta consulta = criarConsultaStub(200);
        PlanoDeSaude plano = criarPlanoDeSaudeStub(0.7);
        //Act
        double reembolso = calculadoraReembolso.calcularReembolso(consulta, paciente, plano);
        // Assert
        assertEquals(140, reembolso, 0.001);
    }

    @Test
    @DisplayName("Deve calcular o reembolso para consultas ou porcentuais de cobertura de 0 ou 100")
    void deveCalcularReembolsoParaConsultaGratisOu100Ou0PorcentoDeCobertura() {
        //Arrange : Domain.Consulta de 500 reais com porcentual de cobertura de 0
        Consulta consulta = criarConsultaStub(500);
        PlanoDeSaude plano = criarPlanoDeSaudeStub(0);
        //Act
        double reembolso = calculadoraReembolso.calcularReembolso(consulta, paciente, plano);
        //Assert
        assertDoubleEquals(0, reembolso);
    }

    @Test
    @DisplayName("Deve calcular reembolso para plano de saúde de 80%")
    void deveCalcularReembolsoParaPlanoDeSaude80() {
        //Arrange
        Consulta consulta = criarConsultaStub(100);
        PlanoDeSaude plano = criarPlanoDeSaudeStub(0.8);
        //Act
        double reembolso = calculadoraReembolso.calcularReembolso(consulta, paciente, plano);
        //Assert
        assertDoubleEquals(80, reembolso);
    }

    @Test
    @DisplayName("Deve garantir que o spy está registrando a ultima consulta e o ultimo paciente")
    void deveGarantirQueSpyRegistraUltimaConsultaEPaciente() {
        //Act
        auditoria.registrarConsulta(consulta, paciente);
        //Assert
        assertTrue(auditoria.isFoiChamado());
        assertEquals(consulta, auditoria.getUltimaConsulta());
        assertEquals(paciente, auditoria.getUultimoPaciente());
    }

    @Test
    @DisplayName("Deve jogar exceção ao não autorizar a consulta")
    void deveJogarExcecaoAoNaoAutorizarConsulta() throws NaoAutorizadoException {
        //Arrange
        //Faz com que isAutorizado seja false.
        when(autorizadorReembolsoMock.isAutorizado(consulta, paciente)).thenReturn(false);
        //Act + Assert
        assertThrows(NaoAutorizadoException.class, () ->
                calculadoraReembolso.calcularReembolsoSeAutorizado(
                        consulta,paciente,criarPlanoDeSaudeStub(0.5), autorizadorReembolsoMock));
    }

    @Test
    @DisplayName("Deve calcular o reembolso respeitando o limite máximo de  150")
    void deveCalcularReembolsoAte150() {
        //Arrange : Domain.Consulta de 500 com plano de cobertura de 50%
        Consulta consulta = criarConsultaStub(500);
        double reembolsoEsperado = 150;
        //Act
        double reembolso = calculadoraReembolso.calcularReembolso(consulta, paciente, criarPlanoDeSaudeStub(0.5));
        //Assert
        assertDoubleEquals(reembolsoEsperado, reembolso);
    }

    @Test
    @DisplayName("Deve calcular reembolso se autorizado, ver a ultima consulta e ultimo paciente")
    void deveCalcularReembolsoValidoComTeto() throws NaoAutorizadoException {
        //Arrange
        Consulta consulta = criarConsultaStub(500);
        PlanoDeSaude plano = criarPlanoDeSaudeStub(0.50);
        auditoria.registrarConsulta(consulta, paciente);
        when(autorizadorReembolsoMock.isAutorizado(consulta, paciente)).thenReturn(true);
        //Act
        double reembolso = calculadoraReembolso.calcularReembolsoSeAutorizado(consulta, paciente, plano, autorizadorReembolsoMock);

        //Assert
        assertDoubleEquals(150, reembolso);
        assertEquals(paciente, auditoria.getUultimoPaciente());
        assertEquals(consulta, auditoria.getUltimaConsulta());
    }
}
