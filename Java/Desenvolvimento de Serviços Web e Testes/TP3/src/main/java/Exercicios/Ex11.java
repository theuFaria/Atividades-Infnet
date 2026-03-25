package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex11 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/sim/entities");

        ApiResponse api =  helper.options(null);

        System.out.println("Código: " + api.getCodigo());
        System.out.println("Allow:" + api.getHeader());
    }
}
