// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Project.DAL.UnitOfWork;
public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}
