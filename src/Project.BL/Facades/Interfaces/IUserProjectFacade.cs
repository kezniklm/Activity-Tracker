using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Facades.Interfaces;
public interface IUserProjectFacade : IFacadeBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>
{
}
