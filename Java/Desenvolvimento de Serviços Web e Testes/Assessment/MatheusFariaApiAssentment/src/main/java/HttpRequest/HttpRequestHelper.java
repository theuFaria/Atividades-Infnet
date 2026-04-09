package HttpRequest;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;

public class HttpRequestHelper {

    private final String Url;

    //Pegar a url "Geral"
    public HttpRequestHelper(String url) {
        Url = url;
    }

    private HttpURLConnection prossUrl(String caminho) throws IOException {
        String entidadesUrl = Url;

        if (caminho != null) {
            entidadesUrl += caminho;
        }

        URL url = new URL(entidadesUrl);
        return (HttpURLConnection) url.openConnection();
    }

    public ApiResponse request(String caminho, String method) throws IOException {

        HttpURLConnection connection = prossUrl(caminho);
        connection.setRequestMethod(method.toUpperCase());
        connection.connect();
        int responseCode = connection.getResponseCode();
        String responseMessage = connection.getResponseMessage();

        BufferedReader reader;
        if (responseCode >= 200 && responseCode < 300) {
            reader = new BufferedReader(new InputStreamReader(connection.getInputStream(), StandardCharsets.UTF_8));
        } else {
            reader = new BufferedReader(new InputStreamReader(connection.getErrorStream(), StandardCharsets.UTF_8));
        }

        StringBuilder sb = new StringBuilder();
        String linha;
        while ((linha = reader.readLine()) != null) {
            sb.append(linha);
        }
        reader.close();
        connection.disconnect();

        return new ApiResponse(responseCode, responseMessage, sb.toString(), connection.getURL().toString());
    }


    public ApiResponse post(String caminho, String payload, String contentType) throws IOException {

        HttpURLConnection connection = prossUrl(caminho);

        connection.setRequestMethod("POST");
        connection.setDoOutput(true);
        connection.setConnectTimeout(5000);
        connection.setReadTimeout(5000);

        connection.setRequestProperty("content-type",
                contentType != null ? contentType : "application/json;charset=utf-8");

        try (DataOutputStream outputStream = new DataOutputStream(connection.getOutputStream())) {

            byte[] dados = payload.getBytes(StandardCharsets.UTF_8);
            outputStream.write(dados);
            outputStream.flush();
        }

        int responseCode = connection.getResponseCode();
        String responseMessage = connection.getResponseMessage();

        //Pega o Corpo:
        BufferedReader reader;
        if (responseCode >= 200 && responseCode < 300) {
            reader = new BufferedReader(new InputStreamReader(connection.getInputStream(), StandardCharsets.UTF_8));
        } else {
            reader = new BufferedReader(new InputStreamReader(connection.getErrorStream(), StandardCharsets.UTF_8));
        }

        StringBuilder sb = new StringBuilder();
        String linha;
        while ((linha = reader.readLine()) != null) {
            sb.append(linha);
        }

        reader.close();
        connection.disconnect();

        return new ApiResponse(responseCode, responseMessage, sb.toString(), connection.getURL().toString());

    }


    public ApiResponse options(String caminho) throws IOException {
        HttpURLConnection connection = prossUrl(caminho);

        connection.setRequestMethod("OPTIONS");
        connection.connect();

        int responseCode = connection.getResponseCode();

        String allowHeader = connection.getHeaderField("Allow");

        connection.disconnect();

        return new ApiResponse(responseCode, allowHeader);
    }

}