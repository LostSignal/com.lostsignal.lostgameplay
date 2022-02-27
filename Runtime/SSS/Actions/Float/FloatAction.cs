//-----------------------------------------------------------------------
// <copyright file="FloatAction.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost.SSS
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public abstract class FloatAction<TTarget> : ActionT<TTarget, float>
        where TTarget : class
    {
        #pragma warning disable 0649
        [SerializeField] private float endValue;
        [SerializeField] private AnimationCurve curve;
        #pragma warning restore 0649

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override float GetDesiredValue(float progress)
        {
            return Mathf.Lerp(this.InitialValue, this.endValue, this.curve.Evaluate(progress));
        }
    }
}
