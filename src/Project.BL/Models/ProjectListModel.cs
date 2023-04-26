namespace Project.BL.Models;

public record ProjectListModel : ModelBase
{
    public static ProjectListModel Empty = new() { Id = Guid.Empty, Name = string.Empty };

    public required string Name { get; set; }
}
