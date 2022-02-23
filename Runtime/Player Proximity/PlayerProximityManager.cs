//-----------------------------------------------------------------------
// <copyright file="PlayerProximityManager.cs" company="Lost Signal">
//     Copyright (c) Lost Signal. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost
{
    using UnityEngine;

    public class PlayerProximityManager : MonoBehaviour, IUpdate
    {
        private static PlayerProximityManager instance;

#pragma warning disable 0649
        [SerializeField] private float runAllOverXSeconds = 0.5f;
#pragma warning restore 0649

        private PlayerProximityList playerProximityList = new PlayerProximityList("Player Proximity List", 1000);

        public static PlayerProximityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = SingletonUtil.CreateSingleton<PlayerProximityManager>("Player Proximity Manager");
                }

                return instance;
            }
        }

        public int Order => 1;

        public void Register(PlayerProximity playerProximity)
        {
            Transform playerProximityTransform = playerProximity.ProximityTransform;

            this.playerProximityList.Add(
                playerProximity.GetInstanceID(), 
                new PlayerProximityItem
                {
                    WorldToLocal = playerProximityTransform.worldToLocalMatrix,
                    Area = playerProximity.Area,
                    IsDynamic = playerProximity.IsDynamic,
                    PlayerProximity = playerProximity,
                    Transform = playerProximityTransform,
                    IsInProximity = false,
                },
                playerProximity);
        }

        public void Unregister(PlayerProximity playerProximity)
        {
            this.playerProximityList.Remove(playerProximity.GetInstanceID());
        }

        public void OnUpdate(float deltaTime)
        {
            this.playerProximityList.RunAllOverXSeconds(deltaTime, this.runAllOverXSeconds);
        }

        private void Awake() => ActivationManager.Register(this);
    }
}

#endif