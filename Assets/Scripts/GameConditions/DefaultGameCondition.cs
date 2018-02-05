using System;
using UnityEngine;

namespace GameConditions
{
    [CreateAssetMenu(menuName = "Game conditions/Default")]
    public class DefaultGameCondition : GameCondition
    {
        public override void Trigger()
        {
            if (changeStateOnce && hasChangedState) {
                return;
            }

            switch (mode) {
                case GameConditionMode.SatisfyOnTrigger:
                    IsSatisfied = true;
                    break;
                case GameConditionMode.FailOnTrigger:
                    IsSatisfied = false;
                    break;
            }
            
            OnStateChanged();
        }

        private void OnEnable()
        {
            switch (mode) {
                case GameConditionMode.SatisfyOnTrigger:
                    IsSatisfied = false;
                    break;
                case GameConditionMode.FailOnTrigger:
                    IsSatisfied = true;
                    break;
            }
        }
    }
}