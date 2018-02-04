using System.Collections;
using Framework.EventListeners;
using UnityEngine;
using UnityEngine.Events;
using Utilites;

namespace Behaviours
{
    public class HookBehaviour : MonoBehaviour, ITriggerListener
    {
        public LineRenderer chainRenderer;

        private bool isCollided;
        
        public Transform HookTransform { get; private set; }

        private Vector3[] linePositions;
        private LayerMask hitMask;
        
        public event UnityAction<Collider2D> onHit;
        public event UnityAction onReturn;

        public void Launch(float flyingSpeed, float maxDistance, LayerMask hitMask)
        {
            HookTransform.gameObject.SetActive(true);
            
            enabled = true;
            this.hitMask = hitMask;
            
            StartCoroutine(FlyingCoroutine(flyingSpeed, maxDistance));
        }

        private IEnumerator FlyingCoroutine(float flyingSpeed, float maxDistance)
        {
            var distanceFlew = 0f;
            
            while (!isCollided && distanceFlew < maxDistance) {
                var frameDistance = flyingSpeed * Time.deltaTime;
                HookTransform.Translate(frameDistance, 0f, 0f);
                distanceFlew += frameDistance;
                
                UpdateLinePositions();

                yield return null;
            }

            var endHookPosition = HookTransform.position;
            var totalHookDistance = Vector3.Distance(HookTransform.position, transform.position);
            var currentHookDistance = totalHookDistance;
            
            while (currentHookDistance > 0.5f) {
                var frameDistance = flyingSpeed * Time.deltaTime;
                currentHookDistance -= frameDistance;

                HookTransform.position = Vector3.Lerp(transform.position, endHookPosition,
                    currentHookDistance / totalHookDistance);
                
                UpdateLinePositions();

                yield return null;
            }
            
            OnReturn();
            enabled = false;
            HookTransform.gameObject.SetActive(false);
        }

        private void UpdateLinePositions()
        {
            linePositions[0] = transform.position;
            linePositions[1] = HookTransform.position;

            chainRenderer.SetPositions(linePositions);
        }

        public void OnTriggerEnter(Collider2D other)
        {
            if (isCollided) {
                return;
            }
            
            if (!hitMask.ContainsLayer(other.gameObject.layer)) {
                return;
            }

            isCollided = true;
            OnHit(other);
        }

        protected virtual void OnHit(Collider2D collider)
        {
            onHit?.Invoke(collider);
        }

        protected virtual void OnReturn()
        {
            onReturn?.Invoke();
        }

        private void OnEnable()
        {
            isCollided = false;
            chainRenderer.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            chainRenderer.gameObject.SetActive(false);
        }

        private void Awake()
        {
            HookTransform = chainRenderer.transform;
            linePositions = new Vector3[2];
        }
    }
}