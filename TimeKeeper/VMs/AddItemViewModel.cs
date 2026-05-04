using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeKeeper.Base;

namespace TimeKeeper.VMs
{
    public class AddItemViewModel : BaseViewModel
    {
        public AddItemViewModel() { }

        public AddItemViewModel(string whatAreWeAdding) : this()
        {
            Title = $"Add {whatAreWeAdding}";
        }
    }
}
