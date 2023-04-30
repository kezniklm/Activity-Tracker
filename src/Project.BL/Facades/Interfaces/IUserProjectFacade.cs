using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Facades.Interfaces;

public interface IUserProjectFacade : IFacadeBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>
{
    public Task<IEnumerable<ProjectListModel>?> DisplayProjectsOfUser(Guid userId);
    public Task<IEnumerable<ProjectListModel>?> DoNotDisplayProjectsOfUser(Guid userId);
}
