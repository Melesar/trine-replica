using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Serialization
{
    [Serializable]
    public class GameSave
    {
        [SerializeField]
        private List<LevelInfo> levels;

        private Dictionary<int, LevelInfo> levelsMap;

        public List<LevelInfo> Levels => levels.ToList();

        public bool GetLevelByNumber(int number, ref LevelInfo levelInfo)
        {
            if (!levelsMap.ContainsKey(number)) {
                return false;
            }

            levelInfo = levelsMap[number];
            return true;
        }

        public void AddLevel(LevelInfo newLevelInfo)
        {
            levelsMap[newLevelInfo.number] = newLevelInfo;
        }
        
        public string ToJson()
        {
            levels = levelsMap.Values.ToList();
            return JsonUtility.ToJson(this);
        }

        public static GameSave FromJson(string json)
        {
            var save = JsonUtility.FromJson<GameSave>(json);
            save.levelsMap = save.levels.ToDictionary(info => info.number);

            return save;
        }

        public GameSave()
        {
            var firstLevelInfo = new LevelInfo {number = 1, isReached = true};
            
            levels = new List<LevelInfo> {firstLevelInfo};
            levelsMap = new Dictionary<int, LevelInfo> {{1, firstLevelInfo}};
        }
    }
}