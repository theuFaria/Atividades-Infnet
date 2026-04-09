package app;
import HttpRequest.ApiResponse;
import HttpRequest.HttpRequestHelper;
import java.io.IOException;

public class Main {

    public static void main(String[] args) throws IOException {
        //Cria o Javalin
        ItemRoutes.CreateJavalin().start(7000);

        //POST - Criar um item na lista
        HttpRequestHelper helper = new HttpRequestHelper("http://localhost:7000/itens"); // Cliente Rest+

        String jsonInput = """
                {
                    "id": 1,
                    "nome": "Mouse",
                    "preco": 150.99,
                    "categoria": "Eletronico"
                }
                """;

        ApiResponse api = helper.post("/create", jsonInput, null); // Chamada da api

        System.out.println("Código: " + api.getCodigo());
        System.out.println("Mensagem: " + api.getMensagem());


        //GET - lista todos os items
        api = helper.request(null, "GET");

        System.out.println("Código: " + api.getCodigo());
        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Corpo: " + api.getCorpo());

        //GET - Pelo Id
        api = helper.request("/1", "GET");

        System.out.println("Código: " + api.getCodigo());
        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Corpo: " + api.getCorpo());

        //GET - status
        helper = new HttpRequestHelper("http://localhost:7000");

        api = helper.request("/status", "GET");

        System.out.println("Código: " + api.getCodigo());
        System.out.println("Mensagem: " + api.getMensagem());
        System.out.println("Corpo: " + api.getCorpo());
    }
}
