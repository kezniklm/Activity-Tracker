// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.ObjectModel;

namespace Project.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    public ObservableCollection<ActivityListModel> Activities { get; set; } = new();
    public ObservableCollection<UserProjectListModel> Users { get; set; } = new();

    public static ProjectDetailModel Empty = new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
    };
}
