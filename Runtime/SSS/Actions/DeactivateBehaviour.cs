//-----------------------------------------------------------------------
// <copyright file="DeactivateBehaviour.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using UnityEngine;

    public sealed class DeactivateBehaviour : Action
    {
#pragma warning disable 0649
        [SerializeField] private Behaviour target;
#pragma warning restore 0649

        public override string DisplayName => "Deactivate Behaviour";

        public override void StateStarted()
        {
        }

        protected override void UpdateProgress(float progress)
        {
            if (progress > 0.0f)
            {
                if (this.target.enabled)
                {
                    this.target.enabled = false;
                }
            }
        }
    }
}

#endif
