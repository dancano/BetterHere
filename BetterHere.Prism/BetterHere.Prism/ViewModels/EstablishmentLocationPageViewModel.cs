using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BetterHere.Prism.ViewModels
{
    public class EstablishmentLocationPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private EstablishmentResponse _establishment;
        private bool _isRunning;
        private bool _isEnabled;
        private ObservableCollection<EstablishmentLocationResponse> _establishmentLocations;

        public EstablishmentLocationPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Establishments Locations";
            LoadUbications();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public EstablishmentResponse Establishment
        {
            get => _establishment;
            set => SetProperty(ref _establishment, value);
        }

        public ObservableCollection<EstablishmentLocationResponse> EstablishmentLocations
        {
            get => _establishmentLocations;
            set => SetProperty(ref _establishmentLocations, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("establishment"))
            {
                Establishment = parameters.GetValue<EstablishmentResponse>("establishment");
            }
        }

        private async void LoadUbications()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();

            Establishment = JsonConvert.DeserializeObject<EstablishmentResponse>(Settings.Establishment);

            Response response = await _apiService.GetEstablishmentDetailAsync(Establishment.Name, url, "api", "/establishment");

            EstablishmentResponse establishment = (EstablishmentResponse)response.Result;
            EstablishmentLocations = new ObservableCollection<EstablishmentLocationResponse>(establishment
                .EstablishmentLocations.Select(l => new EstablishmentLocationItemViewModel(_navigationService)
            {
                Id = l.Id,
                Remarks = l.Remarks,
                Cities = l.Cities,
                TypeEstablishment = l.TypeEstablishment
            }).ToList());
        }
    }
}
