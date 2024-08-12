namespace Project1;

public class Newspaper : IMedia
{
    public string Name { get; set; } = string.Empty;
    public string Report(Airport airport)
    {
        return $"{Name} - A report from the {airport.Name} airport, {airport.CountryIso}.";
    }
    public string Report(CargoPlane cargoPlane)
    {
        return $"{Name} -An interview with the crew of {cargoPlane.Serial}.";
    }
    public string Report(PassengerPlane passengerPlane)
    {
        return $"{Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}.";
    }
}