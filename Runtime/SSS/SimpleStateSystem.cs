//-----------------------------------------------------------------------
// <copyright file="SimpleStateSystem.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using SSS;
    using UnityEngine;

    ////
    //// TODO [bgish]: Add option to move SSS object into different update queues at different distances
    //// TODO [bgish]: Add option to not update when not visible to player
    ////
    public class SimpleStateSystem : MonoBehaviour, IAwake
    {
        #pragma warning disable 0649
        [SerializeField] private bool runDefaultStateOnStartup = true;
        [SerializeField] private bool dontUpdateIfNotVisible = true;
        [SerializeField] private bool isStatic = true;
        [SerializeField] private Transform simpleStateTransform;
        [SerializeField] private List<State> states;
        #pragma warning restore 0649

        private State currentState;
        private bool isSubscribed;
        private bool isEnabled;

        public bool RunDefaultStateOnStartup
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.runDefaultStateOnStartup;
        }

        public bool DontUpdateIfNotVisible
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.dontUpdateIfNotVisible;
        }

        public bool IsStatic
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.isStatic;
        }

        public Transform Transform
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.simpleStateTransform;
        }

        public List<State> States 
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.states;
        }

        public void OnAwake()
        {
            if (this.runDefaultStateOnStartup)
            {
                if (this.states?.Count > 0)
                {
                    this.SetState(this.states[0].Name);
                }
                else
                {
                    Debug.LogWarning($"SimpleStateSystem {this.name} has no states.", this);
                }
            }
        }

        public void UpdateState(float deltaTime)
        {
            if (this.currentState == null || this.isEnabled == false)
            {
                return;
            }

            this.currentState.UpdateState(deltaTime);

            if (this.currentState.IsStateFinished)
            {
                this.UnSubscribeForUpdate();
            }
        }

        // TODO [bgish]: This needs to be completely redone, it's only making sure SSS system is working
        public void SetState(string stateName)
        {
            if (string.IsNullOrWhiteSpace(stateName))
            {
                Debug.LogError($"SimpleStateSystem.SetState got invalid stateName!", this);
                return;
            }

            if (this.currentState != null && this.currentState.Name == stateName)
            {
                // Already in this state
                return;
            }

            this.currentState = null;

            // Finding the state by name
            for (int i = 0; i < this.states.Count; i++)
            {
                if (this.states[i].Name == stateName)
                {
                    this.currentState = this.states[i];
                    break;
                }
            }

            if (this.currentState != null)
            {
                this.currentState.StateStarted();
                this.SubscribeForUpdate();
            }
            else
            {
                Debug.LogError($"SimpleStateSystem.SetState got unknown stateName {stateName}!", this);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SubscribeForUpdate()
        {
            if (this.isSubscribed == false)
            {
                SimpleStateSystemManager.Instance.Register(this);
                this.isSubscribed = true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnSubscribeForUpdate()
        {
            if (this.isSubscribed)
            {
                SimpleStateSystemManager.Instance.Unregister(this);
                this.isSubscribed = false;
            }
        }

        private void OnEnable()
        {
            this.isEnabled = true;
        }

        private void OnDisable()
        {
            this.isEnabled = false;
        }

        private void OnDestroy()
        {
            if (this.isSubscribed)
            {
                this.UnSubscribeForUpdate();
            }
        }
                
        private void Awake() => ActivationManager.Register(this);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Called By Unity")]
        private void OnValidate()
        {
            if (this.simpleStateTransform == null)
            {
                this.simpleStateTransform = this.transform;
                EditorUtil.SetDirty(this);
            }

            // Make sure states are valid
            if (this.states == null)
            {
                this.states = new List<State>();
                EditorUtil.SetDirty(this);
            }

            // Make sure all states have valid actions
            foreach (var state in states)
            {
                if (state.Actions == null)
                {
                    state.Actions = new List<Action>();
                    EditorUtil.SetDirty(this);
                }

                bool needsUpdating = false;
                foreach (var action in state.Actions)
                {
                    if (action != null)
                    {
                        needsUpdating = needsUpdating | action.OnValidate();
                    }
                    else
                    {
                        Debug.LogError($"SimpleStateSystem {this.name} has null Action", this);
                    }
                }

                if (needsUpdating)
                {
                    EditorUtil.SetDirty(this);
                }
            }
        }
    }
}

#endif
