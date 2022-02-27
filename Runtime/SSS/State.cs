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

    [Serializable]
    public class State
    {
#pragma warning disable 0649
        [SerializeField] private string name;
        [SerializeField] private bool repeat;
        [SerializeField] private bool mustUpdateEveryFrame;
        [SerializeReference] private List<Action> actions;
#pragma warning restore 0649

        private float currentTime;
        private float stateDuration;
        private bool isStateFinished;

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

        public bool IsStateFinished 
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.isStateFinished;
        }

        public void StateStarted()
        {
            this.currentTime = 0.0f;
            this.isStateFinished = false;

            for (int i = 0; i < this.actions.Count; i++)
            {
                this.actions[i].StateStarted();
                this.stateDuration = this.actions[i].TotalTime;
            }
        }

        public void UpdateState(float deltaTime)
        {
            this.currentTime += deltaTime;

            if (this.repeat)
            {
                while (this.currentTime > this.stateDuration)
                {
                    this.currentTime -= this.stateDuration;
                }
            }

            bool haveAllActionsFinshed = true;

            for (int i = 0; i < this.actions.Count; i++)
            {
                if (this.actions[i].Update(this.currentTime) == false)
                {
                    haveAllActionsFinshed = false;
                }
            }

            if (this.repeat == false && haveAllActionsFinshed)
            {
                this.isStateFinished = true;
            }
        }
    }
}

#endif
