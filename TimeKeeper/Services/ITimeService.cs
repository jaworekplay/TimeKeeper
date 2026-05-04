
namespace TimeKeeper.Services
{
    public interface ITimeService
    {
        event EventHandler<ulong> Tick;

        void Setup(int interval);
    }
}