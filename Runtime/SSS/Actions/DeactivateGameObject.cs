//-----------------------------------------------------------------------
// <copyright file="DeactivateGameObject.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using UnityEngine;

    public sealed class DeactivateGameObject : Action
    {
#pragma warning disable 0649
        [SerializeField] private GameObject target;
#pragma warning restore 0649

        public override string DisplayName => "Deactivate GameObject";

        public override void StateStarted()
        {
        }

        protected override void UpdateProgress(float progress)
        {
            if (progress > 0.0f)
            {
                if (this.target.activeSelf)
                {
                    this.target.SetActive(false);
                }
            }
        }
    }
}

#endif
