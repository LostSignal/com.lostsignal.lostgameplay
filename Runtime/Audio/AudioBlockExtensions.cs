//-----------------------------------------------------------------------
// <copyright file="AudioBlockExtensions.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

using UnityEngine;

namespace Lost
{
    public static class AudioBlockExtensions
    {
        public static void PlayIfNotNull(this AudioBlock audioBlock)
        {
            if (audioBlock)
            {
                audioBlock.Play();
            }
        }

        public static void PlayIfNotNull(this AudioBlock audioBlock, Vector3 position)
        {
            if (audioBlock)
            {
                audioBlock.Play(position);
            }
        }

        public static void PlayIfNotNull(this AudioBlock audioBlock, Transform transform)
        {
            if (audioBlock)
            {
                audioBlock.Play(transform);
            }
        }

        public static void PlayIfNotNull(this AudioBlock audioBlock, Vector3 position, float pitchPercentageOverride, float volumePercentageOverride)
        {
            if (audioBlock)
            {
                audioBlock.Play(position, pitchPercentageOverride, volumePercentageOverride);
            }
        }

        public static void PlayIfNotNull(this AudioBlock audioBlock, Transform transform, float pitchPercentageOverride, float volumePercentageOverride)
        {
            if (audioBlock)
            {
                audioBlock.Play(transform, pitchPercentageOverride, volumePercentageOverride);
            }
        }
    }
}

#endif
