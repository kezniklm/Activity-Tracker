// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.ObjectModel;

namespace Project.BL.Models;

public record UserProjectDetailModel : ModelBase
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }

    public static UserProjectDetailModel Empty => new()
    {
        Id= Guid.Empty,
        ProjectId= Guid.Empty,
        UserId= Guid.Empty,
    };
}
