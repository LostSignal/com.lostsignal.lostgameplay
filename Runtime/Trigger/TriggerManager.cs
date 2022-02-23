//-----------------------------------------------------------------------
// <copyright file="TriggerManager.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class TriggerManager : MonoBehaviour, IUpdate
    {
        private static TriggerManager instance;

        private TriggerList triggers  = new TriggerList("Triggers", 100);
        
        public static TriggerManager Instance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (instance == null)
                {
                    instance = SingletonUtil.CreateSingleton<TriggerManager>("Trigger Manager");
                }

                return instance;
            }
        }

        public int Order => 2;

        public void OnUpdate(float deltaTime)
        {
            this.triggers.RunAll();
        }

        public void AddTrigger(Trigger trigger)
        {
            var triggerTransform = trigger.TriggerTransform;

            this.triggers.Add(
                trigger.GetInstanceID(), 
                new TriggerItem
                {
                    Area = trigger.Area,
                    HasEntered = false,
                    IsInitialized = false,
                    IsDynamic = trigger.IsDynamic,
                    Transform = triggerTransform,
                    Trigger = trigger,
                    WorldToLocal = triggerTransform.worldToLocalMatrix,
                },
                trigger);
        }

        public void RemoveTrigger(Trigger trigger)
        {
            this.triggers.Remove(trigger.GetInstanceID());
        }

        private void Awake() => ActivationManager.Register(this);
    }
}
