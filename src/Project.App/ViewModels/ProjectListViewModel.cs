using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectListViewModel : ViewModelBase, IRecipient<UserLoginMessage>
{
    private readonly INavigationService _navigationService;
    private readonly IProjectFacade _projectFacade;
    private readonly IUserFacade _userFacade;

    public Guid Id { get; set; }
    public UserDetailModel? User { get; set; }

    public ProjectListViewModel(INavigationService navigationService,
        IMessengerService messengerService, IProjectFacade projectFacade, IUserFacade userFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _projectFacade = projectFacade;
        _userFacade = userFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await _userFacade.GetAsync(Id, "Projects");
    }

    public async void Receive(UserLoginMessage message)
    {
        Id = message.UserId;
        await LoadDataAsync();
    }
}
