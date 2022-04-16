//-----------------------------------------------------------------------
// <copyright file="PlayAudioBlockOnButtonClick.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class PlayAudioBlockOnButtonClick : MonoBehaviour, IValidate, IAwake
    {
#pragma warning disable 0649
        [SerializeField] private AudioBlock audioBlock;
        [SerializeField] private bool playSoundFromButtonPosition;

        [HideInInspector]
        [SerializeField] private Button button;
#pragma warning restore 0649

        public void Validate(List<ValidationError> errors)
        {
            this.AssertNotNull(errors, this.audioBlock, nameof(this.audioBlock));
        }

        public void OnAwake()
        {
            this.button.onClick.AddListener(this.OnButtonClicked);
        }

        private void OnValidate()
        {
            EditorUtil.SetIfNull(this, ref this.button);
        }

        private void Awake() => ActivationManager.Register(this);

        private void OnButtonClicked()
        {
            if (this.audioBlock == null)
            {
                return;
            }
            else if (this.playSoundFromButtonPosition)
            {
                this.audioBlock.Play(this.button.transform.position);
            }
            else
            {
                this.audioBlock.Play();
            }
        }
    }
}

#endif
