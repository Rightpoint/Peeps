using System;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using Microsoft.WindowsAzure.MobileServices;
using Rightpoint.Peeps.Client.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Windows.UI.Xaml;
using Newtonsoft.Json;

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

            // Get new users
            IMobileServiceTable<Peep> peepsTable = this._mobileServiceClient.GetTable<Peep>();
            List<Peep> peeps = (await peepsTable.ReadAsync()).ToList();

            // Get a sample of old users
            using (var client = new HttpClient())
            {
                // Call config endpoint to get today's query parameters
                //var args = GetConfigInfo(client).Result;
                var args = string.Empty;

                // Fetch users based on parameters 
                var r = GetPeepsByArgs(client, args).Result;

                //Take no more than 30 from "old" set
                peeps.AddRange(r.OrderBy(x => Guid.NewGuid()).Take(30).ToList());
            }

            this.Peeps.Initialize(peeps.OrderBy(x=> Guid.NewGuid()).ToList());
        }

        private async Task<IEnumerable<Peep>> GetPeepsByArgs(HttpClient client, string args)
        {
            var peeps = new List<Peep>();
            var response = client.GetAsync(new Uri($"http://rp-peeps.azurewebsites.net/api/peeps?args={args}")).Result;

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // interpret results
                    var result = JsonConvert.DeserializeObject<Models.Peeps>(response.Content.ReadAsStringAsync().Result);

                    foreach (var p in result.peeps)
                    {
                        peeps.Add(new Peep()
                        {
                            Name = p.name,
                            Hometown = p.origin,
                            Office = p.office,
                            Team = p.serviceLine,
                            ImageUrl = "http://rp-peeps.azurewebsites.net" + p.photoPath
                        });
                    }
                }
                catch(Exception ex)
                {
                    if(Debugger.IsAttached) Debugger.Break();
                }
            }

            return peeps;
        }

        private async Task<PeepsFilter> GetConfigInfo(HttpClient client)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://rp-peeps.azurewebsites.net/api/config"),
            };

            var response = await client.SendAsync(request);

            throw new NotImplementedException();
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
