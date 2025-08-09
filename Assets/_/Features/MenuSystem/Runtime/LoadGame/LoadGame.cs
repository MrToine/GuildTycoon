using System;
using System.Collections.Generic;
using Core.Runtime;
using UnityEngine;

namespace MenuSystem.Runtime.LoadGame
{
    public class LoadGame : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void Start()
        {
            _allSaves = GetAllSaves();
            _slotPanel = transform.Find("ListPanel").gameObject;
            CreateSlots();
        }

        #endregion


        #region Main Methods

        //

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        private void CreateSlots()
        {
            foreach (var slot in _allSaves)
            {
                GameObject slotGO = Instantiate(_slotPrefab, _slotPanel.transform);
                slotGO.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = slot;
            }
        }

        #endregion


        #region Privates and Protected

        [SerializeField] private GameObject _slotPrefab;
        
        List<string> _allSaves = new List<string>();
        private GameObject _slotPanel;

        #endregion
    }
}

