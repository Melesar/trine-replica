using System;
using UnityEngine;
using Framework.Events;
using Framework.References;
using Framework.Input.Data;
using Interfaces;
using Variables;

//using static UnityEngine.Input;

namespace Character
{
    public class CharacterInputHandler : MonoBehaviour, ICharacterChangedListener
    {
        public StringReference horizontalAxisName;
        public StringReference jumpAxisName;

        private bool isJump;
        private bool isActive;

        private CharacterController controller;

        private void Update()
        {
            if (!isActive) {
                return;
            }
            
            if (!isJump) {
                isJump = Input.GetAxis(jumpAxisName) != 0f;
            }
        }

        private void FixedUpdate()
        {
            if (!isActive) {
                return;
            }
            
            var move = Input.GetAxis(horizontalAxisName);
            controller.Move(move, isJump);
            isJump = false;
        }

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        public void OnCharacterChanged(CharacterType thisCharacter, CharacterType newCharacter)
        {
            isActive = thisCharacter == newCharacter;
        }
    }
}