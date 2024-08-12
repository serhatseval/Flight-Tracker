namespace Project1;

/// <summary>
/// Entry point my program lets grader to run which week they want to run.
/// </summary>
internal static class Program
{
    static void Main()
    {
        bool debug = false;
        if (!debug)
        {
            Console.WriteLine(
                "Welcome to Flight Tracker System! \nChoose which week you want to run\n1-Project 1\n2-Project 2\n3-Project 3\n4-Project 4\n5-Project 5"
            );
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
            {
                Console.Write("Invalid input. Please enter a number: ");
            }

            switch (choice)
            {
                case 1:
                    ProjectWeek1();
                    break;
                case 2:
                    ProjectWeek2();
                    break;
                case 3:
                    ProjectWeek3();
                    break;
                case 4:
                    ProjectWeek4();
                    break;
                case 5:
                    ProjectWeek5();
                    break;
            }
        }
        else
        {
            ProjectWeek5();
        }
    }

    private static void ProjectWeek1()
    {
        string filePath = Import.Instance.SetFilePath("*.ftr");
        FlightObjectLists data = Import.Instance.ImportFtrData();
        Export export = new Export();
        export.JsonExport();
    }

    private static void ProjectWeek2()
    {
        string filePath = Import.Instance.SetFilePath("*.ftr");
        int minDelay,
            maxDelay;
        Console.WriteLine("For the simulator give the minimum delay time:");
        int.TryParse(Console.ReadLine(), out minDelay);
        Console.WriteLine("For the simulator give the maximum delay time:");
        int.TryParse(Console.ReadLine(), out maxDelay);
        TerminalListener terminalListener = new TerminalListener();
        TCPSimulator simulator = new TCPSimulator(filePath, minDelay, maxDelay);
        simulator.Start();
    }

    private static void ProjectWeek3()
    {
        Console.WriteLine(
            "Which import method you want to try for the GUI?\n1- FTR file\n2- TCP Simulator"
        );
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
        {
            Console.Write("Invalid input. Please enter a number: ");
        }

        if (choice == 1)
        {
            FlightGUIHandler flightGUIHandler = new FlightGUIHandler();
            Import.Instance.SetFilePath("*.ftr");
            Import.Instance.ImportFtrData();
            flightGUIHandler.Visualize();
        }
        else
        {
            string filePath = Import.Instance.SetFilePath("*.ftr");
            int minDelay,
                maxDelay;
            Console.WriteLine("For the simulator give the minimum delay time:");
            int.TryParse(Console.ReadLine(), out minDelay);
            Console.WriteLine("For the simulator give the maximum delay time:");
            int.TryParse(Console.ReadLine(), out maxDelay);
            FlightGUIHandler flightGUIHandler = new FlightGUIHandler();

            TCPSimulator simulator = new TCPSimulator(filePath, minDelay, maxDelay);
            simulator.Start();

            flightGUIHandler.Visualize();
        }
    }

    private static void ProjectWeek4()
    {
        FlightGUIHandler flightGUIHandler = new FlightGUIHandler();
        Import.Instance.SetFilePath("*.ftr");
        TerminalListener terminalListener = new TerminalListener();
        Import.Instance.ImportFtrData();
        terminalListener.Start();
        flightGUIHandler.Visualize();
    }

    private static void ProjectWeek5()
    {
        int minDelay,
            maxDelay;
        FlightGUIHandler flightGUIHandler = new FlightGUIHandler();
        Import.Instance.SetFilePath("*.ftr");
        TerminalListener terminalListener = new TerminalListener();
        Import.Instance.ImportFtrData();
        string filePath = Import.Instance.SetFilePath("*.ftre");
        Console.WriteLine("For the simulator give the minimum delay time:");
        int.TryParse(Console.ReadLine(), out minDelay);
        Console.WriteLine("For the simulator give the maximum delay time:");
        int.TryParse(Console.ReadLine(), out maxDelay);
        UpdateData updateData = new UpdateData();
        updateData.Attach(flightGUIHandler);
        TCPSimulator simulator = new TCPSimulator(filePath, minDelay, maxDelay);
        simulator.Attach(updateData);
        simulator.Start();
        terminalListener.Start();
        flightGUIHandler.Visualize();
    }
}
