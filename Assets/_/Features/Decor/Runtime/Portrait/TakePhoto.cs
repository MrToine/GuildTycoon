using System.Collections;
using Adventurer.Runtime;
using Core.Runtime;
using EventSystem.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace _.Features.Decor.Runtime
{
    public class TakePhoto : BaseMonobehaviour
    {

        #region Publics

        public Image m_targetImage;

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        // Coroutine to capture the portrait asynchronously
        public IEnumerator CaptureRoutine(AdventurerClass adventurer)
        {
            yield return new WaitForEndOfFrame(); // attendre le rendu de la frame

            _camera.Render();
            RenderTexture renderTexture = _camera.targetTexture;

            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = renderTexture;
            Texture2D image = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            image.Apply();
            RenderTexture.active = currentRT;

            Sprite portraitSprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
            AdventurerSignals.RaisePortraitCaptured(adventurer, portraitSprite);

            if (m_targetImage != null)
            {
                m_targetImage.sprite = portraitSprite;
            }

            yield return new WaitForSeconds(0.1f); // attendre un court instant avant de lever l'événement final
            AdventurerSignals.RaisePhotoCaptured(adventurer);
        }

        #endregion


        #region Utils

        //

        #endregion


        #region Privates and Protected

        [SerializeField] Camera _camera;

        #endregion
    }
}
