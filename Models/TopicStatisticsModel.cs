using System;

namespace GroupProject.Models;

public class TopicStatisticsModel
{
    public string TopicName { get; set; }
    public int NoCorrect { get; set; }
    public int NoWrong { get; set; }

    public TopicStatisticsModel(string topicName, int noCorrect, int noWrong)
    {
        TopicName = topicName;
        NoCorrect = noCorrect;
        NoWrong = noWrong;
        
        Console.WriteLine($"Topic: {topicName}, Correct: {noCorrect}, Wrong: {noWrong}");
    }
}