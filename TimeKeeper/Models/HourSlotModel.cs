using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Base;

namespace TimeKeeper.Models
{
    public class HourSlotModel : Observable
    {
        public HourSlotModel()
        {
            Entries = new ObservableCollection<HourContentModel>();
        }
        private string hourText;

        public string HourText
        {
            get
            {
                return hourText;
            }
            set
            {
                hourText = value;
                OnPropertyChanged();
            }
        }

        public int Hour { get; set; }

        public ObservableCollection<HourContentModel> Entries { get; set; }
    }
}
