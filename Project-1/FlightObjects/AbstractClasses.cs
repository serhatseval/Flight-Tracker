namespace Project1;

public abstract class FlightObject
{
    public UInt64 Id { get; set; }
}

public abstract class FlightPeople: FlightObject
{
    public string Name { get; set; } = string.Empty;
    public UInt64 Age { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public abstract class FlightPlane : FlightObject
{
    public string Serial { get; set; } = string.Empty;
    public string CountryIso { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
}

public abstract class FlightGeo : FlightObject
{
    public Single Longitude { get; set; }
    public Single Latitude { get; set; }
    public float Amsl { get; set; }

}