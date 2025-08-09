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
                // Texture name contains spaces
                System.IO.File.Move("../../", assetPath);
                Debug.Log(assetPath);
            }
        }
    }
}
