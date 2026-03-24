package HistoricoConsultas;

import Domain.Consulta;
import Domain.Paciente;

import java.util.List;

public interface HistoricoConsultas {

    void incluir(Consulta consulta, Paciente paciente);
    List<Consulta> obterConsultas(Paciente paciente);
}
