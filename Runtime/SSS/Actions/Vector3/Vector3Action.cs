//-----------------------------------------------------------------------
// <copyright file="Vector3Action.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost.SSS
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public abstract class Vector3Action<TTarget> : ActionT<TTarget, Vector3>
        where TTarget : class
    {
        #pragma warning disable 0649
        [SerializeField] private Vector3 endValue;
        [SerializeField] private AnimationCurve curve;
        #pragma warning restore 0649

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Vector3 GetDesiredValue(float progress)
        {
            return Vector3.Lerp(this.InitialValue, this.endValue, this.curve.Evaluate(progress));
        }
    }
}
