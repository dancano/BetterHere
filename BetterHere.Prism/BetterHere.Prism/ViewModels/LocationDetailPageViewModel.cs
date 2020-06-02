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

namespace BetterHere.Prism.ViewModels
{
    public class LocationDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private EstablishmentLocationResponse _establishmentLocation;
        private bool _isRunning;
        private bool _isEnabled;
        private ObservableCollection<FoodResponse> _foodLocations;

        public LocationDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base (navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Location Detail Page";
            LoadFood();
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

        public EstablishmentLocationResponse EstablishmentLocation
        {
            get => _establishmentLocation;
            set => SetProperty(ref _establishmentLocation, value);
        }

        public ObservableCollection<FoodResponse> FoodLocations
        {
            get => _foodLocations;
            set => SetProperty(ref _foodLocations, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Location"))
            {
                EstablishmentLocation = parameters.GetValue<EstablishmentLocationResponse>("Location");
            }
        }

        private async void LoadFood()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();

            EstablishmentLocation = JsonConvert.DeserializeObject<EstablishmentLocationResponse>(Settings.EstablishmentLocation);

            FoodRequest request = new FoodRequest
            {
                EstablishmentLocationId = EstablishmentLocation.Id
            };

            Response response = await _apiService.GetFoodAsync(url, "api", "/Food/GetFoods", request);

            List<FoodResponse> foods = (List<FoodResponse>)response.Result;
            FoodLocations = new ObservableCollection<FoodResponse>(foods.Select(f => new FoodItemViewModel(_navigationService)
            {
                Id = f.Id,
                FoodName = f.FoodName,
                PicturePathFood = f.PicturePathFood,
                Qualification = f.Qualification,
                Remarks = f.Remarks,
                TypeFoods = f.TypeFoods
            }).ToList());
        }

    }
}
