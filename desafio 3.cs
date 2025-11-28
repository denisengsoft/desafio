using System;

class Program
{
    static void Main()
    {
        Console.Write("Digite o valor original: R$ ");
        double valor = double.Parse(Console.ReadLine());

        Console.Write("Digite a data de vencimento (dd/mm/aaaa): ");
        DateTime vencimento = DateTime.Parse(Console.ReadLine());

        DateTime hoje = DateTime.Today;

        int diasAtraso = (hoje - vencimento).Days;

        if (diasAtraso <= 0)
        {
            Console.WriteLine("\nA conta não está em atraso.");
            Console.WriteLine($"Valor final: R$ {valor:F2}");
        }
        else
        {
            double juros = valor * 0.025 * diasAtraso;
            double valorFinal = valor + juros;

            Console.WriteLine($"\nDias de atraso: {diasAtraso}");
            Console.WriteLine($"Juros calculado: R$ {juros:F2}");
            Console.WriteLine($"Valor final atualizado: R$ {valorFinal:F2}");
        }
    }
}
