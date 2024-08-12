using System.Globalization;

namespace Project1;

public class Factory
{
    /// <summary>
    /// This dictionary designed to be forward data got from files to factories,
    /// allowing to being used for various input types. To serialize with grouped
    /// types it lets you to add objects to seperate respective lists
    /// </summary>
    /// <param name="airports">Airport List</param>
    /// <param name="cargos">Cargo List</param>
    /// <param name="cargoPlanes">Cargo Plane List</param>
    /// <param name="crews">Crew List</param>
    /// <param name="passengers">Passenger List</param>
    /// <param name="passengerPlanes">Passenger Plane List</param>
    /// <param name="flights">Flight List</param>
    /// <returns></returns>
    public static Dictionary<string, Action<string[]>> CreateFileToFactories(
        List<Airport> airports, List<Cargo> cargos, List<CargoPlane> cargoPlanes,
        List<Crew> crews, List<Passenger> passengers,
        List<PassengerPlane> passengerPlanes, List<Flight> flights)
    {
        return new Dictionary<string, Action<string[]>>
        {
            { "AI", values => airports.Add(CreateAirport(values))},
            { "CA", values => cargos.Add(CreateCargo(values)) },
            { "CP", values => cargoPlanes.Add(CreateCargoPlane(values)) },
            { "C", values => crews.Add(CreateCrew(values)) },
            { "P", values => passengers.Add(CreatePassenger(values)) },
            { "PP", values => passengerPlanes.Add(CreatePassengerPlane(values)) },
            { "FL", values => flights.Add(CreateFlight(values)) },
        };
    }

    /// <summary>
    /// Factory to create Airport object
    /// </summary>
    /// <param name="values">String Array Values for Airport object</param>
    /// <returns></returns>
    private static Airport CreateAirport(string[] values)
    {
        var airport = new Airport
        {
            Id = UInt64.Parse(values[1]),
            Name = values[2],
            Code = values[3],
            Longitude = Single.Parse(values[4], CultureInfo.InvariantCulture),
            Latitude = Single.Parse(values[5], CultureInfo.InvariantCulture),
            Amsl = float.Parse(values[6], CultureInfo.InvariantCulture),
            CountryIso = values[7]
        };
        return airport;
    }

    /// <summary>
    /// Factory to create Cargo object
    /// </summary>
    /// <param name="values">String Array Values for Cargo Object</param>
    /// <returns></returns>
    private static Cargo CreateCargo(string[] values)
    {
        var cargo = new Cargo
        {
            Id = UInt64.Parse(values[1]),
            Weight = Single.Parse(values[2],
                CultureInfo.InvariantCulture),
            Code = values[3],
            Description = values[4]
        };
        return cargo;
    }

    /// <summary>
    /// Factory to create Cargo Plane object
    /// </summary>
    /// <param name="values">String Array Values for Cargo Plane Object</param>
    /// <returns></returns>
    private static CargoPlane CreateCargoPlane(string[] values)
    {
        var cargoPlane =
            new CargoPlane
            {
                Serial = values[2],
                Id = UInt64.Parse(values[1]),
                CountryIso = values[3],
                Model = values[4],
                MaxLoad = Single.Parse(values[5],
                    CultureInfo.InvariantCulture)
            };
        return cargoPlane;
    }

    /// <summary>
    /// Factory to create Crew object
    /// </summary>
    /// <param name="values">String Array Values for Crew Object</param>
    /// <returns></returns>
    private static Crew CreateCrew(string[] values)
    {
        var crew = new Crew
        {
            Name = values[2],
            Id = UInt64.Parse(values[1]),
            Age = UInt64.Parse(values[3]),
            Phone = values[4],
            Email = values[5],
            Practice = UInt16.Parse(values[6]),
            Role = values[7]
        };
        return crew;
    }

    /// <summary>
    /// Factory to create Passenger object
    /// </summary>
    /// <param name="values">String Array Values for Passenger Object</param>
    /// <returns></returns>
    private static Passenger CreatePassenger(string[] values)
    {
        var passenger = new Passenger
        {
            Name = values[2],
            Id = UInt64.Parse(values[1]),
            Age = UInt64.Parse(values[3]),
            Phone = values[4],
            Email = values[5],
            Class = values[6],
            Miles = UInt64.Parse(values[7])
        };
        return passenger;
    }

    /// <summary>
    /// Factory to create Passenger Plane Object
    /// </summary>
    /// <param name="values">String Array Values for Passenger Plane
    /// object</param> <returns></returns>
    private static PassengerPlane CreatePassengerPlane(string[] values)
    {
        var passengerPlane =
            new PassengerPlane
            {
                Serial = values[2],
                Id = UInt64.Parse(values[1]),
                CountryIso = values[3],
                Model = values[4],
                FirstClassSize = UInt16.Parse(values[5]),
                BusinessClassSize = UInt16.Parse(values[6]),
                EconomyClassSize = UInt16.Parse(values[7])
            };
        return passengerPlane;
    }

    /// <summary>
    /// Factory to create Flight Object
    /// </summary>
    /// <param name="values">String Array Values for Flight Object</param>
    /// <returns></returns>
    private static Flight CreateFlight(string[] values)
    {
        var flight = new Flight
        {
            Id = UInt64.Parse(values[1]),
            OriginId = UInt64.Parse(values[2]),
            TargetId = UInt64.Parse(values[3]),
            TakeOffTime = DateTime.ParseExact(values[4], "HH:mm", CultureInfo.InvariantCulture),
            LandingTime = DateTime.ParseExact(values[5], "HH:mm", CultureInfo.InvariantCulture),
            Longitude = Single.Parse(values[6], CultureInfo.InvariantCulture),
            Latitude = Single.Parse(values[7], CultureInfo.InvariantCulture),
            Amsl = float.Parse(values[8], CultureInfo.InvariantCulture),
            PlaneId = UInt64.Parse(values[9]),
            CrewId = Import.GetArray(values[10]),
            LoadId = Import.GetArray(values[11])
        };
        return flight;
    }
}