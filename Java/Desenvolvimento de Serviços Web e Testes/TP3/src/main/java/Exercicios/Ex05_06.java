package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex05_06 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/sim/entities");

        System.out.println("Exercício 5: ");
        System.out.println("");
        //Exercicio 5:

        String payload = """
                {
                  "name": "aluno"
                }""";

        ApiResponse api = helper.post(null, "POST", payload, null);

        System.out.println("Corpo: "+api.getCorpo());


        //Fim do exercício.
        System.out.println("");
        System.out.println("Exercício 6: ");
        System.out.println("");

        //Exercicio 6:
        api = helper.request("/11", "GET");

        System.out.println("Corpo: "+api.getCorpo());

        // Fim do exercício.

    }
}
