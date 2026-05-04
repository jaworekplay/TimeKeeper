
namespace TimeKeeper.Services
{
    public interface IDispatcherService
    {
        void RunOnUIThread(Action action);
    }
}