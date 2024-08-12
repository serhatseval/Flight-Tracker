using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using NetworkSourceSimulator;

namespace Project1;

public class UpdateData : IObserverDataUpdate
{
    private List<IObserverImport> observers = new List<IObserverImport>();
    private List<FlightObject> flightObjects = new();
    private List<FlightPeople> flightPeoples = new();
    private List<FlightGeo> flightLocations = new();

    public UpdateData()
    {
        flightObjects = Import.Instance.passengers.ToList<FlightObject>();
        flightObjects.AddRange(Import.Instance.cargos);
        flightObjects.AddRange(Import.Instance.flights);
        flightObjects.AddRange(Import.Instance.airports);
        flightObjects.AddRange(Import.Instance.cargoPlanes);
        flightObjects.AddRange(Import.Instance.crews);
        flightObjects.AddRange(Import.Instance.passengerPlanes);

        flightPeoples = Import.Instance.crews.ToList<FlightPeople>();
        flightPeoples.AddRange(Import.Instance.passengers);

        flightLocations = Import.Instance.airports.ToList<FlightGeo>();
        flightLocations.AddRange(Import.Instance.flights);
    }

    public void Attach(IObserverImport observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserverImport observer)
    {
        observers.Remove(observer);
    }

    public void Notify(FlightObjectLists flightObjectLists)
    {
        foreach (IObserverImport observer in observers)
        {
            observer.Update(flightObjectLists);
        }
    }

    public void OnContactInfoUpdate(ContactInfoUpdateArgs e)
    {
        var contact = flightPeoples.Find(p => p.Id == e.ObjectID);
        if (contact != null)
        {
            Log.WriteLog(
                $"Contact info update: {e.ObjectID}, Old: {contact.Phone}, {contact.Email}, New: {e.PhoneNumber}, {e.EmailAddress}"
            );
            contact.Phone = e.PhoneNumber;
            contact.Email = e.EmailAddress;
        }
        else
        {
            Log.WriteLog($"Contact info update failed: {e.ObjectID}");
        }
    }

    public void OnIDUpdate(IDUpdateArgs e)
    {
        var id = flightObjects.Find(a => a.Id == e.ObjectID);
        if (id != null)
        {
            id.Id = e.NewObjectID;
            Log.WriteLog($"ID update: {e.ObjectID}, {e.NewObjectID}");
        }
        else
        {
            Log.WriteLog($"ID update failed: {e.ObjectID}");
        }
    }

    public void OnPositionUpdate(PositionUpdateArgs e)
    {
        var position = flightLocations.Find(a => a.Id == e.ObjectID);
        if (position != null)
        {
            Log.WriteLog(
                $"Position update: {e.ObjectID}, Old:{position.Latitude}, {position.Longitude}, {position.Amsl} New: {e.Latitude}, {e.Longitude}, {e.AMSL}"
            );
            position.Latitude = e.Latitude;
            position.Longitude = e.Longitude;
            position.Amsl = e.AMSL;
            Notify(Import.Instance.ReturnImport());
        }
        else
        {
            Log.WriteLog($"Position update failed: {e.ObjectID}");
        }
    }
}

public static class Log
{
    public static void WriteLog(string message)
    {
        DirectoryInfo? dirInfo = GetProjectRoot.GetProjectRootDirectory();
        string? projectRoot = dirInfo?.FullName;
        string? dataPath = projectRoot != null ? Path.Combine(projectRoot, "data", "in") : null;

        using (
            StreamWriter writer = new StreamWriter(
                GetPath(DateTime.Now.ToString("dd_MM_yyyy")),
                true
            )
        )
        {
            writer.WriteLine($"{DateTime.Now.ToString("HH:mm")} - {message}");
        }
    }

    private static string GetPath(string fileName)
    {
        DirectoryInfo? dirInfo = GetProjectRoot.GetProjectRootDirectory();
        string? projectRoot = dirInfo?.FullName;
        string? dataPath = projectRoot != null ? Path.Combine(projectRoot, "data", "out") : null;

        if (dataPath != null)
        {
            return Path.Combine(dataPath, $"{fileName}.log");
        }
        else
        {
            throw new Exception("Data path is null.");
        }
    }
}
