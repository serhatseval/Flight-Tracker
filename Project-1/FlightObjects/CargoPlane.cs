namespace Project1;

public class CargoPlane : FlightPlane, IReportable
{
    public string Type { get; set; } = "CP";
    public Single MaxLoad { get; set; }

    public void Reporting(IMedia media)
    {
        Console.WriteLine(media.Report(this));
    }
}