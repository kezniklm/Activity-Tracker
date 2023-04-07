using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Facades.Interfaces;

public interface IActivityFacade : IFacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    Task<IEnumerable<ActivityListModel>> Filter(DateTime start, DateTime end);
    Task<IEnumerable<ActivityListModel>> FilterThisYear();
    Task<IEnumerable<ActivityListModel>> FilterLastMonth();
    Task<IEnumerable<ActivityListModel>> FilterThisMonth();
    Task<IEnumerable<ActivityListModel>> FilterThisWeek();
}
