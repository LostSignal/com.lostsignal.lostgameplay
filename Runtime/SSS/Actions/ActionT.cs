//-----------------------------------------------------------------------
// <copyright file="ActionT.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost.SSS
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public abstract class ActionT<TTarget, TValue> : Action
        where TTarget : class
    {
#pragma warning disable 0649
        [SerializeField] private TTarget target;
#pragma warning restore 0649

        private EqualityComparer<TValue> comparer;
        private TValue initialValue;
        private TValue currentValue;

        protected TTarget Target
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.target;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract TValue GetCurrentValue();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract TValue GetDesiredValue(float progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void SetValue(TValue newValue);

        public override void StateStarted()
        {
            if (this.target == null)
            {
                Debug.LogWarning($"SSS Action \"{this.Description}\" has a null target!");
                return;
            }

            if (this.comparer == null)
            {
                this.comparer = EqualityComparer<TValue>.Default;
            }

            this.initialValue = this.currentValue = this.GetCurrentValue();
        }

        protected override void UpdateProgress(float progress)
        {
            if (this.target == null)
            {
                return;
            }

            var desiredValue = progress > 0.0f ? this.GetDesiredValue(progress) : this.initialValue;
            
            if (this.comparer.Equals(this.currentValue, desiredValue) == false)
            {
                this.currentValue = desiredValue;
                this.SetValue(this.currentValue);
            }
        }
    }
}
