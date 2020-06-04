using BetterHere.Common.Helpers;
using BetterHere.Common.Services;
using BetterHere.Prism.ViewModels;
using BetterHere.Prism.Views;
using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BetterHere.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjM3OTczQDMxMzgyZTMxMmUzMGsxcUcrU0cyYTdXZDVqY3dUckdPQWRoWHdrUi8wak1mRHQvSUpQMU43N0k9");
            InitializeComponent();
            await NavigationService.NavigateAsync("BetterHereMasterDetailPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IGeolocatorService, GeolocatorService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<BetterHereMasterDetailPage, BetterHereMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportPage, ReportPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<EstablishmentLocationPage, EstablishmentLocationPageViewModel>();
            containerRegistry.RegisterForNavigation<LocationDetailPage, LocationDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<FoodPage, FoodPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEstablishmentPage, AddEstablishmentPageViewModel>();
            containerRegistry.RegisterForNavigation<AddFoodPage, AddFoodPageViewModel>();
            containerRegistry.RegisterForNavigation<AddLocationPage, AddLocationPageViewModel>();
        }
    }
}
