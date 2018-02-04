using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class BundleLevels
    {
        [MenuItem("Utilites/Bundle levels")]
        public static void Bundle()
        {
            var manifest = BuildPipeline.BuildAssetBundles(Path.Combine(Application.streamingAssetsPath, "Levels"),
                BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }
    }
}