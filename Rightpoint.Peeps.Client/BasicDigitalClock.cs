using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Rightpoint.Peeps.Client.Annotations;

namespace Rightpoint.Peeps.Client
{
    public class BasicDigitalClock : ViewModelBase
    {
        private long currentTime;

        public BasicDigitalClock()
        {
            UpdateCurrentTime();
        }

        private void UpdateCurrentTime()
        {
            currentTime = DateTime.UtcNow.Ticks;
            RaisePropertyChanged(() => DateFormatted);
            RaisePropertyChanged(() => TimeFormatted);
        }

        public string DateFormatted
        {
            get
            {
                UpdateCurrentTime();
                DateTime now = new DateTime().AddTicks(currentTime).ToLocalTime();
                string strDate = now.ToString("D");
                return strDate;
            }
        }
        public string TimeFormatted
        {
            get
            {
                UpdateCurrentTime();
                DateTime now = new DateTime().AddTicks(currentTime).ToLocalTime();
                string strTime = now.ToString("t");
                return strTime;
            }
        }
    }
}
