﻿using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Facades.Interfaces;

public interface IActivityFacade : IFacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    Task<IEnumerable<ActivityListModel>> Filter(DateTime start, DateTime end, Guid userId);
    Task<IEnumerable<ActivityListModel>> FilterThisYear(Guid userId);
    Task<IEnumerable<ActivityListModel>> FilterLastMonth(Guid userId);
    Task<IEnumerable<ActivityListModel>> FilterThisMonth(Guid userId);
    Task<IEnumerable<ActivityListModel>> FilterThisWeek(Guid userId);
    public Task<IEnumerable<ActivityListModel?>> GetUserActivitiesNotInProject(Guid userId);
    public Task RemoveActivitiesFromProject(Guid userId, Guid projectId);
}
