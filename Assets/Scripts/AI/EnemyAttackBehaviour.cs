using UnityEngine;
using UnityEngine.EventSystems;
using Framework.References;
using Stats;

namespace AI
{
    public class EnemyAttackBehaviour : MonoBehaviour
    {
        public AttackStats attackStats;

        public StringReference attackParamName;

        private bool isPlayerDetected;
        private bool isAttacking;

        private EventSystem eventSystem;
        private Collider2D[] detectedPlayers = new Collider2D[1];
        private Collider2D collider;
        private Animator animator;

        private Collider2D PlayerCollider => isPlayerDetected ? detectedPlayers[0] : null;

        public void Attack()
        {
            if (GetDistanceToPlayer() > attackStats.attackRange) {
                return;
            }

            var playerHealth = PlayerCollider.GetComponent<HealthBehaviour>();
            if (playerHealth.IsDead) {
                StopAttackingPlayer();
            } else {
                playerHealth.TakeDamage(attackStats.damage);
            }
        }

        private void StopAttackingPlayer()
        {
            animator.SetBool(attackParamName, false);
            isPlayerDetected = false;
            isAttacking = false;
        }

        private void FixedUpdate()
        {
            DetectPlayer();
            
            if (!isAttacking && isPlayerDetected) {
                StartAttackingPlayer();
            }
        }

        private void DetectPlayer()
        {
            var playersDetected = Physics2D.OverlapCircleNonAlloc(transform.position,
             attackStats.aggroRange, detectedPlayers, attackStats.attackMask);

            if (playersDetected > 0 && !isPlayerDetected) {
                isPlayerDetected = true;
                SendPlayerDetected();
            } else if (playersDetected == 0 && isPlayerDetected) {
                StopAttackingPlayer();                
                SendPlayerMissed();
            }
        }

        private void StartAttackingPlayer()
        {
            var distanceToPlayer = PlayerCollider.Distance(collider);
            if (distanceToPlayer.distance > attackStats.attackRange) {
                animator.SetBool(attackParamName, false);
                isAttacking = false;
                return;
            }

            animator.speed = Mathf.Min(attackStats.AttackSpeed, 1f);
            animator.SetBool(attackParamName, true);
            isAttacking = true;
        }

        private void SendPlayerDetected()
        {
            var detectedPlayer = detectedPlayers[0].gameObject;
            ExecuteEvents.Execute<IPlayerDetectedListener>(gameObject, new BaseEventData(eventSystem),
                (listener, data) => listener.OnPlayerDetected(detectedPlayer));
        }

        private void SendPlayerMissed()
        {
            ExecuteEvents.Execute<IPlayerMissedListener>(gameObject, new BaseEventData(eventSystem),
                (listener, data) => listener.OnPlayerMissed());
        }

        private float GetDistanceToPlayer()
        {
            var playerCollider = PlayerCollider;
            if (playerCollider == null) {
                return float.PositiveInfinity;
            }

            return playerCollider.Distance(collider).distance;
        }

        private void Awake()
        {
            eventSystem = EventSystem.current;
            collider = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
        }

        private void OnDrawGizmosSelected()
        {
            if (attackStats == null) {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackStats.aggroRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackStats.attackRange);
        }
    }
}