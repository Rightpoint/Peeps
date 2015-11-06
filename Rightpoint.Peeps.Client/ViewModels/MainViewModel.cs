using System;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using Microsoft.WindowsAzure.MobileServices;
using Rightpoint.Peeps.Client.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;

namespace Rightpoint.Peeps.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMobileServiceClient _mobileServiceClient;

        public DynamicCollection<Peep> Peeps { get; set; } = new DynamicCollection<Peep>();

        private BasicDigitalClock _digitalClock;

        public BasicDigitalClock DigitalClock
        {
            get { return _digitalClock; }
            set
            {
                if (_digitalClock != value)
                {
                    _digitalClock = value;
                    RaisePropertyChanged(() => DigitalClock);
                }
            }
        }

        public MainViewModel(NavigationService navigationService, IMobileServiceClient mobileServiceClient) : base(navigationService)
        {
            if (mobileServiceClient == null) throw new ArgumentNullException(nameof(mobileServiceClient));
            this._mobileServiceClient = mobileServiceClient;

            DigitalClock = new BasicDigitalClock();

            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        protected override async Task LoadData(NavigationEventArgs e)
        {
            this.Peeps.Collection.Add(new Peep());

            IMobileServiceTable<Peep> peepsTable = this._mobileServiceClient.GetTable<Peep>();

            List<Peep> peeps = (await peepsTable.ReadAsync()).ToList();

            this.Peeps.Initialize(peeps);
        }

        public override void Cleanup()
        {
            this.Peeps.Cleanup();

            base.Cleanup();
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            var strDate = DigitalClock.DateFormatted;
            var strTime = DigitalClock.TimeFormatted;

            Debug.WriteLine(strTime);
        }
    }
}
