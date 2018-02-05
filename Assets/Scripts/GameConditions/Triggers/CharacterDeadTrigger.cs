using UnityEngine;

namespace GameConditions.Triggers
{
    public class CharacterDeadTrigger : MonoBehaviour, IDeathListener
    {
        public GameCondition condition;
        
        public void OnDeath()
        {
            condition.Trigger();  
        }
    }
}