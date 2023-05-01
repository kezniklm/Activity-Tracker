using Project.App.Models;
using Project.App.ViewModels;
using Project.App.Views.Activities;
using Project.App.Views.Login;
using Project.App.Views.Overview;
using Project.App.Views.Projects;

namespace Project.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        new("//projects", typeof(ProjectListView), typeof(ProjectListViewModel)),
        new("//overview", typeof(OverviewView), typeof(OverviewViewModel)),
        new("//login", typeof(LoginView), typeof(LoginViewModel)),
        new("//overview/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        new("//login/edit", typeof(UserEditView), typeof(UserEditViewModel)),
        new("//login/detail", typeof(UserDetailView), typeof(UserDetailViewModel)),
        new("//login/detail/edit", typeof(UserEditView), typeof(UserEditViewModel)),
        new("//projects/create", typeof(ProjectCreateView), typeof(ProjectCreateViewModel)),
        new("//projects/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel))
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        string route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        string route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
