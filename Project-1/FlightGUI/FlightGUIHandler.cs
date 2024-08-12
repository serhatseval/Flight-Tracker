using FlightTrackerGUI;

namespace Project1
{
    public class FlightGUIHandler : IObserverImport
    {
        private List<Airport> airports = new();
        private List<Flight> flights = new();
        FlightDataAdapter flightDataAdapter = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightGUIHandler"/> class. It attaches to observer.
        /// </summary>
        public FlightGUIHandler()
        {
            Import.Instance.Attach(this);
        }

        /// <summary>
        /// This method updates the flight object lists when subject notifies (Observer design).
        /// </summary>
        /// <param name="flightObjectLists">Flight Object Lists</param>
        public void Update(FlightObjectLists flightObjectLists)
        {
            airports = flightObjectLists.Airports;
            flights = flightObjectLists.Flights;
            UpdateData();
        }


        /// <summary>
        /// This method visualizes data it assigns one thread to visualize and update data.
        /// </summary>
        public void Visualize()
        {
            Thread GUIDataThread = new Thread(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 1000;
                timer.Elapsed += (sender, e) =>
                {
                    UpdateData();
                };
                timer.Start();
            });

            ThreadHandler.Instance.AddThread(GUIDataThread);
            GUIDataThread.Start();
            ThreadHandler.Instance.MakeThreadsBackground();
            Runner.Run();
        }

        /// <summary>
        /// This method updates data for visualization.
        /// </summary>
        private void UpdateData()
        {
            Runner.UpdateGUI(flightDataAdapter.Adapter(flights, airports));
        }
    }
}
