using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xameteo
{
    public class CitySearchViewModel : BaseViewModel
    {
        string searchValue;
        public string SearchValue
        {
            get { return searchValue; }
            set { SetProperty(ref searchValue, value); }
        }

        City selectedResult;
        public City SelectedResult
        {
            get { return selectedResult; }
            set { SetProperty(ref selectedResult, value); }
        }

        bool hasResults;
        public bool HasResults
        {
            get { return hasResults; }
            set { SetProperty(ref hasResults, value); }
        }

        ObservableCollection<City> searchResult = new ObservableCollection<City>();
        public ObservableCollection<City> SearchResult
        {
            get { return searchResult; }
            set { SetProperty(ref searchResult, value); }
        }

        public ICommand GoBackCommand => new Command(GoBack);
        public ICommand SearchCommand => new Command(Search);
        public ICommand ResultSelectedCommand => new Command(ResultSelected);

        public CitySearchViewModel()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                HasResults = SearchResult.Any();
            });
        }

        async void Search()
        {
            if (IsBusy) return;
            IsBusy = true;

            SearchResult.Clear();
            if (String.IsNullOrWhiteSpace(SearchValue))
            {
                await Shell.Current.DisplayAlert("Erreur", "Veuillez entrer une ville", "OK");
            }
            else
            {
                foreach (City c in await HttpService.GetCities(SearchValue))
                {
                    Device.BeginInvokeOnMainThread(() => SearchResult.Add(c));
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    HasResults = SearchResult.Any();
                });
            }
            IsBusy = false;
        }

        public async void ResultSelected()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                LocalStorage.SaveCity(SelectedResult);
                await Shell.Current.Navigation.PopModalAsync();
                IsBusy = false;
            }
        }

        async void GoBack()
        {
            await Shell.Current.Navigation.PopModalAsync(true);
        }

        private void DisplayAlert(string message)
        {
            DependencyService.Get<IToastAlert>()?.DisplayAlert(message);
        }
    }
}
