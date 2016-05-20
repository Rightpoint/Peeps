using System;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using Microsoft.WindowsAzure.MobileServices;
using Rightpoint.Peeps.Client.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;

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

            //TODO: load data on timer
        }

        protected override async Task LoadData(NavigationEventArgs e)
        {
            this.Peeps.Collection.Add(new Peep());

            //TODO: call config endpoint to get today's query parameters

            //TODO: replace call to table with call to api
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
