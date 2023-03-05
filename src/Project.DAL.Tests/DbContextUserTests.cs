using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Project.DAL.Tests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task CreateNewUserById()
    {
        var userEntity = new UserEntity
        {
            Id = new Guid(),
            Name = "Anton",
            Surname = "Bernolák",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };

        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualUser = await dbx.Users.SingleAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualUser);
    }
    [Fact]
    public async Task CreateNewUserByPhotoUrl()
    {
        var userEntity = new UserEntity
        {
            Id = new Guid(),
            Name = "Anton",
            Surname = "Bernolák",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };

        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualUser = await dbx.Users.SingleAsync(i => i.PhotoUrl == userEntity.PhotoUrl);
        DeepAssert.Equal(userEntity, actualUser);
    }


    [Fact]
    public async Task DeleteUserById()
    {
        var userEntity = new UserEntity
        {
            Id = new Guid(),
            Name = "Anton",
            Surname = "Bernolák",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };

        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        ProjectDbContextSUT.Remove(ProjectDbContextSUT.Users.Single(i => i.Id == userEntity.Id));
        await ProjectDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ProjectDbContextSUT.Users.AnyAsync(i => i.Id == userEntity.Id));
    }

    [Fact]
    public async Task DeleteUserByName()
    {
        var userEntity = new UserEntity
        {
            Id = new Guid(),
            Name = "Anton",
            Surname = "Bernolák",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };

        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        ProjectDbContextSUT.Remove(ProjectDbContextSUT.Users.Single(i => i.Id == userEntity.Id));
        await ProjectDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ProjectDbContextSUT.Users.AnyAsync(i => i.Name == userEntity.Name));
    }
}




