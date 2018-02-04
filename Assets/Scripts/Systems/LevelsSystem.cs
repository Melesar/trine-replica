using System;
using System.Collections;
using System.IO;
using Behaviours;
using Framework.Data;
using Framework.Events;
using Framework.References;
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
        [SerializeField] private StringReference assetBundleManifestName;

        [Header("Events")]
        [SerializeField] private GameEvent onLevelLoadingStarted;
        
        public string LevelsFolder => levelsFolder;
        public int LevelsAvailable { get; private set; }

        private AssetBundleManifest levelsManifest;

        public IEnumerator LoadLevel(int levelNumber)
        {
            onLevelLoadingStarted.Raise();
            
            var bundleName = levelsManifest.GetAllAssetBundles()[levelNumber - 1];
            var bundlePath = $"{Application.streamingAssetsPath}/{LevelsFolder}/{bundleName}";
            var bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);

            yield return bundleRequest;

            var bundle = bundleRequest.assetBundle;
            var sceneName = Path.GetFileNameWithoutExtension(bundle.GetAllScenePaths()[0]);

            yield return SceneManager.LoadSceneAsync(sceneName);
        }
        
        public GameObject GetLevelUiItem(int index)
        {
            if (index >= LevelsAvailable) {
                throw new ArgumentException("Invalid level requested", nameof(index));
            }
            
            var level = Instantiate(unlockedLevelUi);
            var loader = level.GetComponent<LevelLoader>();
            if (loader != null) {
                loader.LevelNumber = index + 1;
            }

            return level;
        }
        
        protected override IEnumerator InitializationCoroutine()
        {
            var path = $"{Application.streamingAssetsPath}/{levelsFolder.Value}/{levelsFolder.Value}";

            var bundleRequest = AssetBundle.LoadFromFileAsync(path);

            yield return bundleRequest;

            using (var bundle = new DisposableBundle(bundleRequest.assetBundle)) {
                levelsManifest = bundle.LoadAsset<AssetBundleManifest>(assetBundleManifestName);
            }
            
            LevelsAvailable = levelsManifest.GetAllAssetBundles().Length;
        }
    }
}