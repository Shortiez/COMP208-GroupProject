using System.Collections.ObjectModel;

namespace GroupProject.Models;

public class ModuleTopicStatsModel
{
    public string ModuleName { get; set; }
    public ObservableCollection<TopicStatsModel> Topics { get; set; }

    public ModuleTopicStatsModel(string moduleName)
    {
        ModuleName = moduleName;
        Topics = new ObservableCollection<TopicStatsModel>();
    }
}