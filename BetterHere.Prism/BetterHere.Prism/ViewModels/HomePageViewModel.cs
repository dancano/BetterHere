using BetterHere.Common.Models;
using BetterHere.Common.Services;
using Prism.Navigation;
using Xamarin.Essentials;

namespace BetterHere.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;

        public HomePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Establishments";
            LoadEstablishment();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private async void LoadEstablishment()
        {
            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }


            Response response = await _apiService.GetEstablishmentAsync(url, "api", "/Establishment");


            /*var Slave = JsonConvert.DeserializeObject<SlaveResponse>(Settings.User);
            Response response = await _apiService.GetTripAsync(Slave.Document, url, "api", "/Slaves");
            IsRunning = false;


            Slave = (SlaveResponse)response.Result;
            Trips = Slave.Trips.Where(t => t.CityVisited != null).Select(t => new TripDetailsItemViewModel(_navigationService)
            {
                Id = t.Id,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                CityVisited = t.CityVisited,
                tripDetails = t.tripDetails,
                User = t.User
            }).OrderByDescending(t => t.StartDate).ToList();*/

        }
    }
}
