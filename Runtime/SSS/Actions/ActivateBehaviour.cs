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
        
        //// [HideInInspector]
        //// [SerializeField] private bool[] initialStates;
#pragma warning restore 0649

        public override string DisplayName => "Activate Behaviour";

        public override void StateStarted()
        {
            //// if (this.targets?.Length > 0)
            //// {
            ////     for (int i = 0; i < this.targets.Length; i++)
            ////     {
            ////         this.initialStates[i] = this.targets[i].enabled;
            ////     }
            // }
        }

        protected override void UpdateProgress(float progress)
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

        //// public override bool OnValidate()
        //// {
        ////     if (this.targets != null && (this.initialStates == null || this.initialStates.Length != this.targets.Length))
        ////     {
        ////         this.initialStates = new bool[this.targets.Length];
        ////         return true;
        ////     }
        //// 
        ////     return false;
        //// }
    }
}

#endif
