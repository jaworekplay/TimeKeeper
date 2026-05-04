using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Base;
using TimeKeeper.Enums;
using TimeKeeper.Models;
using TimeKeeper.Services;

namespace TimeKeeper.VMs
{
    public class AddTimeSpanViewModel : BaseViewModel
    {
        public AddTimeSpanViewModel(IWindowService windowService)
        {
            Title = "Add Time Span";
            TimeSpan = new TimeKeeperTimeSpan();
            TimeSpan.StartDate = DateTime.Now;
            WindowService = windowService;

            var vals = Enum.GetValues<ETimeSpanType>();
            Types = new ObservableCollection<ETimeSpanType>(vals);
            CloseCommand = new DelegateCommand(Close);
        }

        private void Close()
        {
            WindowService.CloseWindow(Identifier);
        }

        public TimeKeeperTimeSpan TimeSpan { get; set; }
        public ObservableCollection<ETimeSpanType> Types { get; set; }
        public IDelegateCommand CloseCommand { get; set; }
        public IWindowService WindowService { get; }
    }
}
