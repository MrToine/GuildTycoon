using Adventurer.Runtime;
using Core.Runtime;
using UnityEngine;

namespace Decor.Runtime
{
    public class AdventurerModelBinder : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        public void SetupFromAdventurer(AdventurerClass adventurerClass)
        {
            foreach (Transform part in partsRoot)
            {
                part.gameObject.SetActive(true);
            }

            foreach (string partName in adventurerClass.ModelParts.Values)
            {
                Transform target = partsRoot.Find(partName);
                if (target != null)
                {
                    target.gameObject.SetActive(true);
                }
            }
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        [SerializeField] private Transform partsRoot;

        #endregion
    }
}

