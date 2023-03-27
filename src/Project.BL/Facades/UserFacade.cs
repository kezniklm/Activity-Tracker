using Project.BL.Facades.Interfaces;
using Project.BL.Mappers;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.DAL.Mappers;
using Project.DAL.UnitOfWork;

namespace Project.BL.Facades;

public class UserFacade : FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>,
    IUserFacade
{
    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

}
