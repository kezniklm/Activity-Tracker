using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Facades.Interfaces;

public interface IProjectFacade : IFacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
}
