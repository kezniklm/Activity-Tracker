// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.ObjectModel;

namespace Project.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required string ActivityType { get; set; }
    public string? Description { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id= Guid.Empty,
        ActivityType= string.Empty,
        Description = string.Empty,
        Start= DateTime.MinValue,
        End= DateTime.MinValue,
    };
}
