﻿using System.Collections.Generic;
using System.Linq;

namespace Project.BL.Mappers;

public abstract class
    ModelMapperBase<TEntity, TListModel, TDetailModel> : IModelMapperBase<TEntity, TListModel, TDetailModel>
{
    public abstract TListModel MapToListModel(TEntity? entity);

    public IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity> entities)
        => entities.Select(MapToListModel);

    public abstract TDetailModel MapToDetailModel(TEntity entity);
    public abstract TEntity MapToEntity(TDetailModel model);
}
