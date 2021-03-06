//-----------------------------------------------------------------------
// <copyright file="DissonancePlayerTracker.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.DissonanceIntegration
{
    using Lost.Networking;
    using UnityEngine;

#if USING_DISSONANCE
    public class DissonancePlayerTracker : MonoBehaviour, Dissonance.IDissonancePlayer
#else
    public class DissonancePlayerTracker : MonoBehaviour
#endif
    {
#pragma warning disable 0649
        [SerializeField] private NetworkIdentity networkIdentity;
#pragma warning disable 0649

        public bool IsTracking { get; private set; }

        public string PlayerId { get; private set; }

        public Vector3 Position
        {
            get => this.transform.position;
        }

        public Quaternion Rotation
        {
            get => this.transform.rotation;
        }

#if USING_DISSONANCE

        public Dissonance.NetworkPlayerType Type
        {
            get => this.networkIdentity.IsOwner ? Dissonance.NetworkPlayerType.Local : Dissonance.NetworkPlayerType.Remote;
        }

        public Dissonance.VoicePlayerState PlayerState { get; private set; }

#endif

        private void OnValidate()
        {
            this.AssertGetComponent(ref this.networkIdentity);
        }

        private void Awake()
        {
            this.OnValidate();
        }

#if USING_DISSONANCE

        private void OnEnable()
        {
            this.StartCoroutine(Coroutine());

            System.Collections.IEnumerator Coroutine()
            {
                if (this.IsTracking)
                {
                    yield break;
                }

                while (this.PlayerId == null && NetworkingManager.Instance && DissonanceManager.Instance)
                {
                    var userInfo = NetworkingManager.Instance.GetUserInfo(this.networkIdentity.OwnerId);

                    if (userInfo != null)
                    {
                        this.PlayerId = userInfo.UserHexId;
                        this.IsTracking = true;

                        DissonanceManager.Instance.DissonanceComms.TrackPlayerPosition(this);
                    }

                    yield return null;
                }

                while (this.PlayerState == null)
                {
                    this.PlayerState = DissonanceManager.Instance.DissonanceComms.FindPlayer(this.PlayerId);
                    yield return null;
                }
            }
        }

        private void OnDisable()
        {
            if (this.IsTracking)
            {
                this.IsTracking = false;

                if (DissonanceManager.Instance)
                {
                    DissonanceManager.Instance.DissonanceComms.StopTracking(this);
                }
            }
        }

#endif
    }
}

#endif
