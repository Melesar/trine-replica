using System;
using System.Collections;
using System.IO;
using Framework.References;
using Serialization;
using UnityEngine;

namespace Systems
{
    [CreateAssetMenu(menuName = "Systems/Save system")]
    public class SaveSystem : SystemBase
    {
        public StringReference savePath;
        public StringReference saveFileName;

        private GameSave currentSave;

        public void SetLevelCompleted(int number)
        {
            var levelInfo = new LevelInfo();

            if (currentSave.GetLevelByNumber(number, ref levelInfo)) {
                levelInfo.isCompleted = true;
            }
        }

        public void SetLevelReached(int number)
        {
            var levelInfo = new LevelInfo();

            if (currentSave.GetLevelByNumber(number, ref levelInfo)) {
                levelInfo.isReached = true;
            } else {
                levelInfo.number = number;
                levelInfo.isReached = true;

                currentSave.AddLevel(levelInfo);
            }
        }
        
        public void SaveGame()
        {
            var path = $"{Application.persistentDataPath}/{savePath.Value}/{saveFileName}";
            var json = currentSave.ToJson();

            File.WriteAllText(path, json);
        }

        public LevelInfo GetLevelInfo(int number)
        {
            var levelInfo = new LevelInfo();
            if (!currentSave.GetLevelByNumber(number, ref levelInfo)) {
                levelInfo.number = number;
            }

            return levelInfo;
        }

        private void LoadGame()
        {
            var path = $"{Application.persistentDataPath}/{savePath.Value}";

            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            path = $"{path}/{saveFileName.Value}";

            if (File.Exists(path)) {
                var json = File.ReadAllText(path);
                currentSave = GameSave.FromJson(json);
            } else {
                currentSave = new GameSave();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            LoadGame();
        }
    }
}