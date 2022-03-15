//-----------------------------------------------------------------------
// <copyright file="PeriodicUpdateManager.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if USING_UNITY_VISUAL_SCRIPTING

namespace Lost
{
    using System;

    public class PeriodicUpdateManager : SingletonMonoBehaviour<PeriodicUpdateManager>, IName
    {
        public string Name => "Periodic Update Manager";

        public void Add(PeriodicUpdateUnit unit)
        {
            throw new NotImplementedException();
        }

        public void Remove(PeriodicUpdateUnit unit)
        {
            throw new NotImplementedException();
        }
    }
}

#endif
