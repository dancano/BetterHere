using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Common.Services;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BetterHere.Prism.ViewModels
{
    public class AddEstablishmentPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private bool _isRunning;
        private bool _isEnabled;
        private ImageSource _image;
        private MediaFile _file;
        private DelegateCommand _registerEstablishmentCommand;
        private DelegateCommand _changeImageCommand;

        public AddEstablishmentPageViewModel(
            INavigationService navigationService,
            IApiService apiService,
            IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Title = "Add Establishment";
            Image = App.Current.Resources["UrlNoImage"].ToString();
            IsEnabled = true;
            IsRunning = false;
            Establishment = new EstablishmentRequest();
        }

        public DelegateCommand RegisterEstablishmentCommand => _registerEstablishmentCommand ?? 
            (_registerEstablishmentCommand = new DelegateCommand(AddEstablishmentAsync));

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public EstablishmentRequest Establishment
        {
            get; set;
        }

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
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

        private async void AddEstablishmentAsync()
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

            EstablishmentRequest request = new EstablishmentRequest
            {
                Name = Establishment.Name,
                UserId = new Guid(user.Id)
            };

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            request.PictureEstablishmentArray = imageArray;
            Response responseFind = await _apiService.GetEstablishmentDetailAsync(url, "/api", "/Establishment/", Establishment.Name);

            if (responseFind.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "This establishment already exist", "Accept");
                return;
            }

            Response response = await _apiService.NewEstablishmentAsync(url, "/api", "/establishment", request, "bearer", token.Token);

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

            await App.Current.MainPage.DisplayAlert("Congratulations!!!", "Registered Establishment!", "Accept");
            await _navigationService.GoBackAsync();
        }


        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Establishment.Name))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Insert Name", "Accept");
                return false;
            }

            return true;
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
    }
}
