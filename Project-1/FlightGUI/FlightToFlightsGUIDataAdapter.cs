using System.Globalization;
using System.Runtime.CompilerServices;
using Mapsui.Projections;

namespace Project1;

public interface IDataAdapter<T>
{
    T Adapter(List<Flight> flights, List<Airport> airports);
}

public class FlightDataAdapter : IDataAdapter<FlightsGUIData>
{
    /// <summary>
    /// Adapter to convert a list of flights and a list of airports to a FlightsGUIData object.
    /// </summary>
    /// <param name="flights">Flights list</param>
    /// <param name="airports">Airports list</param>
    /// <returns></returns>
    public FlightsGUIData Adapter(List<Flight> flights, List<Airport> airports)
    {
        List<FlightGUI> flightGUIDatas = new List<FlightGUI>();
        List<Flight> flightsCopy = new List<Flight>(flights);
        foreach (Flight flight in flightsCopy)
        {
            (double flightX, double flightY, double mapCoordRotation, double progress) =
                GetWorldPosition(flight, airports);
            if (progress >= 0 && progress <= 1)
            {
                flight.Longitude = Convert.ToSingle(flightX, CultureInfo.InvariantCulture);
                flight.Latitude = Convert.ToSingle(flightY, CultureInfo.InvariantCulture);
                FlightGUI flightGUIData = new FlightGUI
                {
                    ID = flight.Id,
                    WorldPosition = new WorldPosition(flight.Latitude, flight.Longitude),
                    MapCoordRotation = mapCoordRotation
                };
                flightGUIDatas.Add(flightGUIData);
            }
        }
        return new FlightsGUIData(flightGUIDatas);
    }

    /// <summary>
    /// This method calculates the world position of a flight based on the departure and arrival airports.
    /// </summary>
    /// <param name="flight">Flight</param>
    /// <param name="airports">Airports List</param>
    /// <returns></returns>
    private static (double, double, double, double) GetWorldPosition(
        Flight flight,
        List<Airport> airports
    )
    {
        Airport departure = airports.Find(airport => airport.Id == flight.OriginId)!;
        Airport arrival = airports.Find(airport => airport.Id == flight.TargetId)!;

        DateTime currentTime = DateTime.Now;
        double totalFlightTime,
            elapsedTime,
            remainingTime,
            progress;
        totalFlightTime = (flight.LandingTime - flight.TakeOffTime).TotalSeconds;
        elapsedTime = (currentTime - flight.TakeOffTime).TotalSeconds;
        remainingTime = (flight.LandingTime - currentTime).TotalSeconds;

        if (flight.LandingTime < flight.TakeOffTime)
        {
            totalFlightTime += 24 * 60 * 60;
            if (elapsedTime < 0)
            {
                elapsedTime += 24 * 60 * 60;
            }
            if (remainingTime < 0)
            {
                remainingTime += 24 * 60 * 60;
            }
        }
        progress = elapsedTime / totalFlightTime;
        double flightX,
            flightY;
        if (CheckFlight(flight))
        {
            flightX = WrapLongitude(
                flight.Longitude + (arrival.Longitude - flight.Longitude) / remainingTime
            );
            flightY = WrapLatitude(
                flight.Latitude + (arrival.Latitude - flight.Latitude) / remainingTime
            );
        }
        else
        {
            flightX = WrapLongitude(
                departure.Longitude + (arrival.Longitude - departure.Longitude) * progress
            );
            flightY = WrapLatitude(
                departure.Latitude + (arrival.Latitude - departure.Latitude) * progress
            );
        }
        double mapCoordRotation = CalculateRotation(flight, arrival);
        return (flightX, flightY, mapCoordRotation, progress);
    }

    /// <summary>
    /// This method calculates the rotation of the flight based on the departure and arrival airports.
    /// </summary>
    /// <param name="departure">Departure Airport</param>
    /// <param name="arrival">Arrival Airport</param>
    /// <returns></returns>
    private static double CalculateRotation(Flight current, Airport arrival)
    {
        (double originX, double originY) = SphericalMercator.FromLonLat(
            current.Longitude,
            current.Latitude
        )!;
        (double targetX, double targetY) = SphericalMercator.FromLonLat(
            arrival.Longitude,
            arrival.Latitude
        )!;
        double mapCoordRotation = Math.Atan2(targetX - originX, targetY - originY);
        if (mapCoordRotation < 0)
        {
            mapCoordRotation += 2 * Math.PI;
        }

        return mapCoordRotation;
    }

    /// <summary>
    /// This method wraps the longitude value to be between -180 and 180.
    /// </summary>
    /// <param name="longitude">Longitude value</param>
    /// <returns></returns>
    private static double WrapLongitude(double longitude)
    {
        if (longitude > 180)
            return longitude - 360;
        else if (longitude < -180)
            return longitude + 360;
        else
            return longitude;
    }

    /// <summary>
    /// This method wraps the latitude value to be between -90 and 90.
    /// </summary>
    /// <param name="latitude">Latitude value</param>
    /// <returns></returns>
    private static double WrapLatitude(double latitude)
    {
        if (latitude > 90)
            return 90 - (latitude - 90);
        else if (latitude < -90)
            return -90 + (-90 - latitude);
        else
            return latitude;
    }

    private static bool CheckFlight(Flight flight)
    {
        if (flight.Amsl == 0 && flight.Latitude == 0 && flight.Longitude == 0)
        {
            return false;
        }
        else
            return true;
    }
}
