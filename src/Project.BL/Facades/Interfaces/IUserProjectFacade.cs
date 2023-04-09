using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Facades.Interfaces;
public interface IUserProjectFacade : IFacadeBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>
{
    Task<UserProjectDetailModel> SaveAsync(UserProjectDetailModel model);
}
