using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Common.Services;
using BetterHere.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace BetterHere.Prism.ViewModels
{
    public class AddLocationPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private readonly IGeolocatorService _geolocatorService;
        private TypeEstablishmentResponse  _typeEstablishment;
        private ObservableCollection<TypeEstablishmentResponse> _typeEstablishments;
        private CityResponse _city;
        private ObservableCollection<CityResponse> _cities;
        private string _source;
        private string _buttonLabel;
        private bool _isSecondButtonVisible;
        private bool _isRunning;
        private bool _isEnabled;
        private EstablishmentLocationResponse _locationResponse;
        private Position _position;
        private DelegateCommand _getAddressCommand;
        private DelegateCommand _addLocationCommand;

        public AddLocationPageViewModel(
            INavigationService navigationService, 
            IApiService apiService,
            IGeolocatorService geolocatorService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _geolocatorService = geolocatorService;
            Title = "Add Location";
            IsEnabled = true;
            Location = new EstablishmentLocationRequest();
            TypeEstablishments = new ObservableCollection<TypeEstablishmentResponse>(CombosHelper.GetTypeEstablishment());
            Cities = new ObservableCollection<CityResponse>(CombosHelper.GetCities());
        }

        public DelegateCommand AddLocationCommand => _addLocationCommand ?? (_addLocationCommand = new DelegateCommand(NewLocationAsync));
        public DelegateCommand GetAddressCommand => _getAddressCommand ?? (_getAddressCommand = new DelegateCommand(LoadSourceAsync));

        public EstablishmentLocationRequest Location
        {
            get; set;
        }

        public TypeEstablishmentResponse TypeEstablishment
        {
            get => _typeEstablishment;
            set => SetProperty(ref _typeEstablishment, value);
        }

        public ObservableCollection<TypeEstablishmentResponse> TypeEstablishments
        {
            get => _typeEstablishments;
            set => SetProperty(ref _typeEstablishments, value);
        }

        public CityResponse City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public ObservableCollection<CityResponse> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }

        public bool IsSecondButtonVisible
        {
            get => _isSecondButtonVisible;
            set => SetProperty(ref _isSecondButtonVisible, value);
        }

        public string ButtonLabel
        {
            get => _source;
            set => SetProperty(ref _source, value);
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

        public string Source
        {
            get => _buttonLabel;
            set => SetProperty(ref _buttonLabel, value);
        }

        private async void LoadSourceAsync()
        {
            await _geolocatorService.GetLocationAsync();
            if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
            {
                _position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
                Geocoder geoCoder = new Geocoder();
                IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(_position);
                List<string> addresses = new List<string>(sources);

                if (addresses.Count > 0)
                {
                    Source = addresses[0];
                }
            }
        }


        private async void NewLocationAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "You dn't have connection", "Accept");
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EstablishmentResponse establishment = JsonConvert.DeserializeObject<EstablishmentResponse>(Settings.Establishment);

            EstablishmentLocationRequest locationRequest = new EstablishmentLocationRequest
            {
                SourceLatitude = _geolocatorService.Latitude,
                SourceLongitude = _geolocatorService.Longitude,
                TargetLatitude = _geolocatorService.Latitude,
                TargetLongitude = _geolocatorService.Longitude,
                IdEstablishment = establishment.Id,
                Remarks = Location.Remarks
            };

            locationRequest.IdCity = City.Id;
            locationRequest.IdTypeEstablishment = TypeEstablishment.Id;

            Response response = await _apiService.NewEstablishmentLocationAsync(
                url, 
                "/api",
                "/EstablishmentLocation/PostEstablishmentLocation/", 
                locationRequest, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            IsRunning = false;
            IsEnabled = true;

            _locationResponse = (EstablishmentLocationResponse)response.Result;
            AddLocationPage.GetInstance().AddPin(_position, Source, establishment.Name, PinType.Place);
            await App.Current.MainPage.DisplayAlert("Congratulations!!!", "Registered Place!", "Accept");
            await _navigationService.GoBackAsync();
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Location.IdCity.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Select a city", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(Location.IdTypeEstablishment.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Select type establishment", "Accept");
                return false;
            }

            return true;
        }
    }
}
