using System.Collections.Generic;
using Adventurer.Runtime;
using Core.Runtime;
using EventSystem.Runtime;
using Player.Runtime;
using UnityEngine;

namespace Decor.Runtime
{
    public class AdventurerApearanceSpawner : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        void Start()
        {
            AdventurerSignals.OnAdventurerSpawnRequested += OnSpawnAdventurerModel;
            
            List<AdventurerClass> allAdventurers = GetFact<List<AdventurerClass>>("my_adventurers");

            foreach (AdventurerClass adventurer in allAdventurers)
            {
                OnSpawnAdventurerModel(adventurer);
                _countAdventurers++;
            }

            StartCoroutine(WaitAndDeactivatePortraitGenerator());
        }

        void OnDestroy()
        {
            AdventurerSignals.OnAdventurerSpawnRequested -= OnSpawnAdventurerModel;
        }

        #endregion


        #region Main Methods

        void OnSpawnAdventurerModel(AdventurerClass adventurerClass)
        {
            GameObject model = Instantiate(_adventurerPrefab, transform);
            model.GetComponent<AdventurerModelBinder>().SetupFromAdventurer(adventurerClass);
            _portraitGenerator.GeneratePortrait(adventurerClass, _adventurerPrefab);
        }

        #endregion


        #region Utils

         System.Collections.IEnumerator WaitAndDeactivatePortraitGenerator()
        {
            yield return new WaitUntil(() => _countAdventurers == GetFact<PlayerClass>(GameManager.Instance.Profile).AdventurersCount);
            yield return new WaitForSeconds(1f);
            _portraitGenerator.GameObject.SetActive(false);
        }

        #endregion


        #region Privates and Protected

        [SerializeField] GameObject _adventurerPrefab;
        [SerializeField] PortraitGenerator _portraitGenerator;

        int _countAdventurers = 0;

        #endregion
    }
}
