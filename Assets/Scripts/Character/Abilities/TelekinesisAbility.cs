using Behaviours;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Telekinesis")]
    public class TelekinesisAbility : Ability
    {
        public GameObject particleEffectPrefab;
        
        private Transform anchor;
        private Telekinetic attachedObject;
        
        public override void OnTrigger(PointerEventData pointerData)
        {
            anchor.gameObject.SetActive(true);
            CatchObject(pointerData.position);
        }

        public override void OnChanneling(PointerEventData pointerData)
        {
            if (attachedObject == null) {
                return;
            }

            anchor.position = pointerData.position;
        }

        public override void OnStop(PointerEventData pointerData)
        {
            anchor.gameObject.SetActive(false);
            
            if (attachedObject != null) {
                attachedObject.transform.parent = null;
                attachedObject.Release();
            }
            
            attachedObject = null;
        }
        
        public override void Initialize(GameObject actor)
        {
            base.Initialize(actor);

            anchor = Instantiate(particleEffectPrefab).transform;
            anchor.gameObject.SetActive(false);
        }

        private void CatchObject(Vector3 worldPosition)
        {
            var raycastResult = Physics2D.Raycast(worldPosition, Vector2.zero);
            
            attachedObject = raycastResult.collider?.GetComponent<Telekinetic>();
            if (attachedObject == null) {
                return;
            }

            anchor.position = worldPosition;
            
            attachedObject.transform.SetParent(anchor);
            attachedObject.transform.localPosition = Vector3.zero;
            
            attachedObject.Capture();
        }
    }
}