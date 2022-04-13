//-----------------------------------------------------------------------
// <copyright file="AudioManager.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class AudioManager : Manager<AudioManager>
    {
        private readonly HashSet<int> audioChannelsIntanceIds = new HashSet<int>();

#pragma warning disable 0649
        [SerializeField] private Transform audioPool;
        [SerializeField] private int audioSourcePoolGrowSize = 10;
        [SerializeField] private List<AudioSource> audioSourcePoolItems;
        [SerializeField] private AudioSource audioSourcePrefab;
        [SerializeField] private List<AudioChannel> audioChannels;
#pragma warning restore 0649

        public override void Initialize()
        {
            // Making sure our pool of audio sources is disabled at startup
            for (int i = 0; i < this.audioSourcePoolItems.Count; i++)
            {
                this.audioSourcePoolItems[i].enabled = false;
            }

            // TODO [bgish]: This should not happen till the player settings are loaded (right now uses LostPlayerPrefs)
            for (int i = 0; i < this.audioChannels.Count; i++)
            {
                this.audioChannelsIntanceIds.Add(this.audioChannels[i].GetInstanceID());
                this.audioChannels[i].Load();
            }

            this.SetInstance(this);
        }

        public void Play(AudioBlock audioBlock)
        {
            this.Play(audioBlock, null, Vector3.zero, false);
        }

        public void Play(AudioBlock audioBlock, Vector3 position)
        {
            this.Play(audioBlock, null, position, true);
        }

        public void Play(AudioBlock audioBlock, Transform transform)
        {
            this.Play(audioBlock, transform, transform.position, true);
        }

        public void SaveAudioSettings()
        {
            foreach (var audioChannel in this.audioChannels)
            {
                audioChannel.Save();
            }
        }

        private void Play(AudioBlock audioBlock, Transform parent, Vector3 position, bool isPositionalAudio)
        {
            AudioChannel channel = audioBlock.AudioChannel;

            if (channel == null)
            {
                Debug.LogError($"AudioBlock {audioBlock.name} failed to play.  It does not have a valid AudioChannel.", audioBlock);
                return;
            }

            if (this.audioChannelsIntanceIds.Contains(channel.GetInstanceID()) == false)
            {
                Debug.LogError($"AudioBlock {audioBlock.name} failed to play because it's AudioChannel {channel.name} Is not registered with the AudioManager.", audioBlock);
                return;
            }

            if (channel.IsMuted || channel.Volume == 0.0f)
            {
                return;
            }

            AudioSource audioSource = this.GetAudioSource();

            // Setting up parenting / positioning
            if (parent != null)
            {
                audioSource.transform.SetParent(parent);
                audioSource.transform.Reset();
            }
            else
            {
                if (isPositionalAudio)
                {
                    audioSource.transform.SetParent(null);
                    audioSource.transform.position = position;
                }
                else
                {
                    audioSource.transform.SetParent(CameraManager.Instance.CameraState.Transform);
                    audioSource.transform.Reset();
                }
            }

            audioSource.spatialBlend = isPositionalAudio ? 1.0f : 0.0f;
            audioSource.clip = audioBlock.GetAudioClip();
            audioSource.pitch = audioBlock.GetPitch();
            audioSource.volume = audioBlock.GetVolume() * channel.Volume;

            // Making sure it's all enabled
            audioSource.gameObject.SetActive(true);
            audioSource.enabled = true;

            // We're done! Play!
            audioSource.Play();
        }

        //// TODO [bgish]: This can be highly optimized.  Picking up where we last left off, or having
        ////               multiiple lists for playing audio sources vs non-playing audio sources.  It
        ////               works for now though, so going with it.
        private AudioSource GetAudioSource()
        {
            int poolCount = this.audioSourcePoolItems.Count;

            for (int i = 0; i < poolCount; i++)
            {
                if (this.audioSourcePoolItems[i].isPlaying == false)
                {
                    return this.audioSourcePoolItems[i];
                }
            }

            // If we got here, then we need to grow the pool
            Debug.LogError("AudioManager Pool Forced to Grow. Please set initial capacity ahead of time so this doesn't happen at runtime.", this);
            this.audioSourcePoolItems.Capacity = this.audioSourcePoolItems.Count + this.audioSourcePoolGrowSize;

            for (int i = 0; i < this.audioSourcePoolGrowSize; i++)
            {
                var poolItem = GameObject.Instantiate(this.audioSourcePrefab, this.audioPool.transform);
                poolItem.enabled = false;
                poolItem.name = BetterStringBuilder.New().Append("Audio Source (").Append(this.audioSourcePoolItems.Count).Append(")").ToString();
                this.audioSourcePoolItems.Add(poolItem);
            }

            return this.audioSourcePoolItems[poolCount];
        }
    }
}

#endif
