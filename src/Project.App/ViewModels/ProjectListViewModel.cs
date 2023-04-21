﻿using Project.App.Services;

namespace Project.App.ViewModels;

public partial class ProjectListViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public ProjectListViewModel(INavigationService navigationService) => _navigationService = navigationService;
}
