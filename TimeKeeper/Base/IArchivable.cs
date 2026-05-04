using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Base
{
    public interface IArchivable
    {
        bool Archived { get; set; }
    }
}
