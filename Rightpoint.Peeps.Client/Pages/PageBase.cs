using System;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ServiceLocation;
using Rightpoint.Peeps.Client.ViewModels;

namespace Rightpoint.Peeps.Client.Pages
{
    public class PageBase : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Unloaded -= PageBase_Unloaded;
            this.Unloaded += PageBase_Unloaded;

            string assemblyName = this.GetType().GetTypeInfo().Assembly.GetName().Name;

            string fullName = $"{assemblyName}.ViewModels.{e.SourcePageType.Name.Replace("Page", "ViewModel")}";
            Type viewModelType = Type.GetType(fullName);

            ViewModelBase viewModel = (ViewModelBase)ServiceLocator.Current.GetInstance(viewModelType);
            viewModel.OnNavigatedTo(e);

            this.DataContext = viewModel;

            base.OnNavigatedTo(e);
        }

        private void PageBase_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModelBase viewModel = (ViewModelBase)this.DataContext;
            viewModel.Cleanup();
        }
    }
}
