using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Base;

namespace TimeKeeper.Models
{
    public class HourContentModel : Observable
    {
        private string title;
        private double length;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public double Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
                OnPropertyChanged();
            }
        }
    }
}
