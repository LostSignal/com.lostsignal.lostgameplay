//-----------------------------------------------------------------------
// <copyright file="GameObjectStateAction.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    ////
    //// NOTE [bgish]: SSS has optimization to not update if not in view, or far away.  If mustUpdateEveryFrame is on though, that will be ignored.
    ////
    [Serializable]
    public class State
    {
#pragma warning disable 0649
        [SerializeField] private string name;
        [SerializeField] private bool repeat;
        [SerializeField] private bool mustUpdateEveryFrame;
        [SerializeReference] private List<Action> actions;
#pragma warning restore 0649

        public string Name
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.name;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => this.name = value;
        }

        public bool Repeat
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.repeat;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => this.repeat = value;
        }

        public bool MustUpdateEveryFrame
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.mustUpdateEveryFrame;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => this.mustUpdateEveryFrame = value;
        }

        public List<Action> Actions
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.actions;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => this.actions = value;
        }
    }
}

#endif
