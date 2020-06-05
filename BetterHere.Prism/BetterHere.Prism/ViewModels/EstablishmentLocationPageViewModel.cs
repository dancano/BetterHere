using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Common.Services;
using BetterHere.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

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
        private DelegateCommand _addLocationCommand;

        public EstablishmentLocationPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Establishments Locations";
            LoadUbications();
        }

        public DelegateCommand AddLocationCommand => _addLocationCommand ??
                (_addLocationCommand = new DelegateCommand(AddLocationAsync));

        private async void AddLocationAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(AddLocationPage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
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
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();

            Establishment = JsonConvert.DeserializeObject<EstablishmentResponse>(Settings.Establishment);

            Response response = await _apiService.GetEstablishmentDetailAsync(Establishment.Name, url, "api", "/establishment");

            IsRunning = false;

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
