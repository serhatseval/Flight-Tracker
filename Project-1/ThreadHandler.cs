/// <summary>
/// This class is a singleton that handles all the threads in the program. It is
/// used to interrupt all threads before the program is closed.
/// </summary>
public sealed class ThreadHandler
{
    private static ThreadHandler? instance = null;
    private static Mutex mutex = new Mutex();
    private List<Thread> threads;

    ThreadHandler()
    {
        threads = new List<Thread>();
    }
    
    /// <summary>
    /// Property to get the instance of the ThreadHandler. This is used to make sure that only one instance of the ThreadHandler is created.
    /// </summary>
    public static ThreadHandler Instance
    {
        get
        {
            mutex.WaitOne();
            if (instance == null)
            {
                instance = new ThreadHandler();
            }

            mutex.ReleaseMutex();
            return instance;
        }
    }
    /// <summary>
    /// Function to add a thread to the list of threads. This is used to keep track of all threads in the program.
    /// </summary>
    /// <param name="thread">Thread</param>
    public void AddThread(Thread thread)
    {
        threads.Add(thread);
    }

    /// <summary>
    /// Function to interrupt all threads before the program is closed. This is used to make sure that all threads are closed before the program exits.
    /// </summary>
    public void InterruptAllThreads()
    {
        try
        {
            foreach (var thread in threads)
            {
                thread.Interrupt();
            }
        }
        catch (ThreadInterruptedException)
        {
            Console.WriteLine("Thread interrupted");
        }

        Environment.Exit(0);
    }
    /// <summary>
    /// Function to make all threads background threads. This is used to make sure that the program exits when the main thread exits.
    /// </summary>
    public void MakeThreadsBackground()
    {
        foreach (var thread in threads)
        {
            thread.IsBackground = true;
        }
    }
}