//-----------------------------------------------------------------------
// <copyright file="SimpleStateSystemTriggerListener.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using UnityEngine;

    public class SimpleStateSystemTriggerListener : MonoBehaviour, ITriggerListener
    {
        #pragma warning disable 0649
        [SerializeField] private SimpleStateSystem simpleStateSystem;
        [SerializeField] private Trigger trigger;
        [SerializeField] private string onEnterTriggerStateName;
        [SerializeField] private string onExitTriggerStateName;
        #pragma warning restore 0649

        public void OnPlayerEnterTrigger()
        {
            this.simpleStateSystem.SetState(this.onEnterTriggerStateName);
        }

        public void OnPlayerExitTrigger()
        {
            this.simpleStateSystem.SetState(this.onExitTriggerStateName);
        }

        private void OnValidate()
        {
            if (this.simpleStateSystem == null)
            {
                var sss = this.GetComponent<SimpleStateSystem>();

                if (sss != null)
                {
                    this.simpleStateSystem = sss; 
                    EditorUtil.SetDirty(this);
                }
            }
            
            if (this.trigger == null)
            {
                var trigger = this.GetComponent<Trigger>();

                if (trigger != null)
                {
                    this.trigger = trigger;
                    EditorUtil.SetDirty(this);
                }
            }

            if (this.trigger != null)
            {
                this.trigger.AddListener(this);
            }
        }
    }
}
