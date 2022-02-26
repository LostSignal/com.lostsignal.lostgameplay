//-----------------------------------------------------------------------
// <copyright file="TriggerManager.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    public sealed class TriggerManager : SingletonMonoBehaviour<TriggerManager>, IName, IUpdate
    {
        private TriggerList triggers  = new TriggerList("Triggers", 100);
        
        public string Name => "Trigger Manager";
        
        public int UpdateOrder => 2;

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
