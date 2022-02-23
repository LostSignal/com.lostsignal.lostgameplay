//-----------------------------------------------------------------------
// <copyright file="IStart.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    public interface IUpdate
    {
        int Order { get; }

        void OnUpdate(float deltaTime);
    }
}
