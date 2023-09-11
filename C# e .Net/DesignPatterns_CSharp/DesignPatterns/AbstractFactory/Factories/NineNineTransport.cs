using AbstractFactory.Aircrafts;
using AbstractFactory.Landvehicles;

namespace AbstractFactory.Factories
{
    class NineNineTransport : ITransportFactory
    {
        public IAircraft CreateTransportAircraft()
        {
            return new Helicopter();
        }

        public ILandvehicle CreateTranspostVehicle()
        {
            return new Motorcycle();
        }
    }
}
