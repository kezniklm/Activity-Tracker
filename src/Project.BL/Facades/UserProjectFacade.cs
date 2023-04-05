using Project.BL.Facades.Interfaces;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.DAL.Mappers;
using Project.DAL.UnitOfWork;

namespace Project.BL.Facades;

public class UserProjectFacade :
    FacadeBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel,
        UserProjectEntityMapper>, IUserProjectFacade
{
    public UserProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserProjectModelMapper ingredientAmountModelMapper)
        : base(unitOfWorkFactory, ingredientAmountModelMapper)
    {

    }
}
