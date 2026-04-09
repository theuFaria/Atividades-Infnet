package app;

import io.javalin.Javalin;
import models.Item;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ItemRoutes {
    static private List<Item> items;

    public static Javalin CreateJavalin() {
        items = new ArrayList<>();

        Javalin app = Javalin.create();

        //GET hello
        app.get("/hello", ctx -> ctx.result("Hello, Javalin!"));

        //GET status
        app.get("/status", ctx -> {

            Map<String, String> response = new HashMap<>();
            response.put("status", "OK");
            response.put("timestamp", LocalDateTime.now().toString());

            ctx.json(response);

        });

        //POST echo
        app.post("/echo", ctx -> {
            Map<String, Object> requisicao = ctx.bodyAsClass(Map.class);
            ctx.json(requisicao);
        });

        //GET saudação/{nome}
        app.get("/saudacao/{nome}", ctx -> {

            String nome = ctx.pathParam("nome");

            Map<String, String> response = new HashMap<>();

            response.put("mensagem", "Olá," + nome + "!");
            ctx.json(response);

        });

        //POST - Criação de Item
        app.post("/itens/create", ctx -> {
            Item itemJson = ctx.bodyAsClass(Item.class);
            items.add(itemJson);
            ctx.status(201).json(Map.of("mensagem", "Item criado com sucesso!"));
        });

        //Get - Pega todos os Items
        app.get("/itens", ctx -> {
            ctx.json(items);
        });

        //GET - Pega um item pelo id
        app.get("/itens/{id}", ctx -> {

            try {
                int id = Integer.parseInt(ctx.pathParam("id"));

                Item item = items.stream()
                        .filter(i -> i.getId() == id).findFirst().orElse(null);

                if (item == null) {
                    ctx.status(404).json(Map.of("mensagem", "item não encontrado!"));
                    return;
                }

                ctx.json(item);
            } catch (
                    NumberFormatException e) {
                ctx.status(400).json(Map.of("erro", "O id deve ser um numero inteiro."));
            }
        });

        return app;
    }

}
