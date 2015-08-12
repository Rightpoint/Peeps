using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using Microsoft.WindowsAzure.MobileServices;
using Rightpoint.Peeps.Client.Infrastructure;
using Rightpoint.Peeps.Client.Models;

namespace Rightpoint.Peeps.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Peep> Peeps { get; set; }

        public MainViewModel(NavigationService navigationService) : base(navigationService)
        {
            PeepsMobileServiceClient client = new PeepsMobileServiceClient();
            IMobileServiceTable<Peep> peeps = client.GetTable<Peep>();

            this.Peeps = Peeps.AsEnumerable().ToObservableCollection();
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            // load logic (note that "e" contains Parameter)

            base.OnNavigatedTo(e);
        }
    }
}
