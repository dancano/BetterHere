using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Common.Services;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BetterHere.Prism.ViewModels
{
    public class AddFoodPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private ImageSource _image;
        private readonly FoodRequest _food;
        private TypeFoodResponse _typefood;
        private ObservableCollection<TypeFoodResponse> _typefoods;
        private bool _isRunning;
        private bool _isEnabled;
        private MediaFile _file;
        private DelegateCommand _addFoodCommand;
        private DelegateCommand _changeImageCommand;

        public AddFoodPageViewModel(
            INavigationService navigationService,
            IApiService apiService,
            IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Title = "Add Food";
            Image = App.Current.Resources["UrlNoImage"].ToString();
            IsEnabled = true;
            Food = new FoodRequest();
            TypeFoods = new ObservableCollection<TypeFoodResponse>(CombosHelper.GetTypeFood());
        }

        public DelegateCommand AddFoodCommand => _addFoodCommand ?? (_addFoodCommand = new DelegateCommand(RegisterFoodAsync));
        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));


        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public FoodRequest Food
        {
            get; set;
        }

        public TypeFoodResponse TypeFood
        {
            get => _typefood;
            set => SetProperty(ref _typefood, value);
        }

        public ObservableCollection<TypeFoodResponse> TypeFoods
        {
            get => _typefoods;
            set => SetProperty(ref _typefoods, value);
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

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                "PictureSource",
                "Cancel",
                null,
                "FromGallery",
                "FromCamera");

            if (source == "Cancel")
            {
                _file = null;
                return;
            }

            if (source == "FromCamera")
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }
        private async void RegisterFoodAsync()
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
            EstablishmentLocationResponse establishmentLocation = JsonConvert
                .DeserializeObject<EstablishmentLocationResponse>(Settings.EstablishmentLocation);

            FoodRequest request = new FoodRequest
            {
                FoodName = Food.FoodName,
                Remarks = Food.Remarks,
                EstablishmentLocationId = establishmentLocation.Id,
                Qualification = Food.Qualification,
                UserId = new Guid(user.Id)
            };

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            request.PictureFoodArray = imageArray;
            request.TypeFoodsId = TypeFood.Id;
            Response responseFind = await _apiService.GetFoodAsync(url, "api", "/Food/GetFoods", request);
            List<FoodResponse> foods = (List<FoodResponse>)responseFind.Result;
            var x = foods.FindIndex(f => f.FoodName == request.FoodName);           

            if (x.Equals(null))
            {
                await App.Current.MainPage.DisplayAlert("Error", "This establishment already exist", "Accept");
                return;
            }

            Response response = await _apiService.NewFoodAsync(url, "/api", "/Food/PostFood", request, "bearer", token.Token);

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

            await App.Current.MainPage.DisplayAlert("Congratulations!!!", "Registered Food!", "Accept");
            await _navigationService.GoBackAsync();
        }


        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Food.FoodName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Insert Name", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(Food.TypeFoodsId.ToString()))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Insert Type Food", "Accept");
                return false;
            }

            return true;
        }
    }
}
