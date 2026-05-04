using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Base;
using TimeKeeper.Enums;
using TimeKeeper.Models;

namespace TimeKeeper.VMs
{
    public class JiraTaskDetailsViewModel : BaseViewModel
    {
        private JiraTask task;
        private TimeSpan totalWorkTime;

        public JiraTaskDetailsViewModel(JiraTask task)
        {
            Title = task.Title;
            Task = task;

            RecalculateTimeCommand = new DelegateCommand(Recalculate);
            SelectTasksByTypeCommand = new DelegateCommand<ETimeSpanType?>(SelectTasksByType);

            foreach (var t in task.TimeSpans.Where(t => t.StartDate.Month == DateTime.Now.Month && t.StartDate.DayOfYear == DateTime.Now.DayOfYear && t.TimeSpanType == Enums.ETimeSpanType.Work))
            {
                TotalWorkTime += t.Time;
            }
            //SelectTasksByTypeCommand needs to use check boxes and we need properties to bind to
            //we need something to collapse expand all expanders
        }

        private void Recalculate()
        {
            TotalWorkTime = TimeSpan.Zero;
            foreach (var t in task.TimeSpans
                .Where(t => t.IsSelected))
            {
                TotalWorkTime += t.Time;
            }
        }

        public JiraTask Task
        {
            get
            {
                return task;
            }
            set
            {
                task = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan TotalWorkTime
        {
            get
            {
                return totalWorkTime;
            }
            set
            {
                totalWorkTime = value;
                OnPropertyChanged();
            }
        }

        public IDelegateCommand RecalculateTimeCommand { get; set; }

        public IDelegateCommand SelectTasksByTypeCommand { get; set; }

        private void SelectTasksByType(ETimeSpanType? type)
        {
            DeselectAll();
            if (type == null)
            {
                foreach (var t in Task.TimeSpans)
                {
                    t.IsSelected = true;
                }
                return;
            }

            foreach (var t in Task.TimeSpans.Where(ts => ts.TimeSpanType == type))
            {
                t.IsSelected = true;
            }
        }

        private void DeselectAll()
        {
            foreach (var t in Task.TimeSpans)
            {
                t.IsSelected = false;
            }
        }
    }
}
