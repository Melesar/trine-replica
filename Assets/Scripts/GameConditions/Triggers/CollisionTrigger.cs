using UnityEngine;
using Utilites;

namespace GameConditions.Triggers
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionTrigger : MonoBehaviour
    {
        public GameCondition condition;
        public LayerMask collisionMask;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (collisionMask.ContainsLayer(other.gameObject.layer)) {
                condition.Trigger();
            }
        }
    }
}