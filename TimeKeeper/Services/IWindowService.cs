using TimeKeeper.Base;
using TimeKeeper.Models;

namespace TimeKeeper.Services
{
    public interface IWindowService
    {
        bool? MessageBoxShow(string title, string message);
        void Show(BaseViewModel baseViewModel);
        bool? ShowDialog(BaseViewModel baseViewModel);
        void Show(IViewableJiraTask jiraTask);
        bool CloseWindow(Guid identifier);
        bool CancelWindow(Guid identifier);
    }
}