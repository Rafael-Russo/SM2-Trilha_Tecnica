using System;

namespace AbstractFactory.Aircrafts
{
    class Helicopter : IAircraft
    {
        public void CheckWind()
        {
            Console.WriteLine("Verificando Vento! Vento sudeste. Ventos OK!");
        }

        public void GetCargo()
        {
            Console.WriteLine("Passageiros OK! Ligando as Hélices!");
        }

        public void StartRoute()
        {
            CheckWind();
            GetCargo();
            Console.WriteLine("Iniciando Decolagem!");
        }
    }
}
