using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using TimeKeeper.Base;
using TimeKeeper.Enums;

namespace TimeKeeper.Models
{
    public class JiraTask : Observable, ISerialisable, IBusy, IArchivable, IViewableJiraTask
    {
        [JsonIgnore]
        private object _lock = new object();

        public JiraTask()
        {
            TimeSpans = new ObservableCollection<TimeKeeperTimeSpan>();
        }

        public void Initialise()
        {
            UpdateTotalTime(TimeSpan.Zero);
        }

        public EStatus Status { get; set; }

        public bool Completed
        {
            get
            {
                return completed;
            }
            set
            {
                completed = value;
                OnPropertyChanged();
            }
        }

        public DateTime? ReminderDate { get; set; }

        public string ReminderText
        {
            get
            {
                return reminderText;
            }
            set
            {
                reminderText = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }

        private bool archived;

        public bool Archived
        {
            get { return archived; }
            set { archived = value; OnPropertyChanged(); }
        }

        private bool busy;

        public bool Busy
        {
            get { return busy; }
            set { busy = value; OnPropertyChanged(); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value?.ToUpperInvariant(); OnPropertyChanged(); }
        }

        private DateTime? start;

        public DateTime? Start
        {
            get { return start; }
            set { start = value; OnPropertyChanged(); }
        }

        private DateTime? end;

        public DateTime? End
        {
            get { return end; }
            set { end = value; OnPropertyChanged(); }
        }

        private TimeSpan totalTime;

        public TimeSpan TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TimeKeeperTimeSpan> timeSpans;
        private bool isActive;
        private DateTime lastModified;
        private bool completed;
        private string reminderText;

        public ObservableCollection<TimeKeeperTimeSpan> TimeSpans
        {
            get { return timeSpans; }
            set { timeSpans = value; OnPropertyChanged(); }
        }
        [JsonIgnore]
        public TimeKeeperTimeSpan ActiveTimeSpan { get; set; }

        public void AddTime(TimeSpan timeSpan)
        {
            if (IsActive == false)
            {
                return;
            }

            lock (_lock)
            {
                ActiveTimeSpan.Time = ActiveTimeSpan.Time.Add(timeSpan);
                TotalTime = TotalTime.Add(timeSpan);
            }
        }

        private void UpdateTotalTime(TimeSpan timeSpan)
        {
            //assumption, new time is the only one that changes so we should add that to already existing times
            TotalTime = TimeSpan.Zero;
            foreach (var times in TimeSpans.Where(ts => ts.TimeSpanType == Enums.ETimeSpanType.Work))
            {
                TotalTime += times.Time;
            }
            var totes = TotalTime;
            var interuptions = TimeSpans.Where(ts => ts.TimeSpanType != Enums.ETimeSpanType.Work).Select(t => t.Time);
            foreach (var t in interuptions)
            {
                totes = totes.Subtract(t);
            }
            TotalTime = totes;
            TotalTime = TotalTime.Add(timeSpan);
        }

        public void AddFixedTimeSpan(TimeKeeperTimeSpan keeperTimeSpan)
        {
            TimeSpans.Insert(0, keeperTimeSpan);
            UpdateTotalTime(TimeSpan.Zero);
        }

        [JsonIgnore]
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
                LastModified = DateTime.UtcNow;
            }
        }

        public void StartTimeKeeping()
        {
            IsActive = true;
            if (TotalTime == TimeSpan.FromSeconds(0))
            {
                foreach (var timeWithId in TimeSpans)
                {
                    TotalTime = TotalTime.Add(timeWithId.Time);
                }
            }

            var index = TimeSpans.Count;
            var newT = new TimeKeeperTimeSpan { Id = index, Time = TimeSpan.FromSeconds(0), StartDate = DateTime.Now };
            ActiveTimeSpan = newT;
            TimeSpans.Add(newT);
        }

        public void Stop()
        {
            IsActive = false;
            ActiveTimeSpan = null;
        }

        public const int LockTimeout = 1000;

        public void Lock()
        {
            Busy = true;
            Monitor.TryEnter(_lock, LockTimeout);
        }

        public void Unlock()
        {
            Monitor.Exit(_lock);
            Busy = false;
        }

        public DateTime LastModified
        {
            get
            {
                return lastModified;
            }
            set
            {
                lastModified = value;
                OnPropertyChanged();
            }
        }
    }
}
