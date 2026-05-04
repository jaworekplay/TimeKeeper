using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Base;
using TimeKeeper.Enums;
using TimeKeeper.Interfaces;

namespace TimeKeeper.Models
{
    public class TimeKeeperTimeSpan : Observable, ISelectable
    {
        private TimeSpan time;
        private string additionalInformation;
        private bool isSelected;

        public TimeKeeperTimeSpan()
        {
            TimeSpanType = ETimeSpanType.Work;
            IsSelected = true;
        }

        public int Id { get; set; }
        public TimeSpan Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }
        public DateTime StartDate { get; set; }
        public ETimeSpanType TimeSpanType { get; set; }
        public string AdditionalInformation
        {
            get
            {
                return additionalInformation;
            }
            set
            {
                additionalInformation = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}
