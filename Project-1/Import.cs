namespace Project1;

public class Import
{
    private static Import? instance = null;
    private static readonly object padlock = new object();

    public static Import Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Import();
                }

                return instance;
            }
        }
    }

    private string filePath = string.Empty;
    private static FlightObjectLists allData = new FlightObjectLists();
    public List<Airport> airports = allData.Airports;
    public List<Cargo> cargos = allData.Cargos;
    public List<CargoPlane> cargoPlanes = allData.CargoPlanes;
    public List<Crew> crews = allData.Crews;
    public List<Passenger> passengers = allData.Passengers;
    public List<PassengerPlane> passengerPlanes = allData.PassengerPlanes;
    public List<Flight> flights = allData.Flights;
    public List<FlightObject> flightObjects = new();
    public List<FlightPeople> flightPeoples = new();
    private List<IObserverImport> observers = new List<IObserverImport>();

    /// <summary>
    /// This method sets file path for FTR file type. It checks folder for files
    /// with ".ftr" extension. If more than one file eligible for import it asks
    /// user to choose one.
    /// </summary>
    /// <exception cref="FileNotFoundException">No file has been found in the
    /// input folder. Check data/in.</exception>
    public string SetFilePath(string fileType)
    {
        DirectoryInfo? dirInfo = GetProjectRoot.GetProjectRootDirectory();
        string? projectRoot = dirInfo?.FullName;
        string? dataPath = projectRoot != null ? Path.Combine(projectRoot, "data", "in") : null;
        string[] files =
            dataPath != null ? Directory.GetFiles(dataPath, fileType) : Array.Empty<string>();
        if (files.Length == 0)
        {
            throw new FileNotFoundException("No files found");
        }
        else if (files.Length == 1)
        {
            filePath = files[0];
        }
        else
        {
            Console.WriteLine("Choose a file to import:");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {files[i]}");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.Write("Invalid input. Please enter a number: ");
            }

            filePath = files[choice - 1];
        }

        return filePath;
    }

    /// <summary>
    /// Class to create an Uint64 Array from a string input.
    /// </summary>
    /// <param name="data">String input</param>
    /// <returns></returns>
    public static UInt64[] GetArray(string data)
    {
        data = data.Trim('[', ']');
        string[] input = data.Split(';');
        UInt64[] output = new UInt64[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            if (UInt64.TryParse(input[i], out var value))
            {
                output[i] = value;
            }
        }

        return output;
    }

    /// <summary>
    /// This function imports data from FTR file using the dictionary
    /// CreateFileToFactories. It is using Factory design. It is returning
    /// FlightObjectLists for serialization.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Unexpected flight object type
    /// in the input</exception>
    public FlightObjectLists ImportFtrData()
    {
        var fileToFactories = Factory.CreateFileToFactories(
            airports,
            cargos,
            cargoPlanes,
            crews,
            passengers,
            passengerPlanes,
            flights
        );

        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                string type = values[0];

                if (fileToFactories.TryGetValue(type, out var fileToFactory))
                {
                    fileToFactory(values);
                }
                else
                {
                    throw new InvalidOperationException("Unexpected flight object type");
                }
            }
        }

        Notify(
            allData.AllDataHandler(
                airports,
                cargos,
                cargoPlanes,
                crews,
                passengers,
                passengerPlanes,
                flights
            )
        );
        return allData.AllDataHandler(
            airports,
            cargos,
            cargoPlanes,
            crews,
            passengers,
            passengerPlanes,
            flights
        );
    }

    /// <summary>
    /// This function processes the message from the TCP simulator and adds the
    /// data to the lists.
    /// </summary>
    /// <param name="message">Byte messages</param>
    /// <exception cref="InvalidOperationException">Unexpected flight object type
    /// came from the TCP</exception>
    public void ProcessMessage(byte[] message)
    {
        using (var stream = new MemoryStream(message))
        using (var reader = new BinaryReader(stream))
        {
            string Type = new string(reader.ReadChars(3));
            if (MessagetoStringClass.MessagetoString.TryGetValue(Type, out var action))
            {
                string[] afterParsed = action(message);
                var factory = new Factory();
                var fileToFactories = Factory.CreateFileToFactories(
                    airports,
                    cargos,
                    cargoPlanes,
                    crews,
                    passengers,
                    passengerPlanes,
                    flights
                );
                if (fileToFactories.TryGetValue(afterParsed[0], out var fileToFactory))
                {
                    fileToFactory(afterParsed);
                    Notify(
                        allData.AllDataHandler(
                            airports,
                            cargos,
                            cargoPlanes,
                            crews,
                            passengers,
                            passengerPlanes,
                            flights
                        )
                    );
                }
                else
                {
                    throw new InvalidOperationException("Unexpected flight object type");
                }
            }
            else
            {
                throw new InvalidOperationException("Unexpected flight object type");
            }
        }
    }

    public FlightObjectLists ReturnImport()
    {
        return allData.AllDataHandler(
            airports,
            cargos,
            cargoPlanes,
            crews,
            passengers,
            passengerPlanes,
            flights
        );
    }

    public void Attach(IObserverImport observer)
    {
        observers.Add(observer);
    }

    public void Notify(FlightObjectLists flightObjectLists)
    {
        foreach (IObserverImport observer in observers)
        {
            observer.Update(flightObjectLists);
        }
    }
}