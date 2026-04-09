import app.ItemRoutes;
import com.fasterxml.jackson.databind.ObjectMapper;
import io.javalin.Javalin;
import io.javalin.testtools.JavalinTest;
import models.Categoria;
import models.Item;
import okhttp3.Response;
import org.junit.jupiter.api.*;


import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class ItemTest {

    private Item itemJson;

    @BeforeEach
    void SetUp() {
        itemJson = new Item(1, 10, "Barra de chocolate", Categoria.Alimento);
    }


    @Test
    @DisplayName("deveRetornarHelloJavalin")
    public void deveRetornarHelloJavalin() {
        //Arrange
        try (Javalin app = ItemRoutes.CreateJavalin()) {

            JavalinTest.test(app, (server, client) -> {
                //Action
                Response response = client.get("/hello");
                String body = response.body().string();

                //Assert
                assertEquals(200, response.code());
                assertEquals("Hello, Javalin!", body);

            });
        }
    }

    @Test
    @DisplayName("deveCriarItem")
    public void deveCriarItem() {
        //Arrange
        try (Javalin app = ItemRoutes.CreateJavalin()) {
            //Action
            JavalinTest.test(app, (server, client) -> {

                Response response = client.post("/itens/create", itemJson);
                String body = response.body().string();

                //Assert
                assertEquals(201, response.code());
            });
        }


    }

    @Test
    @DisplayName("testeGetParam")
    public void testGetParam() {
        //Arrange
        try (Javalin app = ItemRoutes.CreateJavalin()) {
            //Action
            JavalinTest.test(app, (server, client) -> {

                ObjectMapper mapper = new ObjectMapper();
                String json = mapper.writeValueAsString(itemJson);

                client.post("/itens/create", json);


                Response response = client.get("/itens/1");
                String body = response.body().string();

                //Transforma o texto da string em um Objeto Item
                Item item = mapper.readValue(body, Item.class);

                //Assert
                assertEquals(itemJson.getId(), item.getId());
                assertEquals(itemJson.getNome(), item.getNome());
                assertEquals(itemJson.getPreco(), item.getPreco());
                assertEquals(itemJson.getCategoria(), item.getCategoria());
            });
        }

    }

    @Test
    @DisplayName("testGetListagem")
    public void testGetListagem() {
        //Arrange
        try (Javalin app = ItemRoutes.CreateJavalin()) {
            //Action
            JavalinTest.test(app, (server, client) -> {

                ObjectMapper mapper = new ObjectMapper();
                String json = mapper.writeValueAsString(itemJson);

                client.post("/itens/create", json);

                Response response = client.get("/itens");
                String body = response.body().string();

                List<Item> items = mapper.readValue(body, List.class);

                assertEquals(1, items.size());

            });
        }
    }

}

