using System;

namespace AbstractFactory.Landvehicles
{
    class Car : ILandvehicle
    {
        public void GetCrago()
        {
            Console.WriteLine("Pegamos os passageiros!");
        }

        public void StartRoute()
        {
            GetCrago();
            Console.WriteLine("Iniciando o trajeto!");
        }
    }
}
