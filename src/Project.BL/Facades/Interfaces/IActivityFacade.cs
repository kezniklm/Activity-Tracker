using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Facades;
public interface IActivityFacade : IFacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
}
