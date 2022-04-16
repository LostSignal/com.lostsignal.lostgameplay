//-----------------------------------------------------------------------
// <copyright file="PlayAudioBlock.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost.SSS
{
    using UnityEngine;

    public class PlayAudioBlock : Action
    {
#pragma warning disable 0649
        [SerializeField] private AudioBlock audioBlock;
        [SerializeField] private Transform audioBlockTransform;
#pragma warning restore 0649

        public override string DisplayName => "Play Audio Block";

        private bool hasPlayed;

        public override void StateStarted()
        {
            this.hasPlayed = false;
        }

        protected override void UpdateProgress(float progress)
        {
            if (this.hasPlayed == false && progress > 0.0f)
            {
                this.hasPlayed = true;

                if (this.audioBlock == null)
                {
                    Debug.LogError("PlayAudioBlock Action has no AudioBlock!");
                    return;
                }

                if (this.audioBlockTransform != null)
                {
                    this.audioBlock.Play(this.audioBlockTransform);
                }
                else
                {
                    this.audioBlock.Play();
                }
            }
        }
    }
}
