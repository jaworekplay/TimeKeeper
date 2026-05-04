using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Services
{
    public class TimeService : ITimeService
    {
        Timer timer;
        ulong ticks;
        public event EventHandler<ulong> Tick;

        public TimeService()
        {
            
        }

        public void Setup(int interval)
        {
            timer = new Timer(new TimerCallback(TimerTick), null, interval, interval);
        }

        private void TimerTick(object state)
        {
            ++ticks;
            Tick.Invoke(this, ticks);
        }
    }
}
