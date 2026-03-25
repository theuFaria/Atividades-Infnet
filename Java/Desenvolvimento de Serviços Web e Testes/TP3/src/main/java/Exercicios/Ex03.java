package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex03 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/sim/entities");

        ApiResponse api = helper.requestById(0);

        System.out.println("Mensagem: "+api.getMensagem());
        System.out.println("Código: "+api.getCodigo());
        System.out.println("Corpo: "+api.getCorpo());

    }
}
