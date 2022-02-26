//-----------------------------------------------------------------------
// <copyright file="ExecuteUnityEvent.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using UnityEngine;
    using UnityEngine.Events;

    public sealed class ExecuteUnityEvent : Action
    {
#pragma warning disable 0649
        [Space]
        [SerializeField] private UnityEvent unityEvent;
#pragma warning restore 0649

        private bool hasUnityEventBeenCalled = false;

        public override string DisplayName => "Execute UnityEvent";

        public override void StateStarted()
        {
            this.hasUnityEventBeenCalled = false;
        }

        protected override void UpdateProgress(float progress)
        {
            if (progress > 0.0f && this.hasUnityEventBeenCalled == false)
            {
                this.hasUnityEventBeenCalled = true;
                this.unityEvent.SafeInvoke();
            }
        }
    }
}

#endif
