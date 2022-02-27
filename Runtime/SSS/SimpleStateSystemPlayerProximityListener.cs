//-----------------------------------------------------------------------
// <copyright file="SimpleStateSystemPlayerProximityListener.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using UnityEngine;

    public class SimpleStateSystemPlayerProximityListener : MonoBehaviour, IPlayerProximityListener
    {
        #pragma warning disable 0649
        [SerializeField] private SimpleStateSystem simpleStateSystem;
        [SerializeField] private PlayerProximity playerProximity;
        [SerializeField] private string onEnterProximityStateName;
        [SerializeField] private string onExitProximityStateName;
        #pragma warning restore 0649

        public void OnPlayerEnterProximity()
        {
            this.simpleStateSystem.SetState(this.onEnterProximityStateName);
        }

        public void OnPlayerExitProximity()
        {
            this.simpleStateSystem.SetState(this.onExitProximityStateName);
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
            
            if (this.playerProximity == null)
            {
                var playerProximity = this.GetComponent<PlayerProximity>();

                if (playerProximity != null)
                {
                    this.playerProximity = playerProximity;
                    EditorUtil.SetDirty(this);
                }
            }

            if (this.playerProximity != null)
            {
                this.playerProximity.AddListener(this);
            }
        }
    }
}
