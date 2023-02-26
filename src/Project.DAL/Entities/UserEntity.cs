﻿namespace Project.DAL.Entities;

public record UserEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? PhotoUrl { get; set; }
    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public ICollection<UserProjectEntity> Projects { get; set; } = new List<UserProjectEntity>();
}