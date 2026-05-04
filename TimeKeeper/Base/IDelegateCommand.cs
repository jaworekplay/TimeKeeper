using System.Windows.Input;

namespace TimeKeeper.Base
{
    public interface IDelegateCommand : ICommand
    {
        void Refresh();
        string Text { get; set; }
    }
}