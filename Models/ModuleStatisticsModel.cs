using System.Collections.ObjectModel;

namespace GroupProject.Models;

public class ModuleStatisticsModel
{
    public string ModuleName { get; set; }
    public ObservableCollection<TopicStatisticsModel> Topics { get; set; }

    public ModuleStatisticsModel(string moduleName)
    {
        ModuleName = moduleName;
        Topics = new ObservableCollection<TopicStatisticsModel>();
    }
}