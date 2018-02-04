using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        protected Animator actorAnimator;
        protected Transform actorTransform;

        public abstract void OnTrigger(PointerEventData pointerData);
        public abstract void OnChanneling(PointerEventData pointerData);
        public abstract void OnStop(PointerEventData pointerData);

        private MonoBehaviour coroutineHost;

        public virtual void Initialize(GameObject actor)
        {
            GetActorReferences(actor);
        }

        protected virtual void GetActorReferences(GameObject actor)
        {
            coroutineHost = actor.GetComponent<MonoBehaviour>();
            actorAnimator = actor.GetComponent<Animator>();
            actorTransform = actor.transform;
        }

        protected Coroutine StartCoroutine(IEnumerator routine)
        {
            return coroutineHost.StartCoroutine(routine);
        }
    }
}    
