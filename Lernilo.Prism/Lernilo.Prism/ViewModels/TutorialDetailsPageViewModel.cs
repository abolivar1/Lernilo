using Lernilo.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lernilo.Prism.ViewModels
{
    public class TutorialDetailsPageViewModel : ViewModelBase
    {
        private TutorialResponse _tutorial;
        public TutorialDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Tutorial Details";
        }

        public TutorialResponse Tutorial
        {
            get => _tutorial;
            set => SetProperty(ref _tutorial, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("tutorial"))
            {
                Tutorial = parameters.GetValue<TutorialResponse>("tutorial");
                Title = Tutorial.Title;
            }
        }

    }
}
