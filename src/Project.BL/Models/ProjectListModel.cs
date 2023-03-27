namespace Project.BL.Models;
public record ProjectListModel
{
    public required string Name { get; set; }

    public static ProjectDetailModel Empty = new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
    };
}
