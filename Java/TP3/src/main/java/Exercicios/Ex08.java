package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex08 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/sim/entities");

        String payload = """
                {
                  "name": "atualizado"
                }""";

        ApiResponse api = helper.post("/10", "PUT", payload, null);

        System.out.println("Código: " + api.getCodigo());
        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Corpo: " + api.getCorpo());

        System.out.println("Resultado com Post: ");

        api = helper.post("/10", "POST", payload, null);

        System.out.println("Código: " + api.getCodigo());
        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Corpo: " + api.getCorpo());
    }
}
