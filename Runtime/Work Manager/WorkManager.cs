//-----------------------------------------------------------------------
// <copyright file="WorkManager.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    using UnityEngine;

    public sealed class WorkManager : SingletonMonoBehaviour<WorkManager>, IName, IAwake, IUpdate
    {
#pragma warning disable 0649
        [SerializeField] private double maxRuntimeInMilliseconds = 0.5f;
#pragma warning restore 0649

        private Queue<IWork> workQueue = new Queue<IWork>(1000);

        public string Name => "Work Manager";

        public int UpdateOrder => 3;

        public void OnAwake()
        {
            this.workQueue.OnGrow += () => Debug.LogError("Work Manager Queue Grew!");
        }

        public void OnUpdate(float deltaTime)
        {
            double endTime = Time.realtimeSinceStartupAsDouble + (this.maxRuntimeInMilliseconds / 1000.0);

            while (this.workQueue.Count > 0)
            {
                // TODO [bgish]: Do a bunch of safty checks and limit the time
                var work = this.workQueue.Dequeue();
                work.DoWork();


                if (Time.realtimeSinceStartupAsDouble > endTime)
                {
                    break;
                }
            }
        }

        public void QueueWork(IWork work)
        {
            this.workQueue.Enqueue(work);
        }

        public void QueueImportantWork(IWork work)
        {
            this.workQueue.EnqueueFront(work);
        }

        private void Awake() => ActivationManager.Register(this);
    }
}
