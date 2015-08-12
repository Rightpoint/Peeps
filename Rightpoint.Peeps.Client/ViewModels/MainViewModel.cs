using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using Microsoft.WindowsAzure.MobileServices;
using Rightpoint.Peeps.Client.Models;

namespace Rightpoint.Peeps.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMobileServiceClient _mobileServiceClient;

        public ObservableCollection<Peep> Peeps { get; set; }

        public MainViewModel(NavigationService navigationService, IMobileServiceClient mobileServiceClient) : base(navigationService)
        {
            if (mobileServiceClient == null) throw new ArgumentNullException(nameof(mobileServiceClient));

            this._mobileServiceClient = mobileServiceClient;
        }

        public override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // load logic (note that "e" contains Parameter)

            IMobileServiceTable<Peep> peeps = this._mobileServiceClient.GetTable<Peep>();

            this.Peeps = await peeps.ToCollectionAsync();

            base.OnNavigatedTo(e);
        }
    }
}
