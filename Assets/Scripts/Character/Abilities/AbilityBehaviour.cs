using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Abilities
{
    public class AbilityBehaviour : MonoBehaviour, IDeathListener, ICharacterChangedListener
    {
        public KeyCode key;
        public Ability ability;

        private Camera mainCamera;
        private EventSystem eventSystem;
        private PointerEventData pointerData;

        private bool isActive;
        
        private void Update()
        {
            if (!isActive) {
                return;
            }
            
            UpdatePointerData();
            TrackKeyPressing();
        }

        private void TrackKeyPressing()
        {
            if (Input.GetKeyDown(key)) {
                ability.OnTrigger(pointerData);
            } else if (Input.GetKey(key)) {
                ability.OnChanneling(pointerData);
            } else if (Input.GetKeyUp(key)) {
                ability.OnStop(pointerData);
            }
        }

        private void UpdatePointerData()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                pointerData.pressPosition = TransformScreenPoint(Input.mousePosition);
            }

            pointerData.pressPosition = TransformScreenPoint(pointerData.position);
            pointerData.position = TransformScreenPoint(Input.mousePosition);
        }
        
        private void Start()
        {
            ability.Initialize(gameObject);
        }

        private void Awake()
        {
            eventSystem = EventSystem.current;
            mainCamera = Camera.main;
            pointerData = new PointerEventData(eventSystem);
        }

        private Vector3 TransformScreenPoint(Vector3 screenPoint)
        {
            var worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
            worldPoint.z = 0f;

            return worldPoint;
        }

        public void OnDeath()
        {
            enabled = false;
        }

        public void OnCharacterChanged(CharacterType thisCharacter, CharacterType newCharacter)
        {
            isActive = thisCharacter == newCharacter;
        }
    }
}