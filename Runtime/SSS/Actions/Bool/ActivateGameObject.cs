//-----------------------------------------------------------------------
// <copyright file="ActivateGameObject.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public sealed class ActivateGameObject : ActionT<GameObject, bool>
    {
        public override string DisplayName => "Activate GameObject";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool GetCurrentValue() => (this.Target != null) ? this.Target.activeSelf : false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool GetDesiredValue(float progress) => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SetValue(bool newValue) => this.Target.SetActive(newValue);
    }
}

#endif
