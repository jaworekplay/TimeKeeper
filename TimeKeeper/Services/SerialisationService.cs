using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.Reflection.Metadata;
using TimeKeeper.Base;//<- use this to persist log entries

namespace TimeKeeper.Services
{
    public class SerialisationService : ISerialisationService
    {
        public const string Directory = "TimeKeeper", TemporarySuffix = "_tmp", Extension = "json", ArchiveDir = "Archive";
        public IsolatedStorageFile IsolatedStorage { get; }

        public SerialisationService()
        {
            IsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            if (IsolatedStorage.DirectoryExists(Directory) == false)
            {
                IsolatedStorage.CreateDirectory(Directory);
            }
            if (IsolatedStorage.DirectoryExists(Directory+"\\"+ArchiveDir) == false)
            {
                IsolatedStorage.CreateDirectory(Directory + "\\" + ArchiveDir);
            }
        }

        public bool Save<T>(T instance) where T : ISerialisable, new()
        {
            instance.Lock();
            var json = JsonSerializer.Serialize(instance);
            instance.Unlock();
            var fileMode = FileMode.Create;
            //needs to overwrite old vals alltogether
            if (IsolatedStorage.FileExists($"{Directory}\\{instance.Id}.{Extension}") || IsolatedStorage.FileExists($"{Directory}\\{instance.Title}.{Extension}"))
            {
                fileMode = FileMode.Truncate;
            }

            using (var stream = IsolatedStorage.OpenFile($"{Directory}\\{instance.Id}.{Extension}", fileMode))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                }
            }

            return true;
        }

        public IList<T> Load<T>() where T : ISerialisable, new()
        {
            List<T> result = new List<T>();
            JsonSerializerOptions options = new JsonSerializerOptions();


            if (IsolatedStorage.DirectoryExists(Directory))
            {
                var files = IsolatedStorage.GetFileNames($"{Directory}\\");
                foreach (var filePath in files)
                {
                    using (var stream = IsolatedStorage.OpenFile($"{Directory}\\{filePath}", FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            var str = reader.ReadToEnd();
                            result.Add(JsonSerializer.Deserialize<T>(str));
                        }
                    }
                }
            }

            return result;
        }

        public bool Archive<T>(T instance) where T : ISerialisable, new()
        {
            var result = false;

            if (IsolatedStorage.DirectoryExists(Directory))
            {
                if (IsolatedStorage.FileExists($"{Directory}\\{instance.Id}.{Extension}"))
                {
                    IsolatedStorage.MoveFile($"{Directory}\\{instance.Id}.{Extension}", $"{Directory}\\{ArchiveDir}\\{instance.Id}.{Extension}");
                }
            }

            return result;
        }

        public bool Archive(string[] fileNames)
        {
            foreach (var file in fileNames)
            {
                if (IsolatedStorage.FileExists(Directory + "\\" + file))
                {
                    IsolatedStorage.MoveFile(Directory + "\\" + file, Directory + "\\" + ArchiveDir + "\\" + file);
                }
            }

            return true;
        }
    }
}
