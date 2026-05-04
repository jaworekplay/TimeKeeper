using System.IO.IsolatedStorage;
using TimeKeeper.Base;

namespace TimeKeeper.Services
{
    public interface ISerialisationService
    {
        IList<T> Load<T>() where T : ISerialisable, new();
        bool Save<T>(T instance) where T : ISerialisable, new();
        bool Archive<T>(T instance) where T : ISerialisable, new();
        bool Archive(string[] fileNames);
        IsolatedStorageFile IsolatedStorage { get; }
    }
}