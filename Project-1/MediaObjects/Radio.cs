namespace Project1;

public class Radio : IMedia
{
    public string Name { get; set; } = string.Empty;
    public string Report(Airport airport)
    {
        return $"Reporting for {Name}, Ladies and Gentlemen, we are at the {airport.Name} airport";
    }
    public string Report(CargoPlane cargoPlane)
    {
        return $"Reporting for {Name}, Ladies and Gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
    }
    public string Report(PassengerPlane passengerPlane)
    {
        return $"Reporting for {Name}, Ladies and Gentlemen, we’ve just witnessed {passengerPlane.Serial} takeoff.";
    }
}