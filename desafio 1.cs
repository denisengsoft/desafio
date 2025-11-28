using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    public class Venda
    {
        public string vendedor { get; set; }
        public double valor { get; set; }
    }

    public class DadosVendas
    {
        public List<Venda> vendas { get; set; }
    }

    static void Main()
    {
        string caminho = "vendas.json";

        if (!File.Exists(caminho))
        {
            Console.WriteLine("Arquivo JSON não encontrado!");
            return;
        }

        string json = File.ReadAllText(caminho);
        DadosVendas dados = JsonSerializer.Deserialize<DadosVendas>(json);

        Dictionary<string, double> comissoes = new Dictionary<string, double>();

        foreach (var venda in dados.vendas)
        {
            double comissao = 0;

            if (venda.valor < 100)
                comissao = 0;
            else if (venda.valor < 500)
                comissao = venda.valor * 0.01;
            else
                comissao = venda.valor * 0.05;

            if (!comissoes.ContainsKey(venda.vendedor))
                comissoes[venda.vendedor] = 0;

            comissoes[venda.vendedor] += comissao;
        }

        Console.WriteLine("COMISSÕES CALCULADAS:\n");

        foreach (var item in comissoes)
        {
            Console.WriteLine($"{item.Key}: R$ {item.Value:F2}");
        }
    }
}
