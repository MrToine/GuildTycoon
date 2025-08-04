using System.Collections.Generic;
using Core.Runtime.Interfaces;
using UnityEngine;

namespace Core.Runtime
{
    public class BaseMonobehaviour : MonoBehaviour
    {
        #region Publics
    
        [Header("Debug")]
        [SerializeField] protected bool m_isVerbose;
        
        public enum FactPersistence
        {
            Normal,
            Persistent,
        }
        
        public GameObject GameObject => _gameObject ? _gameObject : _gameObject = gameObject;
        public Rigidbody Rigidbody => _rigidbody ? _rigidbody : _rigidbody = GetComponent<Rigidbody>();
        public Transform Transform => _transform ? _transform : _transform = GetComponent<Transform>();
        
        #endregion
        
        #region Fact Dictionnary

        protected bool FactExists<T>(string key, out T value)
        {
            GameFacts.FactExists<T>(key, out value);
            return value != null;
        }

        protected T GetFact<T>(string key)
        {
            return GameFacts.GetFact<T>(key);
        }

        protected Dictionary<string, IFact> GetAllFacts()
        {
            return GameFacts.GetAllFacts();
        }
        
        protected List<T> GetAllFactsOfType<T>()
        {
            return GameFacts.GetAllFactsOfType<T>();
        }

        protected void SetFact<T>(string key, T value, FactPersistence persistence = FactPersistence.Normal)
        {
            GameFacts.SetFact<T>(key, value, persistence);
        }

        protected void RemoveFact<T>(string key)
        {
            GameFacts.RemoveFact<T>(key);
        }

        #endregion
        
        #region Save System

        protected List<string> GetAllSaves()
        {
            return GameFacts.GetAllSaves();
        }

        protected void SaveFacts(string slotName = "")
        {
            string slot = GameManager.Instance.Profile;
            if (slotName != "")
            {
                slot = slotName;
            }
            
            if (GameFacts.SaveFacts(slot))
            {
                Info("Données sauvegardées avec succès dans le fichier save.json");
            }
            else
            {
                Error("Failed to save facts");
            }
        }

        protected void LoadFacts(string slotName = "")
        {
            string slot = GameManager.Instance.Profile;
            if (slotName != "")
            {
                slot = slotName;
            }
            
            if (GameFacts.LoadFacts(slot))
            {
                Info("Données chargées avec succès");
            }
            else
            {
                Error("Failed to load facts");
            }
        }

        protected void DeleteSaveFile()
        {
            GameFacts.DeleteSaveFile(GameManager.Instance.Profile);
        }
        
        #endregion
        
        #region DEBUG
        
        protected void Info(string message)
        {
            if (!m_isVerbose) return;
            Debug.Log(message, this);
        }

        protected void Error(string message)
        {
            if (!m_isVerbose) return;
            Debug.LogError(message, this);
        }

        protected void Warning(string message)
        {
            if (!m_isVerbose) return;
            Debug.LogWarning(message, this);
        }
        
        #endregion
        
        #region GETTERS
        
        private GameObject _gameObject;
        private Rigidbody _rigidbody;
        private Transform _transform;

        protected FactDictionnary GameFacts => GameManager.m_gameFacts;

        #endregion
    }
}
