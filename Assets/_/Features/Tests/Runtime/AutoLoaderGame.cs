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
            Info("On vérifie que GameManager existe");
            if (GameManager.Instance == null)
            {
                Warning("On charge le GameManager en repassant par le TitleScreen");
                SceneManager.LoadScene("TitleScreen");
            }
            else
            {
                Warning("Le GameManager existe, alors tout est ok");
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

