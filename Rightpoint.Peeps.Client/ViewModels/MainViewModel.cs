using System.Collections.ObjectModel;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Rightpoint.Peeps.Client.Models;

namespace Rightpoint.Peeps.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Peep> Peeps { get; set; }

        public MainViewModel(NavigationService navigationService) : base(navigationService)
        {
            this.Peeps = new ObservableCollection<Peep>(); // TODO: sync table to populate UI with real data
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            // load logic (note that "e" contains Parameter)

            base.OnNavigatedTo(e);
        }
    }
}
