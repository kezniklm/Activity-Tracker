using System.Collections.ObjectModel;

namespace Project.BL.Models;

public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? PhotoUrl { get; set; }
    public ObservableCollection<ActivityListModel> Activities { get; set; } = new();
    public ObservableCollection<UserProjectListModel> Projects { get; set; } = new();

    public static UserDetailModel Empty => new()
    {
        Id= Guid.Empty,
        Name= string.Empty,
        Surname= string.Empty,
    };
}
