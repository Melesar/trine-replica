using System.Collections;
using AI;
using Framework.References;
using Stats;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Melee strike")]
    public class MeleeStrikeAbility : Ability
    {
        public AttackStats attackStats;

        public StringReference attackParam;
        public FloatReference animationAttackDelay;
        
        public override void OnTrigger(PointerEventData pointerData)
        {
            StartCoroutine(AttackCoroutine());
        }
        
        public override void Initialize(GameObject actor)
        {
            base.Initialize(actor);
            actorAnimator.speed = attackStats.AttackSpeed;
        }

        private IEnumerator AttackCoroutine()
        {
            actorAnimator.SetTrigger(attackParam);
            yield return new WaitForSeconds(animationAttackDelay);

            HealthBehaviour targetHealth;
            if (FindTarget(out targetHealth)) {
                DealDamage(targetHealth);
            }
        }

        private void DealDamage(HealthBehaviour targetHealth)
        {
            targetHealth?.TakeDamage(attackStats.damage);
        }

        private bool FindTarget(out HealthBehaviour tagetHealth)
        {
            DebugAttackRaycast();
            
            var hit = Physics2D.Raycast(actorTransform.position + attackStats.attackOriginOffset, actorTransform.right, attackStats.attackRange,
                attackStats.attackMask);

            if (hit.collider == null) {
                tagetHealth = null;
                return false;
            }

            tagetHealth = hit.collider.GetComponent<HealthBehaviour>();
            return true;

        }

        private void DebugAttackRaycast()
        {
            var start = actorTransform.position + attackStats.attackOriginOffset;
            var end = start + actorTransform.right * attackStats.attackRange;
            Debug.DrawLine(start, end, Color.red, 1f);
        }

        public override void OnChanneling(PointerEventData pointerData)
        {
        }

        public override void OnStop(PointerEventData pointerData)
        {
        }

        
    }
}