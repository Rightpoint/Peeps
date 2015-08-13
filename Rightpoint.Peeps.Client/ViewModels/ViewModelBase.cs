using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using System.Threading.Tasks;

namespace Rightpoint.Peeps.Client.ViewModels
{
    public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        protected readonly INavigationService NavigationService;

        protected ViewModelBase(NavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }

        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadData(e);
        }

        protected abstract Task LoadData(NavigationEventArgs e);
    }
}
