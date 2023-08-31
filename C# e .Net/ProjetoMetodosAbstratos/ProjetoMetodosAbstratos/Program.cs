using ProjetoMetodosAbstratos.Entities;
using System.Collections.Generic;
using System.Globalization;

namespace ProjetoMetodosAbstratos
{
    class Program {
        static void Main(string[] args) {
            List<Account> lista = new List<Account>();

            Console.Write("Enter the number of tax payers: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 1;  i <= n; i++) {
                Console.WriteLine($"Tax payer #{i} data: ");
                Console.Write("Individual or Company (i/c)? ");
                char tipo = char.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string nome = Console.ReadLine();
                Console.Write("Anual income: ");
                double renda = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                if (tipo == 'i') {
                    Console.Write("Health expenditures: ");
                    double gasto = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    lista.Add(new IndividualAccount(nome, renda, gasto));
                } else {
                    Console.Write("Number of employees: ");
                    int funcionarios = int.Parse(Console.ReadLine());
                    lista.Add(new CompanyAccount(nome, renda, funcionarios));
                }
            }

            Console.WriteLine();
            Console.WriteLine("TAXES PAID: ");

            double sum = 0;
            foreach(Account a in lista) {
                Console.WriteLine($"{a.Nome}: ${a.CalcularImposto().ToString("F2", CultureInfo.InvariantCulture)}");
                sum += a.CalcularImposto();
            }

            Console.WriteLine();
            Console.WriteLine("TOTAL TAXES: $" + sum.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}