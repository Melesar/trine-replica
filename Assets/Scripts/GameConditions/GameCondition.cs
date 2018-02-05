using UnityEngine;
using UnityEngine.Events;

namespace GameConditions
{
    public enum GameConditionMode
    {
        SatisfyOnTrigger, FailOnTrigger
    }
    
    public abstract class GameCondition : ScriptableObject
    {
        public GameConditionMode mode;
        public bool changeStateOnce = true;
        
        public bool IsSatisfied { get; protected set; }

        public event UnityAction StateChanged;

        protected bool hasChangedState;
        
        public abstract void Trigger();

        public void Reset()
        {
            hasChangedState = false;
        }

        protected virtual void OnStateChanged()
        {
            hasChangedState = true;
            StateChanged?.Invoke();
        }
    }
}