//-----------------------------------------------------------------------
// <copyright file="GameObjectStateAction.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost.SSS
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Serializable]
    public abstract class Action
    {
#pragma warning disable 0649
        [SerializeField] private string description;
        [SerializeField] private float delayBeforeStart;
#pragma warning restore 0649

        public string Description => this.description;

        public abstract string DisplayName { get; }

        public float DelayBeforeStart
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.delayBeforeStart;
        }

        public virtual float Duration
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.delayBeforeStart;
        }

        public virtual void OnValidate()
        {
        }

        public abstract void StateStarted();

        public abstract void StateUpdated(float progress);
    }
}
