using AbstractFactory.Aircrafts;
using AbstractFactory.Landvehicles;

namespace AbstractFactory.Factories
{
    interface ITransportFactory
    {
        IAircraft CreateTransportAircraft();
        ILandvehicle CreateTranspostVehicle();
    }
}
