using Systems;
using UnityEngine;

namespace Behaviours
{
    public class LevelLoader : MonoBehaviour
    {
        public LevelsSystem levelsSystem;

        public int LevelNumber { get; set; }
        
        public void OnLevelSelected()
        {
            levelsSystem.LoadLevel(LevelNumber);
        }
    }
}