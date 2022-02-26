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
        [SerializeField] private bool isLoopingState;
#pragma warning restore 0649

        public string Description => this.description;

        public abstract string DisplayName { get; }

        public bool IsLoopingState
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.isLoopingState;
        }

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

        public virtual bool OnValidate()
        {
            return false;
        }

        public abstract void StateStarted();

        public virtual void Update(float currentTime)
        {
            if (currentTime < this.delayBeforeStart)
            {
                this.UpdateProgress(0.0f);
            }
            else
            {
                this.UpdateProgress(1.0f);
            }
        }

        protected abstract void UpdateProgress(float progress);
    }
}
