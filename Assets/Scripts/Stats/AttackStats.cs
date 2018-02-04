using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Attack stats")]
    public class AttackStats : ScriptableObject
    {
        public float aggroRange;
        public float attackRange;

        public int damage;
        public float attackCooldown;
        
        public Vector3 attackOriginOffset;

        public LayerMask attackMask;

        public float AttackSpeed => 1f / attackCooldown;
    }
}