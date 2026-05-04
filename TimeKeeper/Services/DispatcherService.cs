using System.Windows;
using System.Windows.Threading;

namespace TimeKeeper.Services
{
    public class DispatcherService : IDispatcherService
    {
        Dispatcher Dispatcher { get; set; }

        public DispatcherService()
        {
            Dispatcher = Application.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher;
        }

        public void RunOnUIThread(Action action)
        {
            Dispatcher.BeginInvoke(action);
        }
    }
}
