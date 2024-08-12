namespace Project1;

/// <summary>
/// This class is created to hold the lists to serialize objects grouped.
/// </summary>
public class FlightObjectLists
{
    public List<Airport> Airports { get; set; } = new List<Airport>();
    public List<Cargo> Cargos { get; set; } = new List<Cargo>();
    public List<CargoPlane> CargoPlanes { get; set; } = new List<CargoPlane>();
    public List<Crew> Crews { get; set; } = new List<Crew>();
    public List<Passenger> Passengers { get; set; } = new List<Passenger>();
    public List<PassengerPlane> PassengerPlanes { get; set; } = new List<PassengerPlane>();
    public List<Flight> Flights { get; set; } = new List<Flight>();

    public FlightObjectLists AllDataHandler(List<Airport> airports, List<Cargo> cargos, List<CargoPlane> cargoPlanes,
        List<Crew> crews, List<Passenger> passengers, List<PassengerPlane> passengerPlanes, List<Flight> flights)
    {
        FlightObjectLists allData = new FlightObjectLists
        {
            Airports = airports,
            Cargos = cargos,
            CargoPlanes = cargoPlanes,
            Crews = crews,
            Passengers = passengers,
            PassengerPlanes = passengerPlanes,
            Flights = flights
        };
        return allData;
    }
}