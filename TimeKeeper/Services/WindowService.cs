using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TimeKeeper.Base;
using TimeKeeper.Models;

namespace TimeKeeper.Services
{
    public class WindowService : IWindowService
    {
        public bool? ShowDialog(BaseViewModel baseViewModel)
        {
            var wnd = new Window();
            wnd.SizeToContent = SizeToContent.WidthAndHeight;
            wnd.DataContext = baseViewModel;

            return wnd.ShowDialog();
        }

        public void Show(BaseViewModel baseViewModel)
        {
            var wnd = new Window();
            wnd.DataContext = baseViewModel;

            wnd.Show();
        }

        public bool? MessageBoxShow(string title, string message)
        {
            var baseViewModel = new MessageBoxViewModel();
            baseViewModel.Title = title;
            baseViewModel.Message = message;

            var wnd = new Window();
            wnd.DataContext = baseViewModel;

            return wnd.ShowDialog();
        }

        public void Show(IViewableJiraTask jiraTask)
        {
            var baseViewModel = jiraTask;

            var wnd = new Window();
            wnd.MinHeight = 275;
            wnd.MinWidth = 436;
            wnd.SizeToContent = SizeToContent.WidthAndHeight;
            wnd.DataContext = baseViewModel;

            wnd.Show();
        }

        public bool CloseWindow(Guid identifier)
        {
            var windows = Application.Current.Windows.OfType<Window>();
            //we rely on Guid being used in Tag property
            var found = windows.FirstOrDefault(w => (Guid?)w.Tag == identifier);
            if (found == null)
            {
                return false;
            }
            found.DialogResult = true;
            found.Close();
            return true;
        }
        
        public bool CancelWindow(Guid identifier)
        {
            var windows = Application.Current.Windows.OfType<Window>();
            //we rely on Guid being used in Tag property
            var found = windows.FirstOrDefault(w => (Guid?)w.Tag == identifier);
            if (found == null)
            {
                return false;
            }

            found.Close();
            return true;
        }
    }
}
