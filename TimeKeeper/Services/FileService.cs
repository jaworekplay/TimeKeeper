using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Base;
using TimeKeeper.Models;

namespace TimeKeeper.Services
{
    public class FileService : IFileService
    {
        public IsolatedStorageFile IsolatedStorage { get; }

        public FileService(IsolatedStorageFile isolatedStorage)
        {
            IsolatedStorage = isolatedStorage;
        }

        public void OpenStoredFiles()
        {
            //TODO;
        }

        public string[] GetAllFiles()
        {
            var result = IsolatedStorage.GetFileNames(SerialisationService.Directory + "\\");
            return result;
        }

        public string GetFileForTask(ISerialisable task)
        {
            return $"{SerialisationService.Directory}\\{task.Id}.{SerialisationService.Extension}";
        }
    }
}
