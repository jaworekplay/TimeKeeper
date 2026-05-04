using System.IO.IsolatedStorage;
using TimeKeeper.Base;
using TimeKeeper.Models;

namespace TimeKeeper.Services
{
    public interface IFileService
    {
        IsolatedStorageFile IsolatedStorage { get; }

        void OpenStoredFiles();
        string[] GetAllFiles();
        string GetFileForTask(ISerialisable task);
    }
}