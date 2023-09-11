using System;

namespace AbstractFactory.Landvehicles
{
    class Motorcycle : ILandvehicle
    {
        public void GetCrago()
        {
            Console.WriteLine("Pegamos a encomenda!");
        }

        public void StartRoute()
        {
            GetCrago();
            Console.WriteLine("Iniciando a entrega!");
        }
    }
}
