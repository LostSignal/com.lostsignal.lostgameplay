//-----------------------------------------------------------------------
// <copyright file="SimpleStateSystemManager.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Lost
{
    public sealed class SimpleStateSystemManager : SingletonMonoBehaviour<SimpleStateSystemManager>, IName, IUpdate
    {
        private SimpleStateSystemList simpleStateSystems = new SimpleStateSystemList("Simple State Systems", 100);

        public int UpdateOrder => 10;

        public string Name => "SSS Manager";

        public void OnUpdate(float deltaTime)
        {
            this.simpleStateSystems.RunAll();
        }

        public void Register(SimpleStateSystem simpleStateSystem)
        {
            this.simpleStateSystems.Add(
                simpleStateSystem.GetInstanceID(), 
                new SimpleStateSystemListItem
                {
                    SimpleStateSystem = simpleStateSystem,
                    Transform = simpleStateSystem.Transform,
                    Position = simpleStateSystem.Transform.position,
                    DontUpdateIfNotVisible = simpleStateSystem.DontUpdateIfNotVisible,
                    IsStatic = simpleStateSystem.IsStatic,
                    DeltaTime = 0.0f,
                }, 
                simpleStateSystem);
        }

        public void Unregister(SimpleStateSystem simpleStateSystem)
        {
            this.simpleStateSystems.Remove(simpleStateSystem.GetInstanceID());
        }

        private void Awake() => ActivationManager.Register(this);
    }
}
