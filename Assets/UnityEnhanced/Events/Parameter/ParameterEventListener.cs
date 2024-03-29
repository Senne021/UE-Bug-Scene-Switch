﻿// ----------------------------------------------------------------------------
// Based on Work from Ryan Hipple, Unite 2017 - Game Architecture with Scriptable Objects
// ----------------------------------------------------------------------------

using UE.Instancing;
using UnityEngine;
using UnityEngine.Events;

namespace UE.Events
{
    public abstract class ParameterEventListener<T, TS> : InstanceObserver where TS : ParameterEvent<T, TS>
    {
        protected abstract ParameterEvent<T, TS> GenericEvent { get; }
        protected abstract UnityEvent<T> GenericResponse { get; }

        [Tooltip("When this is checked, the listener will still work when the game object is disabled.")]
        public bool persistent;

        protected virtual void OnEnable()
        {
            if (!persistent) GenericEvent.AddListener(this, Key);
        }

        protected virtual void OnDisable()
        {
            if (!persistent) GenericEvent.RemoveListener(this, Key);
        }

        protected virtual void Awake()
        {
            if (persistent) GenericEvent.AddListener(this, Key);
        }

        protected virtual void OnDestroy()
        {
            if (persistent) GenericEvent.RemoveListener(this, Key);
        }

        /// <summary>
        /// This is called when the listened event gets raised.
        /// </summary>
        public virtual void OnEventRaised(T value)
        {
            GenericResponse.Invoke(value);
        }

        public override IInstanciable Target =>  GenericEvent;
    }
}