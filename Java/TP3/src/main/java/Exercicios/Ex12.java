package Exercicios;

import Helpers.ApiResponse;
import Helpers.HttpRequestHelper;

import java.io.IOException;

public class Ex12 {
    public static void main(String[] args) throws IOException {

        HttpRequestHelper helper = new HttpRequestHelper("https://apichallenges.eviltester.com/simpleapi");

        //Exercício A: Pegar todos os itens da API
        //---------------------------
        System.out.println("A:");
        ApiResponse api = helper.request("/items", "GET");

        System.out.println("Código: " + api.getCodigo());
        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Corpo: " + api.getCorpo());

        //---------------------------------------
        //Exercício B: Gerar isbn
        System.out.println("B:");
        ApiResponse api2 = helper.request("/randomisbn", "GET");

        System.out.println("Mensagem: " + api2.getMensagem());
        System.out.println("Código: " + api2.getCodigo());
        String isbn = api2.getCorpo();
        System.out.println("ISBN: " + isbn);

        //---------------------------------
        //Exercicio C: Criar item  com o isbn gerado
        System.out.println("C:");

        String payload = "{\n" +
                "  \"type\": \"book\",\n" +
                "  \"isbn13\": \"" + isbn + "\",\n" +
                "  \"price\": 5.99,\n" +
                "  \"numberinstock\": 5\n" +
                "}";

        ApiResponse api3 = helper.post("/items", "POST", payload, null);

        System.out.println("Mensagem: " + api3.getMensagem());
        System.out.println("Código: " + api3.getCodigo());
        System.out.println("Corpo: " + api3.getCorpo());

        //Pega o ID:
        String[] partes = api3.getCorpo().split(",");
        int id = Integer.parseInt(partes[0].replaceAll("\\D", ""));

        //-------------------------------
        //Exercício D: Atualizar o item do isbn criado
        System.out.println("D:");

        String payload2 = "{\n" +
                "  \"type\": \"book\",\n" +
                "  \"isbn13\": \"" + isbn + "\",\n" +
                "  \"price\": 2.99,\n" +
                "  \"numberinstock\": 2\n" +
                "}";

        ApiResponse api4 = helper.post("/items/" + id,"PUT", payload2, null);

        System.out.println("Mensagem: " + api4.getMensagem());
        System.out.println("Código: " + api4.getCodigo());
        System.out.println("Corpo: " + api4.getCorpo());
        //--------------------------------
        //Exercicio E: Deletar item do o isbn criado
        System.out.println("E:");

        ApiResponse api5 = helper.request("/items/" + id, "DELETE");

        System.out.println("Mensagem: " + api5.getMensagem());
        System.out.println("Código: " + api5.getCodigo());
    }
}
