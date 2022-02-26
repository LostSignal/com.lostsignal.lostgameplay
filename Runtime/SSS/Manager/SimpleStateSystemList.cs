//-----------------------------------------------------------------------
// <copyright file="SimpleStateSystemList.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public sealed class SimpleStateSystemList : ProcessList<SimpleStateSystemListItem>
    {
        private CameraState cameraState;
        private float deltaTime;

        public SimpleStateSystemList(string name, int capacity) : base(name, capacity)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnBeforeProcess()
        {
            base.OnBeforeProcess();

            this.cameraState = CameraManager.Instance.CameraState;
            this.deltaTime = Time.deltaTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(ref SimpleStateSystemListItem item)
        {
            // TODO [bgish]: If this SSS is really far away, only update every 1/2/3/4 frames

            item.DeltaTime += this.deltaTime;

            if (item.DontUpdateIfNotVisible)
            {
                if (item.IsStatic == false)
                {
                    item.Position = item.Transform.position;
                }

                if (this.cameraState.IsInView(item.Position) == false)
                {
                    return;
                }
            }

            item.SimpleStateSystem.UpdateState(item.DeltaTime);
            item.DeltaTime = 0.0f;
        }
    }
}
