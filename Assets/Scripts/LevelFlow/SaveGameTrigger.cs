using Systems;
using UnityEngine;

namespace LevelFlow
{
    public class SaveGameTrigger : MonoBehaviour
    {
        public SaveSystem saveSystem;
        public LevelsSystem levelsSystem;
        
        public void SetLevelCompleted()
        {
            var currentLevelNumber = levelsSystem.CurrentLevelInfo.number;
            saveSystem.SetLevelCompleted(currentLevelNumber);
        }

        public void ReachNextLevel()
        {
            var currentLevelNumber = levelsSystem.CurrentLevelInfo.number;
            saveSystem.SetLevelReached(currentLevelNumber + 1);
        }

        public void SaveGame()
        {
            saveSystem.SaveGame();
        }
    }
}