//-----------------------------------------------------------------------
// <copyright file="TriggerItem.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using UnityEngine;

    public struct TriggerItem
    {
        public Matrix4x4 WorldToLocal;
        public Area Area;
        public bool HasEntered;
        public bool IsDynamic;
        public bool IsInitialized;
        public Trigger Trigger;
        public Transform Transform;
    }
}
