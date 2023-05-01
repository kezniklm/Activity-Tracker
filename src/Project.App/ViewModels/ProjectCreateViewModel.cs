using CommunityToolkit.Mvvm.Input;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

public partial class ProjectCreateViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IProjectFacade _projectFacade;

    public ProjectCreateViewModel(IProjectFacade projectFacade,
        INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
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
        await _projectFacade.SaveAsync(Project);
        MessengerService.Send(new ProjectCreateMessage { ProjectId = Project.Id });
        _navigationService.SendBackButtonPressed();
    }
}
