namespace Project1;

public class NewsGenerator
{
    private List<IMedia> media = new List<IMedia>();
    private List<IReportable> reportables = new List<IReportable>();
    private int mediaIndex = 0;
    private int reportableIndex = 0;

    public NewsGenerator(List<IMedia> media, List<IReportable> reportables)
    {
        this.media = media;
        this.reportables = reportables;
    }

    public void GenerateNextNews()
    {
        reportables[reportableIndex].Reporting(media[mediaIndex]);
        reportableIndex++;
        mediaIndex++;
        if(reportableIndex >= reportables.Count)
        {
            reportableIndex = 0;
        }
        if(mediaIndex >= media.Count)
        {
            mediaIndex = 0;
        }
    }
}