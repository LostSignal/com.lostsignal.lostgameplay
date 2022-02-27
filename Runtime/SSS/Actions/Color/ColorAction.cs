//-----------------------------------------------------------------------
// <copyright file="ColorAction.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost.SSS
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public abstract class ColorAction<TTarget> : ActionT<TTarget, Color>
        where TTarget : class
    {
        #pragma warning disable 0649
        [SerializeField] private Color endValue;
        [SerializeField] private AnimationCurve curve;
        #pragma warning restore 0649

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Color GetDesiredValue(float progress)
        {
            return Color.Lerp(this.InitialValue, this.endValue, this.curve.Evaluate(progress));
        }
    }
}
