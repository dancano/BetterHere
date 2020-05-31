using BetterHere.Common.Models;
using BetterHere.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterHere.Prism.ViewModels
{
    public class FoodItemViewModel : FoodResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectFoodCommand;

        public FoodItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectFoodCommand => _selectFoodCommand ??
            (_selectFoodCommand = new DelegateCommand(SelectFoodAsync));

        private async void SelectFoodAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {"food", this }
            };
            await _navigationService.NavigateAsync(nameof(FoodPage), parameters);
        }
    }
}
