using NetworkSourceSimulator;

namespace Project1;

public class TCPSimulator
{
    private NetworkSourceSimulator.NetworkSourceSimulator simulator;
    private Thread simulatorThread;

    private List<IObserverDataUpdate> observers = new List<IObserverDataUpdate>();

    public void Attach(IObserverDataUpdate observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserverDataUpdate observer)
    {
        observers.Remove(observer);
    }

    public TCPSimulator(string ftrFilePath, int minDelay, int maxDelay)
    {
        simulator = new NetworkSourceSimulator.NetworkSourceSimulator(
            ftrFilePath,
            minDelay,
            maxDelay
        );
        simulator.OnNewDataReady += Simulator_OnNewDataReady;
        simulator.OnContactInfoUpdate += Simulator_OnContactInfoUpdate;
        simulator.OnIDUpdate += Simulator_OnIDUpdate;
        simulator.OnPositionUpdate += Simulator_OnPositionUpdate;

        simulatorThread = new Thread(new ThreadStart(simulator.Run));
        ThreadHandler.Instance.AddThread(simulatorThread);
    }

    private void Simulator_OnNewDataReady(object sender, NewDataReadyArgs e)
    {
        var message = simulator.GetMessageAt(e.MessageIndex);
        Import.Instance.ProcessMessage(message.MessageBytes);
    }

    private void Simulator_OnContactInfoUpdate(object sender, ContactInfoUpdateArgs e)
    {
        foreach(IObserverDataUpdate observer in observers){
            observer.OnContactInfoUpdate(e);
        } 
    }

    private void Simulator_OnIDUpdate(object sender, IDUpdateArgs e)
    {
        foreach(IObserverDataUpdate observer in observers){
            observer.OnIDUpdate(e);
        } 
    }

    private void Simulator_OnPositionUpdate(object sender, PositionUpdateArgs e)
    {
        foreach(IObserverDataUpdate observer in observers){
            observer.OnPositionUpdate(e);
        } 
    }

    public void Start()
    {
        simulatorThread.Name = "TCP Simulator";
        simulatorThread.Start();
    }
}

