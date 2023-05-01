using CommunityToolkit.Mvvm.Input;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

public partial class ProjectCreateViewModel : ViewModelBase
{
    private readonly IAlertService _alertService;
    private readonly INavigationService _navigationService;
    private readonly IProjectFacade _projectFacade;

    public ProjectCreateViewModel(IProjectFacade projectFacade,
        INavigationService navigationService, IMessengerService messengerService, IAlertService alertService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;


    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Project = await _projectFacade.GetAsync(Id, string.Empty) ?? ProjectDetailModel.Empty;
    }

    [RelayCommand]
    private async Task SaveProjectAsync()
    {
        try
        {
            await _projectFacade.SaveAsync(Project);
        }
        catch (Exception ex)
        {
            await _alertService.DisplayAsync("Project Error", ex.Message);
        }

        MessengerService.Send(new ProjectCreateMessage { ProjectId = Project.Id });
        _navigationService.SendBackButtonPressed();
    }
}
