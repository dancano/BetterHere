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
        private EstablishmentResponse _establishment;

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

        public EstablishmentResponse Establishment
        {
            get => _establishment;
            set => SetProperty(ref _establishment, value);
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


            Response response = await _apiService.GetEstablishmentAsync(url, "api", "/establishment");

            Establishment = (EstablishmentResponse)response.Result;

        }
    }
}
