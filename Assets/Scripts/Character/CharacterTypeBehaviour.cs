using System;
using Interfaces;
using UnityEngine;
using RuntimeSets;
using UnityEngine.EventSystems;
using Variables;

namespace Character
{
    public class CharacterTypeBehaviour : MonoBehaviour, IDeathListener
    {
        public CharacterType characterType;
        public CharactersSet set;
        public CharacterTypeVariable currentCharacter;

        public bool IsAvailableForSwitch { get; private set; } = true;
        
        private BaseEventData eventData; 
        
        private void Awake()
        {
            set.Add(this);
            
            eventData = new BaseEventData(EventSystem.current);
            
            currentCharacter.valueChanged += OnCharacterChanged;
        }

        private void OnCharacterChanged(CharacterType oldCharacter, CharacterType newCharacter)
        {
            ExecuteEvents.Execute<ICharacterChangedListener>(gameObject, eventData,
                (handler, data) => handler.OnCharacterChanged(characterType, newCharacter));
        }

        private void OnDestroy()
        {
            currentCharacter.valueChanged -= OnCharacterChanged;
            
            set.Remove(this);
        }

        public void OnDeath()
        {
            IsAvailableForSwitch = false;
        }
    }
}