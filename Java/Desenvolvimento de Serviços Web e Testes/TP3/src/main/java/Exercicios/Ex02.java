package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex02 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/sim/entities");

        ApiResponse api = helper.requestById(1);

        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Código: " + api.getCodigo());
        System.out.println("Corpo: " + api.getCorpo());

        System.out.println("ID: 2");

        api = helper.requestById(2);

        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Código: " + api.getCodigo());
        System.out.println("Corpo: " + api.getCorpo());


        api = helper.request(null, "GET");
        System.out.println("Corpo completo de todas as entidades: " + api.getCorpo());

    }
}
