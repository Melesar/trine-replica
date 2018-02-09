using Systems;
using UnityEngine;

namespace UI
{
    public class WinWindow : MonoBehaviour
    {
        public LevelsSystem levelsSystem;

        public void LoadNextLevel()
        {
            levelsSystem.LoadNextLevel();
        }
    }
}