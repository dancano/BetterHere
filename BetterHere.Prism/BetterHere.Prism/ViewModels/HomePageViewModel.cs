using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Common.Services;
using BetterHere.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace BetterHere.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private DelegateCommand _addEstablishmentCommand;
        private ObservableCollection<EstablishmentResponse> _establishments;

        public HomePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Establishments";
            LoadEstablishment();
        }

        public DelegateCommand AddEstablishmentCommand => _addEstablishmentCommand ??
            (_addEstablishmentCommand = new DelegateCommand(AddEstablishmentAsync));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<EstablishmentResponse> Establishments
        {
            get => _establishments;
            set => SetProperty(ref _establishments, value);
        }

        private async void LoadEstablishment()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }


            Response response = await _apiService.GetEstablishmentAsync(url, "api", "/establishment");

            IsRunning = false;

            List<EstablishmentResponse> establishments = (List<EstablishmentResponse>)response.Result;
            Establishments = new ObservableCollection<EstablishmentResponse>(establishments.Select(e => new EstablishmentItemViewModel(_navigationService)
            {
                Id = e.Id,
                Name = e.Name,
                LogoEstablishmentPath = e.LogoEstablishmentPath,
                EstablishmentLocations = e.EstablishmentLocations
            }).ToList());
        }
        private async void AddEstablishmentAsync()
        {
            if (Settings.IsLogin == true)
            {
                await _navigationService.NavigateAsync(nameof(AddEstablishmentPage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
        }
    }
}
