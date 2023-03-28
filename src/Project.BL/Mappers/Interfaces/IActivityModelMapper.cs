using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Mappers.Interfaces;
public interface IActivityModelMapper
    : IModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
}
