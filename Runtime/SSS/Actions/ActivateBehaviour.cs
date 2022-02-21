//-----------------------------------------------------------------------
// <copyright file="ActivateBehaviour.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using UnityEngine;

    public sealed class ActivateBehaviour : Action
    {
#pragma warning disable 0649
        [SerializeField] private Behaviour[] targets;
#pragma warning restore 0649

        public override string DisplayName => "Activate Behaviour";

        public override void StateStarted()
        {
        }

        public override void StateUpdated(float progress)
        {
            if (progress > 0.0f && this.targets?.Length > 0)
            {
                for (int i = 0; i < this.targets.Length; i++)
                {
                    if (this.targets[i].enabled == false)
                    {
                        this.targets[i].enabled = true;
                    }
                }
            }
        }
    }
}

#endif
