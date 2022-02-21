//-----------------------------------------------------------------------
// <copyright file="SimpleStateSystem.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost
{
    using System.Collections.Generic;
    using SSS;
    using UnityEngine;

    ////
    //// TODO [bgish]: Add option to move SSS object into different update queues at different distances
    //// TODO [bgish]: Add option to not update when not visible to player
    ////
    public class SimpleStateSystem : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private List<State> states;
#pragma warning restore 0649

        public List<State> States => this.states;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Called By Unity")]
        private void OnValidate()
        {
            // Make sure states are valid
            if (this.states == null)
            {
                this.states = new List<State>();
            }

            // Make sure all states have valid actions
            foreach (var state in states)
            {
                if (state.Actions == null)
                {
                    state.Actions = new List<Action>();
                }

                foreach (var action in state.Actions)
                {
                    if (action != null)
                    {
                        action.OnValidate();
                    }
                    else
                    {
                        Debug.LogError($"SimpleStateSystem {this.name} has null Action", this);
                    }
                }
            }
        }
    }
}

#endif
