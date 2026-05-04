using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeKeeper.Base;
using TimeKeeper.Interfaces;
using TimeKeeper.Models;
using TimeKeeper.Services;

namespace TimeKeeper.VMs
{
    public class MainViewModel : BaseViewModel, ICloseable
    {
        private const string AwesomeTitle = "Time Keeper";
        private const int Seconds = 1;
        private ITimeService timerService;
        private IWindowService windowService;
        private ISerialisationService serialisationService;
        private ITimeService serialisationTimerService;
        private IDispatcherService dispatcherService;
        private IFileService fileService;

        public MainViewModel() : this(null, null, null, null, null, null)
        {
            Title = AwesomeTitle;
            Tasks = new ObservableCollection<JiraTask>();
            CreateTaskCommand = new DelegateCommand(CreateTask, CanCreateTask) { Text = "Create Task" };
            StartTimerCommand = new DelegateCommand<JiraTask>(StartTimerForTask) { Text = "Start" };
            StopTimerCommand = new DelegateCommand<JiraTask>(StopTimerForTasks) { Text = "Stop" };
            SaveCommand = new DelegateCommand(Save) { Text = "Save" };
            LoadSavedTasksCommand = new DelegateCommand(LoadSavedTasks) { Text = "Load Saved Tasks" };
            PopOutCommand = new DelegateCommand<JiraTask>(PopOut);
            OpenSavedFilesCommand = new DelegateCommand(OpenSavedFiles) { Text = "Open Saved Files" };
            ArchiveTaskCommand = new DelegateCommand<JiraTask>(ArchiveTask) { Text = "Archive" };
            AddTimeSpanCommand = new DelegateCommand<JiraTask>(AddTimeSpan) { Text = "Add Time" };
            ViewDayActivityCommand = new DelegateCommand(ViewDayActivity) { Text = "View Day Activity" };

            LoadTasks();
        }


        public MainViewModel(
            ITimeService timerService,
            IWindowService windowService,
            ISerialisationService serialisationService,
            ITimeService serialisationTimerService,
            IDispatcherService dispatcher,
            IFileService fileService)
        {
            this.timerService = timerService ?? new TimeService();
            this.timerService.Setup(1000);
            this.timerService.Tick += TimerService_Tick;
            this.windowService = windowService ?? new WindowService();
            this.serialisationService = serialisationService ?? new SerialisationService();
            this.serialisationTimerService = serialisationTimerService ?? new TimeService();
            this.serialisationTimerService.Setup(1000 * 60 * 10);//10 minutes
            this.serialisationTimerService.Tick += SerialisationTimerService_Tick;
            this.dispatcherService = dispatcher ?? new DispatcherService();
            this.fileService = fileService ?? new FileService(this.serialisationService.IsolatedStorage);
        }

        public void LoadTasks()
        {
            var items = serialisationService.Load<JiraTask>().ToList();
            foreach (var item in items.OrderByDescending(i => i.LastModified))
            {
                item.Initialise();
                Tasks.Add(item);
            }
        }

        private void SerialisationTimerService_Tick(object? sender, ulong e)
        {
            foreach (var task in Tasks)
            {
                serialisationService.Save(task);
            }
        }

        private void TimerService_Tick(object? sender, ulong e)
        {
            var time = TimeSpan.FromSeconds(e);
            TimeRunning = time;
            if (SelectedTask is object)
            {
                dispatcherService.RunOnUIThread(() => SelectedTask.AddTime(TimeSpan.FromSeconds(Seconds)));
            }
        }

        private TimeSpan timeRunning;

        public TimeSpan TimeRunning
        {
            get { return timeRunning; }
            set { timeRunning = value; OnPropertyChanged(); }
        }


        private bool busy;

        public bool Busy
        {
            get { return busy; }
            set { busy = value; OnPropertyChanged(); }
        }

        public ObservableCollection<JiraTask> Tasks { get; set; }
        private JiraTask selectedTask;

        public JiraTask SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged();
            }
        }


        public ICommand CreateTaskCommand { get; set; }
        private bool CanCreateTask()
        {
            return !Busy;
        }

        private void CreateTask()
        {
            var task = new JiraTask();
            var id = Tasks.Max(j => j.Id);
            task.Id = ++id;
            Tasks.Insert(0, task);
            StartTimerForTask(task);
        }

        public ICommand StartTimerCommand { get; set; }

        private void StartTimerForTask(JiraTask jiraTask)
        {
            jiraTask.Start = DateTime.Now;
            jiraTask.StartTimeKeeping();
            SelectedTask = jiraTask;
        }

        public ICommand StopTimerCommand { get; set; }

        private void StopTimerForTasks(JiraTask task)
        {
            task.Stop();
        }

        public ICommand SaveCommand { get; set; }

        private void Save()
        {
            foreach (var task in Tasks)
            {
                serialisationService.Save(task);
            }
        }

        public ICommand LoadSavedTasksCommand { get; set; }

        private void LoadSavedTasks()
        {
            var tasks = serialisationService.Load<JiraTask>();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        public ICommand PopOutCommand { get; set; }

        private void PopOut(JiraTask task)
        {
            var vm = new JiraTaskDetailsViewModel(task);
            windowService.Show(vm);
        }

        public ICommand OpenSavedFilesCommand { get; set; }

        void OpenSavedFiles()
        {
            fileService.OpenStoredFiles();
        }

        public ICommand ArchiveTaskCommand { get; set; }

        void ArchiveTask(JiraTask jiraTask)
        {
            serialisationService.Archive(jiraTask);
        }

        public IDelegateCommand AddTimeSpanCommand { get; set; }

        void AddTimeSpan(JiraTask jiraTask)
        {
            AddTimeSpanViewModel vm = new AddTimeSpanViewModel(windowService);
            var result = windowService.ShowDialog(vm);
            if (result == true)
            {
                jiraTask.AddFixedTimeSpan(vm.TimeSpan);
            }
        }

        public IDelegateCommand ViewDayActivityCommand { get; set; }

        void ViewDayActivity()
        {
            var vm = new DayActivityViewModel(Tasks.Where(t => t.LastModified.Date == DateTime.Now.Date));
            windowService.ShowDialog(vm);
        }

        public void Close()
        {
            windowService.CloseWindow(Identifier);
        }
    }
}
