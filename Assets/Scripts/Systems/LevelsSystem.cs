using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Behaviours;
using Framework.Data;
using Framework.Events;
using Framework.References;
using Serialization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Systems
{
    [CreateAssetMenu(menuName = "Systems/Levels system")]
    public class LevelsSystem : SystemBase
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject unlockedLevelUi;
        [SerializeField] private GameObject lockedLevelUi;
        
        [Header("Strings")]
        [SerializeField] private StringReference levelsFolder;
        [SerializeField] private StringReference currentLevelFileName;
        [SerializeField] private StringReference assetBundleManifestName;

        [Header("Events")]
        [SerializeField] private GameEvent onLevelLoadingStarted;
        
        [Space]
        [SerializeField] private SaveSystem saveSystem;
        
        public string LevelsFolder => levelsFolder;

        public int LevelsAvailable
        {
            get
            {
                if (levelsManifest == null) {
                    LoadManifest();
                }
                return levelsAvailable;
            }
        }

        public LevelInfo CurrentLevelInfo
        {
            get
            {
                if (!isLevelInfoInitialized) {
                    LoadCurrentLevelInfo();
                }
                
                return currentLevelInfo;
            }
            private set
            {
                currentLevelInfo = value;
                isLevelInfoInitialized = true;
                SaveCurrentLevelInfo();
            }
        }

        private string CurrentLevelFilePath => $"{Application.persistentDataPath}/{currentLevelFileName}";

        private AssetBundleManifest LevelsManifest
        {
            get
            {
                if (levelsManifest == null) {
                    LoadManifest();
                }
                return levelsManifest;
            }
            set { levelsManifest = value; }
        }

        private AssetBundleManifest levelsManifest;
        private LevelInfo currentLevelInfo;
        private int levelsAvailable;
        private bool isLevelInfoInitialized;
        
        public void LoadNextLevel()
        {
            LoadLevel(CurrentLevelInfo.number + 1);
        }

        public void LoadLevel(int levelNumber)
        {
            StartCoroutine(LoadLevelCoroutine(levelNumber));
        }
        
        public GameObject GetLevelUiItem(int index)
        {
            if (index >= LevelsAvailable) {
                throw new ArgumentException("Invalid level requested", nameof(index));
            }
            
            var levelInfo = saveSystem.GetLevelInfo(index + 1);
            return levelInfo.isReached ? GetUnlockedLevel(index) : GetLockedLevel();
        }

        private GameObject GetLockedLevel()
        {
            return Instantiate(lockedLevelUi);
        }

        private GameObject GetUnlockedLevel(int index)
        {
            var level = Instantiate(unlockedLevelUi);
            var loader = level.GetComponent<LevelLoader>();
            if (loader != null) {
                loader.LevelNumber = index + 1;
            }

            return level;
        }
        
        private IEnumerator LoadLevelCoroutine(int levelNumber)
        {
            onLevelLoadingStarted.Raise();

            var bundleName = LevelsManifest.GetAllAssetBundles()[levelNumber - 1];
            var bundlePath = $"{Application.streamingAssetsPath}/{LevelsFolder}/{bundleName}";
            var bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);

            yield return bundleRequest;

            using (var bundle = new DisposableBundle(bundleRequest.assetBundle)) {
                var sceneName = Path.GetFileNameWithoutExtension(bundle.GetAllScenePaths()[0]);
                yield return SceneManager.LoadSceneAsync(sceneName);
            }

            CurrentLevelInfo = saveSystem.GetLevelInfo(levelNumber);
            
            FinishCoroutine();
        }

        private void LoadManifest()
        {
            var path = $"{Application.streamingAssetsPath}/{levelsFolder}/{levelsFolder}";

            Debug.Log($"Instance: {GetInstanceID()}. Manifest path: {path}");
            
            using (var bundle = new DisposableBundle(AssetBundle.LoadFromFile(path))) {
                levelsManifest = bundle.LoadAsset<AssetBundleManifest>(assetBundleManifestName);
            }

            levelsAvailable = levelsManifest.GetAllAssetBundles().Length;
        }

        private void SaveCurrentLevelInfo()
        {
            var formatter = new BinaryFormatter();
            using (var file = new FileStream(CurrentLevelFilePath, FileMode.OpenOrCreate)) {
                formatter.Serialize(file, CurrentLevelInfo);
            }
        }

        private void LoadCurrentLevelInfo()
        {
            if (!File.Exists(CurrentLevelFilePath)) {
                return;
            }
            
            var formatter = new BinaryFormatter();
            using (var file = new FileStream(CurrentLevelFilePath, FileMode.Open)) {
                currentLevelInfo = (LevelInfo) formatter.Deserialize(file);
            }

            isLevelInfoInitialized = true;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            LoadManifest();
            LoadCurrentLevelInfo();
        }
    }
}