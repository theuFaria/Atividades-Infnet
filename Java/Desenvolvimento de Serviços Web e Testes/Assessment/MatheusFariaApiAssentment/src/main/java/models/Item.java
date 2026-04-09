package models;

public class Item {
    private int id;
    private String nome;
    private double preco;
    private Categoria categoria;

    public double getPreco() {
        return preco;
    }

    public String getNome() {
        return nome;
    }


    public int getId() {
        return id;
    }

    public Categoria getCategoria() {
        return categoria;
    }

    public Item(int id, double preco, String nome, Categoria categoria) {
        this.id = id;
        this.preco = preco;
        this.nome = nome;
        this.categoria = categoria;
    }

    public Item() {
    }
}
