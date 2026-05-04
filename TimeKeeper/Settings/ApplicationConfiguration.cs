using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Settings
{
    public class ApplicationConfiguration
    {
        public TimeSpan DefaultBreakLength { get; set; }
        public TimeSpan CustomBreakLength { get; set; }
    }
}
