package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex04 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/sim/entities");

        ApiResponse api = helper.request("?tipo=voador&tipo2=terra", "GET");

        System.out.println("Código: " + api.getCodigo());
        System.out.println("URL: " + api.getURL());

    }
}
