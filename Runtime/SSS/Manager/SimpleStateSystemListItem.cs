//-----------------------------------------------------------------------
// <copyright file="SimpleStateSystemListItem.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using UnityEngine;

namespace Lost
{
    public struct SimpleStateSystemListItem
    {
        public SimpleStateSystem SimpleStateSystem;
        public Transform Transform;
        public Vector3 Position;
        public bool DontUpdateIfNotVisible;
        public bool IsStatic;
        public float DeltaTime;
    }
}
