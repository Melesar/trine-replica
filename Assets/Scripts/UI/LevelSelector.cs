using Systems;
using Framework.UI.Interfaces;
using UnityEngine;

namespace UI
{
    public class LevelSelector : MonoBehaviour, IWindowOpenListener, IWindowCloseListener
    {
        public RectTransform levelsBase;
        
        public LevelsSystem levelsSystem;

        public void OnWindowOpened()
        {
            SpawnLevelItems();
        }

        public void OnWindowClosed()
        {
            RemoveLevelItems();
        }
        
        private void SpawnLevelItems()
        {
            for (int i = 0; i < levelsSystem.LevelsAvailable; i++) {
                var levelItem = levelsSystem.GetLevelUiItem(i);
                levelItem.transform.SetParent(levelsBase, false);
            }            
        }

        private void RemoveLevelItems()
        {
            foreach (Transform levelItem in levelsBase) {
                Destroy(levelItem.gameObject);
            }
        }
    }
}