//-----------------------------------------------------------------------
// <copyright file="TriggerList.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public sealed class TriggerList : ProcessList<TriggerItem>
    {
        private Vector3 playerPosition;

        public TriggerList(string name, int capacity) : base(name, capacity)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnBeforeProcess()
        {
            base.OnBeforeProcess();

            this.playerPosition = ActorManager.Instance.MainPlayerPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(ref TriggerItem item)
        {
            if (item.IsDynamic)
            {
                item.WorldToLocal = item.Transform.worldToLocalMatrix;
            }

            bool isInside = item.Area.IsInside(item.WorldToLocal, this.playerPosition);

            if (item.IsInitialized == false)
            {
                item.IsInitialized = true;
                item.HasEntered = isInside;
                item.Trigger.UpdateState(isInside);
            }
            else if (item.HasEntered != isInside)
            {
                item.HasEntered = isInside;
                item.Trigger.UpdateState(isInside);
            }
        }
    }
}
