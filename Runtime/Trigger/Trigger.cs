//-----------------------------------------------------------------------
// <copyright file="Trigger.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class Trigger : MonoBehaviour, IWork
    {
        private enum State
        {
            Uninitialized,
            InTrigger,
            OutOfTrigger,
        }

#pragma warning disable 0649
        [ReadOnly] [SerializeField] private Transform triggerTransform;
        [SerializeField] private Area area;
        [SerializeField] private bool isDynamic;
        [SerializeField] List<Component> listeners;
#pragma warning restore 0649

        private State currentState;
        private State newState;

        public Transform TriggerTransform
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.triggerTransform;
        }

        public Area Area
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.area;
        }

        public bool IsDynamic
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.isDynamic;
        }

        public void UpdateState(bool isInTrigger)
        {
            this.newState = isInTrigger ? State.InTrigger : State.OutOfTrigger;

            if (this.currentState != this.newState)
            {
                WorkManager.Instance.QueueImportantWork(this);
            }            
        }

        public void DoWork()
        {
            if (this.currentState != this.newState)
            {
                this.currentState = this.newState;

                if (this.currentState == State.InTrigger)
                {
                    this.OnEnterTrigger();
                }
                else
                {
                    this.OnExitTrigger();
                }
            }
        }

        public void OnEnterTrigger()
        {
            if (this.listeners?.Count > 0)
            {
                for (int i = 0; i < this.listeners.Count; i++)
                {
                    (this.listeners[i] as ITriggerListener)?.OnPlayerEnterTrigger();
                }
            }
        }

        public void OnExitTrigger()
        {
            if (this.listeners?.Count > 0)
            {
                for (int i = 0; i < this.listeners.Count; i++)
                {
                    (this.listeners[i] as ITriggerListener)?.OnPlayerExitTrigger();
                }
            }
        }

        public void AddListener(ITriggerListener listener)
        {
            if (listener is Component component)
            {
                if (this.listeners == null)
                {
                    this.listeners = new List<Component>();
                }

                if (this.listeners.Contains(component) == false)
                {
                    this.listeners.Add(component);
                    EditorUtil.SetDirty(this);
                }
            }
            else
            {
                Debug.LogError("Trying to add an ITriggerListener that is not also a Component");
            }
        }

        public void RemoveListener(ITriggerListener listener)
        {
            if (listener is Component component)
            {
                if (this.listeners != null && this.listeners.Contains(component))
                {
                    this.listeners.Remove(component);
                    EditorUtil.SetDirty(this);
                }
            }
            else
            {
                Debug.LogError("Trying to remove an ITriggerListener that is not also a Component");
            }
        }

        private void Awake() => ActivationManager.Register(this);

        private void OnEnable()
        {
            TriggerManager.Instance.AddTrigger(this);
        }

        private void OnDisable()
        {
            if (TriggerManager.Instance != null)
            {
                TriggerManager.Instance.RemoveTrigger(this);
                this.UpdateState(false);
            }
        }

        private void OnValidate()
        {
            if (this.triggerTransform == null)
            {
                this.triggerTransform = this.transform;
                EditorUtil.SetDirty(this.gameObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Area.Draw(this.transform, this.area, Color.white.SetA(0.2f));
        }
    }
}
