package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex09 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/sim/entities");

        helper.request("/9", "DELETE");

        ApiResponse api =  helper.request("/9", "GET");

        System.out.println("Mensagem: "+api.getMensagem());
        System.out.println("Código: "+api.getCodigo());
        System.out.println("Corpo: "+api.getCorpo());

    }
}
