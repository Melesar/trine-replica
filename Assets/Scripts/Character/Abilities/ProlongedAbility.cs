using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Character.Abilities
{
    public abstract class ProlongedAbility : ScriptableObject
    {
        public bool IsRunning { get; protected set; }

        public event UnityAction onFinished;

        protected GameObject actor;
        private MonoBehaviour coroutineHost;
        
        public abstract void StartAbility(PointerEventData pointerData);
        
        protected Coroutine StartCoroutine(IEnumerator routine)
        {
            return coroutineHost.StartCoroutine(routine);
        }

        public virtual void Initialize(GameObject actor)
        {
            this.actor = actor;
        }

        protected virtual void OnFinished()
        {
            onFinished?.Invoke();
        }
    }
}