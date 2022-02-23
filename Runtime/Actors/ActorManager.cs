//-----------------------------------------------------------------------
// <copyright file="ActorManager.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class ActorManager : MonoBehaviour, IUpdate
    {
        private static ActorManager instance;
        private Transform mainCameraTransform;
        private Vector3 mainPlayerPosition;
        
        public static ActorManager Instance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (instance == null)
                {
                    instance = SingletonUtil.CreateSingleton<ActorManager>("Actor Manager");
                }

                return instance;
            }
        }

        public int Order => 0;

        public Vector3 MainPlayerPosition
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.mainPlayerPosition;
        }

        public void OnUpdate(float deltaTime)
        {
            if (this.mainCameraTransform == null)
            {
                this.mainCameraTransform = Camera.main.transform;
            }
            
            this.mainPlayerPosition = this.mainCameraTransform.position;
        }

        private void Awake() => ActivationManager.Register(this);
    }
}
