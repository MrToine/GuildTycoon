using Core.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestFacts.Runtime
{
    public class AutoLoaderGame : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        void Awake()
        {
            if (GameManager.Instance == null)
            {
                SceneManager.LoadScene("TitleScreen");
            }
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        // Variables privées

        #endregion
    }
}

