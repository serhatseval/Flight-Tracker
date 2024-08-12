using NetworkSourceSimulator;

namespace Project1;

/// <summary>
/// Interface for the observer pattern.
/// </summary>
public interface IObserverImport
{
    void Update(FlightObjectLists flightObjectLists);
}

public interface IObserverDataUpdate
{
    void OnContactInfoUpdate(ContactInfoUpdateArgs e);
    void OnIDUpdate(IDUpdateArgs e);
    void OnPositionUpdate(PositionUpdateArgs e);
}
