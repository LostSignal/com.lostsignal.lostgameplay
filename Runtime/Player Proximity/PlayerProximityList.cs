//-----------------------------------------------------------------------
// <copyright file="PlayerProximityList.cs" company="Lost Signal">
//     Copyright (c) Lost Signal. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public sealed class PlayerProximityList : ProcessList<PlayerProximityItem>
    {
        private Vector3 playerPosition;

        public PlayerProximityList(string name, int capacity)
            : base(name, capacity)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnBeforeProcess()
        {
            base.OnBeforeProcess();

            this.playerPosition = ActorManager.Instance.MainPlayerPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(ref PlayerProximityItem item)
        {
            if (item.IsDynamic)
            {
                item.WorldToLocal = item.Transform.worldToLocalMatrix;
            }

            bool isInside = item.Area.IsInside(item.WorldToLocal, this.playerPosition);

            if (item.IsInitialized == false)
            {
                item.IsInitialized = true;
                item.IsInProximity = isInside;
                item.PlayerProximity.UpdateState(isInside);
            }
            else if (item.IsInProximity != isInside)
            {
                item.IsInProximity = isInside;
                item.PlayerProximity.UpdateState(isInside);
            }
        }
    }
}

#endif
