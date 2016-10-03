using System;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using Microsoft.WindowsAzure.MobileServices;
using Rightpoint.Peeps.Client.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;

namespace Rightpoint.Peeps.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ResourceLoader _resource = new ResourceLoader();
        private readonly IFaceServiceClient _faceServiceClient;
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

            var refreshTimer = new DispatcherTimer();
            refreshTimer.Tick += RefreshTimerOnTick;
            refreshTimer.Interval = new TimeSpan(0, 24, 0, 0);
            refreshTimer.Start();

            _faceServiceClient = new FaceServiceClient(_resource.GetString("api_key"));
        }

        private async void CollectionTimerOnTick(object sender, object o)
        {
        }

        private void RefreshTimerOnTick(object sender, object o)
        {
            NavigationService.NavigateTo("MainPage");
        }

        private async Task<FaceRectangle[]> UploadAndDetectFaces(string imageFilePath)
        {
            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    var faces = await _faceServiceClient.DetectAsync(imageFileStream);
                    var faceRects = faces.Select(face => face.FaceRectangle);
                    return faceRects.ToArray();
                }
            }
            catch (Exception)
            {
                return new FaceRectangle[0];
            }
        }

        private async Task<IEnumerable<Peep>> GetPeepsByArgs(HttpClient client, string args)
        {
            var peeps = new List<Peep>();
            var response = client.GetAsync(new Uri($"http://rp-peeps.azurewebsites.net/api/peeps?args={args}")).Result;

            if (!response.IsSuccessStatusCode) return peeps;


            // interpret results
            var result = JsonConvert.DeserializeObject<Models.Peeps>(await response.Content.ReadAsStringAsync());

            foreach (var p in result.peeps)
            {
                try
                {
                    var img = "http://rp-peeps.azurewebsites.net" + p.photoPath;

                    var face = await _faceServiceClient.DetectAsync(img, false, true, new List<FaceAttributeType>() { FaceAttributeType.FacialHair });

                    //TODO transform rectangle based on size of image?

                    var peep = new Peep()
                    {
                        Name = p.name,
                        Hometown = p.origin,
                        Office = p.office,
                        Team = p.serviceLine,
                        ImageUrl = img,
                        Face = face.FirstOrDefault()
                    };

                    // Ensure no double-stache
                    if (peep.Face?.FaceAttributes != null)
                    {
                        if (peep.Face.FaceAttributes.FacialHair.Beard > 0.75 ||
                            peep.Face.FaceAttributes.FacialHair.Moustache > 0.75)
                            peep.Face = null;
                    }

                    peeps.Add(peep);
                    Debug.WriteLine($"Added {peep.Name}!");
                }
                catch (FaceAPIException ex)
                {
                    Debug.WriteLine($"{ex.ErrorCode}: {ex.ErrorMessage}");

                    // Assumes "Limit Reached" exception
                    await Task.Delay(10000);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Source}: {ex.Message}");
                }

            }
            return peeps;
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            //var strDate = DigitalClock.DateFormatted;
            //var strTime = DigitalClock.TimeFormatted;

            //Debug.WriteLine(strTime);
        }

        #region Override
        protected override async Task LoadData(NavigationEventArgs e)
        {
            // Get new users
            IMobileServiceTable<Peep> peepsTable = this._mobileServiceClient.GetTable<Peep>();
            List<Peep> peeps = new List<Peep>();    //(await peepsTable.ReadAsync()).ToList();

            foreach (var peep in peeps)
            {
                peep.Salutation = "Welcome!";
            }

            // Get a sample of old users
            using (var client = new HttpClient())
            {
                // Call config endpoint to get today's query parameters
                var args = string.Empty;

                // Fetch users based on parameters 
                var r = await GetPeepsByArgs(client, args);

                //Take no more than 30 from "old" set
                peeps.AddRange(r.OrderBy(x => Guid.NewGuid()).Take(30 - peeps.Count).ToList());
            }

            this.Peeps.Initialize(peeps.OrderBy(x => Guid.NewGuid()).ToList());

            Peeps.CollectionTimer.Tick += CollectionTimerOnTick;
        }

        public override void Cleanup()
        {
            this.Peeps.Cleanup();

            base.Cleanup();
        }
        #endregion
    }
}
