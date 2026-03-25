package HistoricoConsultas;

import Domain.Consulta;
import Domain.Paciente;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class HistoricoConsultasFake implements HistoricoConsultas{

    private Map<Paciente, List<Consulta>> historico = new HashMap<>();

    @Override
    public void incluir(Consulta consulta, Paciente paciente) {
        historico.computeIfAbsent(paciente, k -> new ArrayList<Consulta>()).add(consulta);
    }

    public List<Consulta> obterConsultas(Paciente paciente) {
        return historico.getOrDefault(paciente, new ArrayList<>());
    }
}
