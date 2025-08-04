using UnityEditor;
using UnityEngine;

namespace Validators.Editor
{
    [InitializeOnLoad]
    public static class PlayModePreventer
    {
        static PlayModePreventer()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.ExitingEditMode)
            {
                string[] guids = AssetDatabase.FindAssets("t:Texture");
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    if (path.Contains(""))
                    {
                        //EditorApplication.isPlaying = false;
                        //Debug.LogError("NON ! Tu as une erreur de nommage dans les textures ! ;)");
                    }
                }
            }
        }
    }
}

