using Avalonia.Rendering;

namespace Project1;

class TerminalListener : IObserverImport
{
    private static Export export = new Export();
    private static NewsGenerator? newsGenerator;
    private List<IMedia> newsReporters = new List<IMedia>();

    public static List<IMedia> CreateNewsProviders()
    {
        var newsProviders = new List<IMedia>
        {
            new Television { Name = "Abelian Television" },
            new Television { Name = "Channel TV - Tensor" },
            new Radio { Name = "Quantifier Radio" },
            new Radio { Name = "Shmem Radio" },
            new Newspaper { Name = "Categories Journal" },
            new Newspaper { Name = "Polytechnical Gazette" }
        };

        return newsProviders;
    }

    public TerminalListener()
    {
        terminalReader = new Thread(new ThreadStart(TerminalLoop));
        ThreadHandler.Instance.AddThread(terminalReader);
        Import.Instance.Attach(this);
        newsReporters = CreateNewsProviders();
    }

    public void Start()
    {
        terminalReader.Start();
    }

    private Thread terminalReader;

    /// <summary>
    /// This method is a loop that listens to the terminal and executes commands.
    /// </summary>
    private void TerminalLoop()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (input == null)
            {
                continue;
            }
            else
            {
                if (TerminalCommands.TryGetValue(input.ToLower(), out var action))
                {
                    action();
                }
                else
                {
                    Console.WriteLine("Command not found. Type 'help' for a list of commands.");
                }
            }
        }
    }

    public void Update(FlightObjectLists flightObjectLists)
    {
        List<IReportable> reportables = new List<IReportable>();
        reportables.AddRange(flightObjectLists.Airports);
        reportables.AddRange(flightObjectLists.PassengerPlanes);
        reportables.AddRange(flightObjectLists.CargoPlanes);
        newsGenerator = new NewsGenerator(newsReporters, reportables);
    }

    /// <summary>
    /// Dictionary to handle Terminal Commands, allowing in future adding more commands without handling switches.
    /// </summary>
    private static Dictionary<string, Action> TerminalCommands = new Dictionary<string, Action>()
    {
        {
            "print",
            () =>
            {
                Console.WriteLine("Printing");
                export.JsonExport();
            }
        },
        {
            "exit",
            () =>
            {
                ThreadHandler.Instance.InterruptAllThreads();
            }
        },
        {
            "help",
            () => Console.WriteLine("Commands: \nprint - prints the data\nexit - exits the program \nreport - reports one news\ndebugger - reports in a loop.")
        },
        {
            "report",
            () =>
            {
                if (newsGenerator != null)
                {
                    newsGenerator.GenerateNextNews();
                }
            }
        },
        {
            "debugger",
            () =>
            {
                while (true)
                {
                    Console.WriteLine("Debugger is running Ctrl+C to exit.");
                    if (newsGenerator != null)
                    {
                        newsGenerator.GenerateNextNews();
                    }
                    Thread.Sleep(100);
                }
            }
        }
    };
}
