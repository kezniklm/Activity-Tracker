using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Facades.Interfaces;

public interface IUserFacade : IFacadeBase<UserEntity, UserListModel, UserDetailModel>
{
}
