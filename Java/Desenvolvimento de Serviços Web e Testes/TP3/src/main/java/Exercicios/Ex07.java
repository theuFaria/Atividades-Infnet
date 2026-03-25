package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex07 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/sim/entities");

        String payload = """
                {
                  "name": "atualizado"
                }""";

        ApiResponse api = helper.post("/10", "POST", payload, null);

        //API não atualiza
        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Corpo: " + api.getCorpo());

    }
}
