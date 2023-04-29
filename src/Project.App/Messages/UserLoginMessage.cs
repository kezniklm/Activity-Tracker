namespace Project.App.Messages;

public record UserLoginMessage
{
    public required Guid UserId { get; init; }
}
