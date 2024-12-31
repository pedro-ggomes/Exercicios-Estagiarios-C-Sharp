using System;

class LivroDigital : Livro
{
    // Atributos
    public double TamanhoArquivo {get;set;}

    // Construtor
    public LivroDigital(string Titulo, string Autor, int AnoPublicacao, double TamanhoArquivoMB) : base(Titulo,Autor,AnoPublicacao)
    {
        this.TamanhoArquivo = TamanhoArquivoMB;

    }

    //Métodos
    public override void ExibirDetalhes()
    {
      base.ExibirDetalhes();
      Console.WriteLine($"O tamanho do arquivo é {this.TamanhoArquivo} MB."); 
        
    }

}