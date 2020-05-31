using Lernilo.Common.Models;
using Lernilo.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lernilo.Prism.ViewModels
{
    public class TutorialsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<TutorialItemViewModel> _tutorials;

        public TutorialsPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Tutorials";
            LoadTournamentsAsync();
        }

        public List<TutorialItemViewModel> Tutorials
        {
            get => _tutorials;
            set => SetProperty(ref _tutorials, value);
        }

        private async void LoadTournamentsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TutorialResponse>(
                url,
                "/api",
                "/Tutorials");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            List<TutorialResponse> list = (List<TutorialResponse>)response.Result;
            Tutorials = list.Select(t => new TutorialItemViewModel(_navigationService)
            {
                Title = t.Title,
                Description = t.Description,
                Id = t.Id,
                Customer = t.Customer,
                PicturePath = t.PicturePath,
                TotalRate = t.TotalRate,
                Category = t.Category,
                Date = t.Date,
                Comments = t.Comments,
                TutorialReports = t.TutorialReports
            }).ToList();
        }
    }

}
