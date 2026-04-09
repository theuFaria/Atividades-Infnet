package HttpRequest;

public class ApiResponse {

    private final int statusCode;
    private String statusMessage;
    private String URL;
    private String body;

    private String header;

    public ApiResponse(int statusCode, String statusMessage, String body, String URL) {
        this.statusCode = statusCode;
        this.statusMessage = statusMessage;
        this.body = body;
        this.URL = URL;
    }

    public ApiResponse(int statusCode, String header) {
        this.statusCode = statusCode;
        this.header = header;
    }

    public int getCodigo() {
        return statusCode;
    }

    public String getMensagem() {
        return statusMessage;
    }

    public String getCorpo() {
        return body;
    }

    public String getURL() {
        return URL;
    }

    public String getHeader() {
        return header;
    }

    @Override
    public String toString() {
        return "Status: " + statusCode + " " + statusMessage + "\nBody: " + body;
    }
}