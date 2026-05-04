using System.Collections.ObjectModel;

namespace TimeKeeper.Models
{
    public interface IViewableJiraTask
    {
        bool Archived { get; set; }
        bool Busy { get; set; }
        DateTime? End { get; set; }
        bool IsActive { get; set; }
        DateTime? Start { get; set; }
        ObservableCollection<TimeKeeperTimeSpan> TimeSpans { get; set; }
        string Title { get; set; }
        TimeSpan TotalTime { get; set; }
    }
}