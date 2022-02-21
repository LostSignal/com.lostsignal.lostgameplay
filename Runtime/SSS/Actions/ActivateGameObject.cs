//-----------------------------------------------------------------------
// <copyright file="ActivateGameObject.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using UnityEngine;

    public sealed class ActivateGameObject : Action
    {
#pragma warning disable 0649
        [SerializeField] private GameObject target;
#pragma warning restore 0649

        public override string DisplayName => "Activate GameObject";

        public override void StateStarted()
        {
        }

        public override void StateUpdated(float progress)
        {
            if (progress > 0.0f)
            {
                if (this.target.activeSelf == false)
                {
                    this.target.SetActive(true);
                }
            }
        }
    }
}

#endif
