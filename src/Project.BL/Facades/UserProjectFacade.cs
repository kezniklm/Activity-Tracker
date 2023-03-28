using System;
using System.Threading.Tasks;
using Project.BL.Facades.Interfaces;
using Project.BL.Mappers;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.DAL.Mappers;
using Project.DAL.Repositories;
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
