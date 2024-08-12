namespace Project1;

public class GetProjectRoot
{
    /// <summary>
    /// This function returns relative address of Project-1 folder. It is designed
    /// to let to work in different OSes.
    /// </summary>
    /// <returns></returns>
    public static DirectoryInfo? GetProjectRootDirectory()
    {
        string exePath = AppDomain.CurrentDomain.BaseDirectory;

        DirectoryInfo? dirInfo = new DirectoryInfo(exePath);

        while (dirInfo != null && dirInfo.Name != "bin")
        {
            dirInfo = dirInfo.Parent;
        }

        return dirInfo?.Parent;
    }
}