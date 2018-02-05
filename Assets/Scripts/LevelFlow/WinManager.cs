using System.Collections.Generic;
using Framework.Events;
using UnityEngine;
using GameConditions;

namespace LevelFlow
{
    public class WinManager : MonoBehaviour
    {
        public List<GameCondition> winConditions;
        public GameEvent onLevelWon;

        private void CheckForWin()
        {
            if (winConditions.TrueForAll(con => con.IsSatisfied)) {
                OnLevelWon();
            }
        }

        private void OnLevelWon()
        {
            onLevelWon.Raise();
        }

        private void Start()
        {
            foreach (var winCondition in winConditions) {
                winCondition.StateChanged += CheckForWin;
            }
        }

        private void OnDestroy()
        {
            foreach (var winCondition in winConditions) {
                winCondition.Reset();
                winCondition.StateChanged -= CheckForWin;
            }
        }
    }
}