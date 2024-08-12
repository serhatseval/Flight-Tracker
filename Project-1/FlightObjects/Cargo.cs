namespace Project1;

public class Cargo : FlightObject
{
    public string Type { get; set; } = "CA";
    public Single Weight { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}