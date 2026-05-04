using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Base
{
    public class BaseViewModel : Observable
    {
        private const string TitleTempString = "Change me";
        private string title;

        public BaseViewModel()
        {
            Title = TitleTempString;
            Identifier = Guid.NewGuid();
        }

        public void RefreshCommands(params IDelegateCommand[] commands)
        {
            foreach (var command in commands)
            {
                command.Refresh();
            }
        }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        public Guid Identifier { get; set; }
    }
}
