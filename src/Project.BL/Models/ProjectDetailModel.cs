using System.Collections.ObjectModel;

namespace Project.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();
    public ObservableCollection<UserProjectListModel> Users { get; init; } = new();

    public static ProjectDetailModel Empty = new()
    {
        Id = Guid.Empty,
        Name = string.Empty
    };
}
