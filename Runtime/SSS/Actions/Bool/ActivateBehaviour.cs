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

    public sealed class ActivateBehaviour : ActionT<Behaviour, bool>
    {
        public override string DisplayName => "Activate Behaviour";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool GetCurrentValue() => (this.Target != null) ? this.Target.enabled : false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool GetDesiredValue(float progress) => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SetValue(bool newValue) => this.Target.enabled = newValue;
    }
}

#endif
