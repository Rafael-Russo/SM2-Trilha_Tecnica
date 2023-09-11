using System;

namespace AbstractFactory.Aircrafts
{
    class Airplane : IAircraft
    {
        public void CheckWind()
        {
            Console.WriteLine("Verificando os ventos! Ventos a 25km. Ventos OK!");
        }

        public void GetCargo()
        {
            Console.WriteLine("Passageiros à bordo, Voô autorizado!");
        }

        public void StartRoute()
        {
            CheckWind();
            GetCargo();
            Console.WriteLine("Iniciando decolagem!");
        }
    }
}
