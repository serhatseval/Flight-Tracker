namespace Project1;

public class Passenger : FlightPeople
{
    public string Type { get; set; } = "P";
    public string Class { get; set; } = string.Empty;
    public UInt64 Miles { get; set; }
}