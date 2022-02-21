//-----------------------------------------------------------------------
// <copyright file="GameObjectStateAction.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost.SSS
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    
    public enum ActionTimeMode
    {
        Instant,
        Overtime,
    }

    public enum ActionStartMode
    {
        CurrentValue,
        FixedValue,
    }

    ////
    //// NOTE [bgish]: If startMode is set to CurrentValue, then it should mark it readonly and updated every frame?
    ////
    [Serializable]
    public abstract class ActionOverTime<TTarget, TValue> : Action
    {
#pragma warning disable 0649
        [FoldoutGroup("Settings", true)]
        [SerializeField] private ActionTimeMode timeMode;

        [FoldoutGroup("Settings")]
        [SerializeField] private ActionStartMode startMode;

        [SerializeField] private TTarget[] targets;

        [ShowIf("startMode", ActionStartMode.FixedValue)]
        [SerializeField] private TValue startValue;

        [ShowIf("timeMode", ActionTimeMode.Overtime)]
        [SerializeField] public AnimationCurve animationCurve;

        [SerializeField] private TValue endValue;

        // Add a minimum speed?

        [HideInInspector]
        [SerializeField] public float duration;
#pragma warning restore 0649

        private TValue start;
        private TValue end;

        protected abstract TValue GetCurrentValue();

        protected abstract TValue Lerp(TValue start, TValue end, float progress);        

        protected abstract void SetCurrentValue(TTarget target, TValue newValue);

        public override void StateStarted()
        {
            this.start = this.startMode == ActionStartMode.CurrentValue ? this.GetCurrentValue() :
                         this.startMode == ActionStartMode.FixedValue ? this.startValue :
                         default;
            
            this.end = this.endValue;
        }

        public override void StateUpdated(float progress)
        {
            if (progress < 0.0f)
            {
                return;
            }

            bool hasTargets = this.targets?.Length > 0;

            if (hasTargets == false)
            {
                return; // Early Out
            }
            else if (this.timeMode == ActionTimeMode.Instant)
            {
                for (int i = 0; i < this.targets.Length; i++)
                {
                    this.SetCurrentValue(this.targets[i], this.end);
                }
            }
            else if (this.timeMode == ActionTimeMode.Overtime)
            {
                for (int i = 0; i < this.targets.Length; i++)
                {
                    this.SetCurrentValue(this.targets[i], this.Lerp(this.start, this.end, this.animationCurve.Evaluate(progress)));
                }
            }
            else
            {
                Debug.LogError($"Unknown Mode {this.timeMode} Found");
            }
        }

        public override void OnValidate()
        {
            base.OnValidate();

            // Making sure an animation curve exists
            if (this.animationCurve == null)
            {
                this.animationCurve = new AnimationCurve(
                    new Keyframe
                    {
                        time = 0,
                        value = 0
                    },
                    new Keyframe
                    {
                        time = 1,
                        value = 1
                    });
            }

            // Looking at the keys to get the duration
            var keys = this.animationCurve.keys;
            float animationCurveDuration = keys[keys.Length - 1].time;

            if (this.duration != animationCurveDuration)
            {
                this.duration = animationCurveDuration;
            }
        }

        public override float Duration
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.DelayBeforeStart + this.duration;
        }

        public AnimationCurve AnimationCurve => this.animationCurve;
    }
}

#endif
