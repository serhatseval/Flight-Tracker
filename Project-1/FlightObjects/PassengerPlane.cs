namespace Project1;

public class PassengerPlane : FlightPlane, IReportable
{
    public string Type { get; set; } = "PP";
    public UInt16 FirstClassSize { get; set; }
    public UInt16 BusinessClassSize { get; set; }
    public UInt16 EconomyClassSize { get; set; }

    public void Reporting(IMedia media)
    {
        Console.WriteLine(media.Report(this));
    }
}