using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Base;
using TimeKeeper.Models;

namespace TimeKeeper.VMs
{
    public class DayActivityViewModel : BaseViewModel
    {
        public DayActivityViewModel(IEnumerable<JiraTask> tasks)
        {
            Title = "Day Activity";
            Hours = new ObservableCollection<HourSlotModel>();
            Tasks = new ObservableCollection<JiraTask>(tasks);

            for (int i = 0; i < 24; i++)
            {
                Hours.Add(new HourSlotModel() { HourText = $"{i + 1}", Hour = i + 1 });
            }

            foreach (var t in Tasks)
            {
                var timeSpans = t.TimeSpans.Where(ts => ts.StartDate.Date == DateTime.Now.Date);
                foreach (var ts in timeSpans)
                {
                    var foundHour = Hours.Single(h => h.Hour == ts.StartDate.Hour);
                    foundHour.Entries.Add(new HourContentModel { Title = ts.TimeSpanType.ToString() });
                }
            }
        }

        public ObservableCollection<HourSlotModel> Hours { get; set; }
        public ObservableCollection<JiraTask> Tasks { get; set; }
    }
}
