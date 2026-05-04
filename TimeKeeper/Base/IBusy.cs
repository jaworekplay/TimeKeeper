using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Base
{
    public interface IBusy
    {
        bool Busy { get; set; }
    }
}
