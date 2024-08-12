namespace Project1;

public interface IMedia
{
    string Name { get; set; }
    string Report(Airport airport);
    string Report(CargoPlane cargoPlane);
    string Report(PassengerPlane passengerPlane);
}

