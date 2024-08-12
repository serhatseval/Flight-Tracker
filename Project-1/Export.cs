using System.Text.Json;

namespace Project1;

public class Export
{
    /// <summary>
    /// This function serialise to JSON.
    /// </summary>
    /// <exception cref="Exception">Export path is not there. Check
    /// files.</exception>
    public void JsonExport()
    {
        FlightObjectLists flightObjectLists = Import.Instance.ReturnImport();
        DirectoryInfo? dirInfo = GetProjectRoot.GetProjectRootDirectory();
        string? exportPath = dirInfo?.FullName;
        exportPath = Path.Combine(
            exportPath ?? throw new Exception("Export path is null"),
            "data",
            "out"
        );
        string filePath = Path.Combine(exportPath, $"snapshot_{DateTime.Now:HH_mm_ss}.json");
        string json = JsonSerializer.Serialize(
            flightObjectLists,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// This function prompts the user for a file name and checks if the file
    /// already exists.
    /// </summary>
    /// <param name="exportPath">Directory file will be exported</param>
    /// <returns></returns>
    /// <exception cref="Exception">No file name has been put by the
    /// user</exception>
    private string UserPrompt(string exportPath)
    {
        Console.Write("Enter a name for the export: ");
        string name = Console.ReadLine() ?? throw new Exception("No file name has been given");
        string filePath = exportPath + '/' + name + ".json";
        if (File.Exists(filePath))
        {
            Console.WriteLine("File already exists. Do you want to overwrite it? ('Y'es / 'N'o)");
            string? response = Console.ReadLine();
            if (response?.ToLower() == "no" || response?.ToLower() == "n")
            {
                Console.WriteLine(
                    "File not overwritten, export cancelled. Press any key to continue"
                );
                Console.Read();
                Environment.Exit(0);
            }

            Console.WriteLine("File will be overwritten. Press any key to continue");
            Console.Read();
        }

        return filePath;
    }
}