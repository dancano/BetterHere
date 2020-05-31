using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterHere.Prism.ViewModels
{
    public class FoodPageViewModel : ViewModelBase
    {
        public FoodPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Foods";
        }
    }
}
