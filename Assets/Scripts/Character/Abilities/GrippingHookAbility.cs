using Behaviours;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Character.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Gripping hook")]
    public class GrippingHookAbility : ProlongedAbility
    {
        [Header("Game properties")]
        public float flyingSpeed;
        public float maxDistance;
        public LayerMask hitMask;

        [Space]
        public Vector2 parentingOffset;
        public HookBehaviour hookPrefab;

        private HookBehaviour hook;
        private Transform hookTransform;
        private Transform hookedTarget;
        
        public override void StartAbility(PointerEventData pointerData)
        {
            RotateHook(pointerData.position);
            hook.Launch(flyingSpeed, maxDistance, hitMask);
            IsRunning = true;
        }

        private void RotateHook(Vector2 worldPos)
        {
            hook.transform.LookAt(worldPos);
        }

        private void OnHookHit(Collider2D hitCollider)
        {
            hookedTarget = hitCollider.transform;
            hookedTarget.SetParent(hook.HookTransform);
        }

        private void OnHookReturn()
        {
            IsRunning = false;

            if (hookedTarget != null) {
                hookedTarget.parent = null;
            }

            hookedTarget = null;
            
            OnFinished();
        }

        public override void Initialize(GameObject actor)
        {
            base.Initialize(actor);
            
            hook = Instantiate(hookPrefab, actor.transform, false);
            hookTransform = hook.transform;
            hookTransform.localPosition = parentingOffset;
            
            hook.onHit += OnHookHit;
            hook.onReturn += OnHookReturn;
        }
    }
}