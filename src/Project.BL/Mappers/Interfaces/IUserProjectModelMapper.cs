using System;
using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Mappers;

public interface IUserProjectModelMapper
    : IModelMapperBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>
{
    UserProjectListModel MapToListModel(UserProjectDetailModel detailModel);
    UserProjectEntity MapToEntity(UserProjectDetailModel model, Guid id);
    void MapToExistingDetailModel(UserProjectDetailModel existingDetailModel, UserProjectListModel userProjects);
    UserProjectEntity MapToEntity(UserProjectListModel model, Guid id);
}
