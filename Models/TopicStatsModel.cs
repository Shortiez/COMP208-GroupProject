namespace GroupProject.Models;

public class TopicStatsModel
{
    public string TopicName { get; set; }
    public int NoCorrect { get; set; }
    public int NoWrong { get; set; }

    public TopicStatsModel(string topicName, int noCorrect, int noWrong)
    {
        TopicName = topicName;
        NoCorrect = noCorrect;
        NoWrong = noWrong;
    }
}