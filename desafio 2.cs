using System;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
    public class Produto
    {
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; }
        public int estoque { get; set; }
    }

    public class DadosEstoque
    {
        public List<Produto> estoque { get; set; }
    }

    public class Movimentacao
    {
        public Guid Id { get; set; }
        public int CodigoProduto { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }

    static void Main()
    {
        string json = @"
        {
	        ""estoque"":
	        [
	          {
		        ""codigoProduto"": 101,
		        ""descricaoProduto"": ""Caneta Azul"",
		        ""estoque"": 150
	          },
	          {
		        ""codigoProduto"": 102,
		        ""descricaoProduto"": ""Caderno Universitário"",
		        ""estoque"": 75
	          },
	          {
		        ""codigoProduto"": 103,
		        ""descricaoProduto"": ""Borracha Branca"",
		        ""estoque"": 200
	          },
	          {
		        ""codigoProduto"": 104,
		        ""descricaoProduto"": ""Lápis Preto HB"",
		        ""estoque"": 320
	          },
	          {
		        ""codigoProduto"": 105,
		        ""descricaoProduto"": ""Marcador de Texto Amarelo"",
		        ""estoque"": 90
	          }
	        ]
        }";

        DadosEstoque dados = JsonSerializer.Deserialize<DadosEstoque>(json);

        List<Movimentacao> movimentacoes = new List<Movimentacao>();

        while (true)
        {
            Console.WriteLine("\n--- Movimentação de Estoque ---");
            Console.Write("Informe o código do produto (ou 0 para sair): ");
            int codigo = int.Parse(Console.ReadLine());

            if (codigo == 0)
                break;

            Produto produto = dados.estoque.Find(p => p.codigoProduto == codigo);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                continue;
            }

            Console.WriteLine($"Produto: {produto.descricaoProduto}");
            Console.WriteLine($"Estoque atual: {produto.estoque}");

            Console.Write("Digite 'E' para Entrada ou 'S' para Saída: ");
            string tipo = Console.ReadLine().ToUpper();

            Console.Write("Quantidade da movimentação: ");
            int quantidade = int.Parse(Console.ReadLine());

            if (tipo == "S" && quantidade > produto.estoque)
            {
                Console.WriteLine("Não há estoque suficiente para essa saída!");
                continue;
            }

            // Registrar movimentação
            Movimentacao mov = new Movimentacao()
            {
                Id = Guid.NewGuid(),
                CodigoProduto = codigo,
                Quantidade = quantidade * (tipo == "S" ? -1 : 1),
                Descricao = tipo == "E" ? "Entrada de Mercadoria" : "Saída de Mercadoria",
                Data = DateTime.Now
            };

            movimentacoes.Add(mov);

            // Atualiza estoque
            produto.estoque += mov.Quantidade;

            Console.WriteLine("\nMovimentação registrada com sucesso!");
            Console.WriteLine($"ID: {mov.Id}");
            Console.WriteLine($"Descrição: {mov.Descricao}");
            Console.WriteLine($"Quantidade movimentada: {mov.Quantidade}");
            Console.WriteLine($"Estoque final do produto: {produto.estoque}");
        }

        Console.WriteLine("\nFim do programa.");
    }
}
