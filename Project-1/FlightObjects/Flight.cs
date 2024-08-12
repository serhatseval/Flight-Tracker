namespace Project1;

public class Flight : FlightGeo
{
    public string Type { get; set; } = "FL";
    public UInt64 OriginId { get; set; }
    public UInt64 TargetId { get; set; }
    public DateTime TakeOffTime { get; set; }
    public DateTime LandingTime { get; set; }
    public UInt64 PlaneId { get; set; }
    public UInt64[] CrewId { get; set; } = new UInt64[0];
    public UInt64[] LoadId { get; set; } = new UInt64[0];
}