using System.Collections.Generic;
using Framework.Events;
using GameConditions;
using UnityEngine;

namespace LevelFlow
{
    public class LoseManager : MonoBehaviour
    {
        public List<GameCondition> loseConditions;
        public GameEvent onGameOver;

        private void CheckForLose()
        {
            if (loseConditions.Exists(con => !con.IsSatisfied)) {
                OnGameOver();
            }
        }

        private void OnGameOver()
        {
            onGameOver.Raise();
        }

        private void Start()
        {
            foreach (var loseCondition in loseConditions) {
                loseCondition.StateChanged += CheckForLose;
            }
        }

        private void OnDestroy()
        {
            foreach (var loseCondition in loseConditions) {
                loseCondition.Reset();
                loseCondition.StateChanged -= CheckForLose;
            }
        }
    }
}