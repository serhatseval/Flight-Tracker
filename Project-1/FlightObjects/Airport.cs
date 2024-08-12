namespace Project1;

public class Airport : FlightGeo, IReportable
{
    public string Type { get; set; } = "AI";
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CountryIso { get; set; } = string.Empty;

    public void Reporting(IMedia media)
    {
        Console.WriteLine(media.Report(this));
    }
}