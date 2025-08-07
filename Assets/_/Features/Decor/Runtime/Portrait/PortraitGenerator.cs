using System.Collections;
using System.Collections.Generic;
using _.Features.Decor.Runtime;
using Adventurer.Runtime;
using Core.Runtime;
using Player.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace Decor.Runtime
{
    public class PortraitGenerator : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API
        
        //

        #endregion


        #region Main Methods

        Queue<(AdventurerClass, GameObject)> _queue = new();
        bool _isProcessing = false;

        public void GeneratePortrait(AdventurerClass adventurerClass, GameObject prefab)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            _queue.Enqueue((adventurerClass, prefab));
            if (!_isProcessing)
            {
                StartCoroutine(ProcessQueue());
            }
        }

        IEnumerator ProcessQueue()
        {
            _isProcessing = true;

            while (_queue.Count > 0)
            {
                var (adventurerClass, prefab) = _queue.Dequeue();

                _currentAdventurerModel = Instantiate(prefab, transform);
                _currentAdventurerModel.GetComponent<AdventurerModelBinder>().SetupFromAdventurer(adventurerClass);
                _currentAdventurerModel.GetComponent<NavMeshAgent>().enabled = false;
                _currentAdventurerModel.GetComponent<Deplacements>().enabled = false;

                var photo = GetComponentInChildren<TakePhoto>();
                if (photo != null)
                {
                    yield return StartCoroutine(photo.CaptureRoutine(adventurerClass));
                }

                Destroy(_currentAdventurerModel);
                yield return new WaitForEndOfFrame();
                _currentAdventurerModel = null;

                yield return new WaitForSeconds(0.1f); // courte pause pour laisser respirer Unity
            }

            _isProcessing = false;
            gameObject.SetActive(false);
        }
        
        #endregion


        #region Utils

        

        #endregion


        #region Privates and Protected

        GameObject _currentAdventurerModel;

        #endregion
    }
}
