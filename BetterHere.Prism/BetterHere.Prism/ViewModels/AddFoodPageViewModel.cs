using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Common.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

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

        public TypeFoodResponse Typefood
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
        private void RegisterFoodAsync()
        {
            throw new NotImplementedException();
        }
    }
}
