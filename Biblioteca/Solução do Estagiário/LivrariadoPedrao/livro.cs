using System;

public class Livro
{
    // Atributos
    public string Titulo {get;set;}

    public string Autor{get;set;}
    public int AnoPublicacao{get;set;}
    // Construtor
    public Livro(string Titulo, string Autor, int AnoPublicacao)
    {
        this.Titulo = Titulo;
        this.Autor = Autor;
        this.AnoPublicacao = AnoPublicacao;
    } 
    // Método
    public virtual void ExibirDetalhes()
    {
        Console.WriteLine("Informações sobre o Livro: ");
        Console.WriteLine($"O livro {this.Titulo} foi publicado em {this.AnoPublicacao} pelo autor {this.Autor}.");   
    }    

}