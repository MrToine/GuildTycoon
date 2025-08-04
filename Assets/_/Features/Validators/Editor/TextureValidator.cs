using UnityEditor;
using UnityEngine;

namespace Validators.Editor
{
    public class TextureValidator : AssetPostprocessor
    {
        private void OnPreprocessTexture()
        {
            if (assetPath.Contains("é")) // <- si une image contient un <é>
            {
                Debug.LogError($"Texture name contains spaces : {assetPath}");
                System.IO.File.Move("../../", assetPath);
                Debug.Log(assetPath);
            }
        }
    }
}
