using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Abilities
{
    public class ProlongedAbilityBehaviour : MonoBehaviour, IDeathListener, IAbilityStartListener, IAbilityEndListener, ICharacterChangedListener
    {
        public KeyCode key;
        public ProlongedAbility ability;

        private EventSystem eventSystem;
        private PointerEventData pointerData;
        private Camera mainCamera;

        private bool isActive;
        private bool canStart = true;
        private bool IsRunning => ability?.IsRunning ?? false;

        private void StartAbility()
        {
            if (!canStart) {
                return;
            }

            ability.onFinished += OnAbilityFinished;
            ability.StartAbility(pointerData);

            ExecuteEvents.Execute<IAbilityStartListener>(gameObject, pointerData,
                (handler, data) => handler.OnAbilityStart());
        }

        private void OnAbilityFinished()
        {
            ability.onFinished -= OnAbilityFinished;
            ExecuteEvents.Execute<IAbilityEndListener>(gameObject, pointerData,
                (handler, data) => handler.OnAbilityEnd());
        }

        private void Update()
        {
            if (!isActive) {
                return;
            }
            
            UpdatePointerData();
            TrackInput();
        }

        private void UpdatePointerData()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                pointerData.pressPosition = TransformScreenPoint(Input.mousePosition);
            }

            pointerData.pressPosition = TransformScreenPoint(pointerData.position);
            pointerData.position = TransformScreenPoint(Input.mousePosition);
        }

        private void TrackInput()
        {
            if (Input.GetKeyDown(key)) {
                StartAbility();
            }
        }

        private void Start()
        {
            ability?.Initialize(gameObject);
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

        public void OnAbilityStart()
        {
            if (!IsRunning) {
                canStart = false;
            }
        }

        public void OnAbilityEnd()
        {
            canStart = true;
        }

        public void OnCharacterChanged(CharacterType thisCharacter, CharacterType newCharacter)
        {
            isActive = thisCharacter == newCharacter;
        }
    }
}