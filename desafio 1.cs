using System;
using System.Collections.Generic;
using System.Text.Json;

class Programa
{
    public class Venda
    {
        public string vendedor { get; set; }
        public double valor { get; set; }
    }

    public class Dados
    {
        public List<Venda> vendas { get; set; }
    }

    static void Main()
    {
        string json = @"
        {
          ""vendas"": [
            { ""vendedor"": ""João Silva"", ""valor"": 1200.50 },
            { ""vendedor"": ""João Silva"", ""valor"": 950.75 },
            { ""vendedor"": ""João Silva"", ""valor"": 1800.00 },
            { ""vendedor"": ""João Silva"", ""valor"": 1400.30 },
            { ""vendedor"": ""João Silva"", ""valor"": 1100.90 },
            { ""vendedor"": ""João Silva"", ""valor"": 1550.00 },
            { ""vendedor"": ""João Silva"", ""valor"": 1700.80 },
            { ""vendedor"": ""João Silva"", ""valor"": 250.30 },
            { ""vendedor"": ""João Silva"", ""valor"": 480.75 },
            { ""vendedor"": ""João Silva"", ""valor"": 320.40 },

            { ""vendedor"": ""Maria Souza"", ""valor"": 2100.40 },
            { ""vendedor"": ""Maria Souza"", ""valor"": 1350.60 },
            { ""vendedor"": ""Maria Souza"", ""valor"": 950.20 },
            { ""vendedor"": ""Maria Souza"", ""valor"": 1600.75 },
            { ""vendedor"": ""Maria Souza"", ""valor"": 1750.00 },
            { ""vendedor"": ""Maria Souza"", ""valor"": 1450.90 },
            { ""vendedor"": ""Maria Souza"", ""valor"": 400.50 },
            { ""vendedor"": ""Maria Souza"", ""valor"": 180.20 },
            { ""vendedor"": ""Maria Souza"", ""valor"": 90.75 },

            { ""vendedor"": ""Carlos Oliveira"", ""valor"": 800.50 },
            { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1200.00 },
            { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1950.30 },
            { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1750.80 },
            { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1300.60 },
            { ""vendedor"": ""Carlos Oliveira"", ""valor"": 300.40 },
            { ""vendedor"": ""Carlos Oliveira"", ""valor"": 500.00 },
            { ""vendedor"": ""Carlos Oliveira"", ""valor"": 125.75 },

            { ""vendedor"": ""Ana Lima"", ""valor"": 1000.00 },
            { ""vendedor"": ""Ana Lima"", ""valor"": 1100.50 },
            { ""vendedor"": ""Ana Lima"", ""valor"": 1250.75 },
            { ""vendedor"": ""Ana Lima"", ""valor"": 1400.20 },
            { ""vendedor"": ""Ana Lima"", ""valor"": 1550.90 },
            { ""vendedor"": ""Ana Lima"", ""valor"": 1650.00 },
            { ""vendedor"": ""Ana Lima"", ""valor"": 75.30 },
            { ""vendedor"": ""Ana Lima"", ""valor"": 420.90 },
            { ""vendedor"": ""Ana Lima"", ""valor"": 315.40 }
          ]
        }";

        Dados dados = JsonSerializer.Deserialize<Dados>(json);

        Dictionary<string, double> comissoes = new Dictionary<string, double>();

        foreach (var venda in dados.vendas)
        {
            double comissao = 0;

            if (venda.valor < 100)
            {
                comissao = 0;
            }
            else if (venda.valor < 500)
            {
                comissao = venda.valor * 0.01;
            }
            else
            {
                comissao = venda.valor * 0.05;
            }

            if (!comissoes.ContainsKey(venda.vendedor))
                comissoes[venda.vendedor] = 0;

            comissoes[venda.vendedor] += comissao;
        }

        // Exibir resultados
        foreach (var item in comissoes)
        {
            Console.WriteLine($"{item.Key}: R${item.Value:F2}");
        }
    }
}
