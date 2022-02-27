//-----------------------------------------------------------------------
// <copyright file="ActivateBehaviour.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public sealed class DeactivateBehaviour : ActionT<Behaviour, bool>
    {
        public override string DisplayName => "Deactivate Behaviour";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool GetCurrentValue() => this.Target.enabled;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool GetDesiredValue(float progress) => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SetValue(bool newValue) => this.Target.enabled = newValue;
    }
}

#endif
