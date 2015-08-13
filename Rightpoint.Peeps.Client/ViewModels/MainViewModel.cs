using System;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using Microsoft.WindowsAzure.MobileServices;
using Rightpoint.Peeps.Client.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Rightpoint.Peeps.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMobileServiceClient _mobileServiceClient;

        public DynamicCollection<Peep> Peeps { get; set; } = new DynamicCollection<Peep>();

        public MainViewModel(NavigationService navigationService, IMobileServiceClient mobileServiceClient) : base(navigationService)
        {
            if (mobileServiceClient == null) throw new ArgumentNullException(nameof(mobileServiceClient));
            this._mobileServiceClient = mobileServiceClient;
        }

        protected override async Task LoadData(NavigationEventArgs e)
        {
            IMobileServiceTable<Peep> peepsTable = this._mobileServiceClient.GetTable<Peep>();

            List<Peep> peeps = (await peepsTable.ReadAsync()).ToList();

            this.Peeps.Initialize(peeps);
        }

        public override void Cleanup()
        {
            this.Peeps.Cleanup();

            base.Cleanup();
        }
    }
}
