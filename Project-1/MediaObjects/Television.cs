namespace Project1;

public class Television : IMedia
{
    public string Name { get; set; } = string.Empty;

    public string Report(Airport airport)
    {
        return $"{Name} An image of {airport.Name} airport";
    }

    public string Report(CargoPlane cargoPlane)
    {
        return $"{Name} An image of {cargoPlane.Serial} cargo plane";
    }

    public string Report(PassengerPlane passengerPlane)
    {
        return $"{Name} An image of {passengerPlane.Serial} flight plane";
    }
}