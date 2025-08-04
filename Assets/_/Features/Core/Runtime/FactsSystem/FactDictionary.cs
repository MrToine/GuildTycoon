using System;
using System.Collections.Generic;
using System.IO;
using Core.Runtime.Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Runtime
{
    public class FactDictionnary
    {
        #region Publics

        public Dictionary<string, IFact> AllFacts => _facts;

        #endregion
        
        #region Main Methods

        public bool FactExists<T>(string key, out T value)
        {
            if (_facts.TryGetValue(key, out var fact) && fact is Fact<T> typedFact)
            {
                value = typedFact.Value;
                return true;
            }
            
            value = default;
            return false;
        }

        public void SetFact<T>(string key, T value, BaseMonobehaviour.FactPersistence persistence)
        {
            if (_facts.TryGetValue(key, out var existingFact))
            {
                if (existingFact is Fact<T> typedFact)
                {
                    typedFact.Value = value;
                    typedFact.IsPersistent = persistence == BaseMonobehaviour.FactPersistence.Persistent;
                }
                else
                {
                    throw new InvalidCastException("Fact exists but is a wrong type");
                }
            }
            else
            {
                bool isPersistent = persistence == BaseMonobehaviour.FactPersistence.Persistent;
                _facts[key] = new Fact<T>(value, isPersistent);
            }
        }

        public T GetFact<T>(string key)
        {
            if (!_facts.TryGetValue(key, out var fact))
            {
                throw new InvalidCastException($"Fact {key} does not exist");
            }

            if (_facts[key] is not Fact<T> typedFact)
            {
                throw new InvalidCastException($"Fact {key} is not of type {typeof(T)}");
            }

            return typedFact.Value;
        }

        public Dictionary<string, IFact> GetAllFacts()
        {
            return AllFacts;
        }
        
        public List<T> GetAllFactsOfType<T>()
        {
            List<T> list = new();
            foreach (var fact in _facts.Values)
            {
                if (fact is Fact<T> typedFact)
                {
                    list.Add(typedFact.Value);
                }
            }
            return list;
        }

        public void RemoveFact<T>(string key)
        {
            _facts.Remove(key);
        }
        
        public List<string> GetAllSaves()
        {
            foreach (var slot in Directory.GetFiles(Application.persistentDataPath))
            {
                if (slot.Contains("_save.json") && !slot.Contains("GeneralSettings"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(slot);
                    if (fileName.EndsWith("_save"))
                    {
                        string slotName = fileName.Substring(0, fileName.Length - "_save".Length); // "Rom1"
                        _saves.Add(slotName);
                    }
                }
            }
            return _saves;
        }

        public bool SaveFileExists(string slotName= "")
        {
            if (slotName != "")
            {
                string filename = Path.Combine(Application.persistentDataPath, $"{slotName}_save.json");
                return File.Exists(filename);
            }

            int nbFiles = Directory.GetFiles(Application.persistentDataPath, "*.json").Length;
            string fileSettings = Path.Combine(Application.persistentDataPath, "GeneralSettings_save.json");
            if (File.Exists(fileSettings)) nbFiles--; // On retire le fichier nommé GeneralSettings_save.json
            
            if(nbFiles <= 0) return false;
            
            return true;
        }

        public bool SaveFacts(string slotName)
        {
            Dictionary<string, IFact> persistentsFacts = GetPersistentsFacts();
            
            // Permet à Newtonsoft de savoir quel type concret sérialiser via le champ $type
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                TypeNameHandling = TypeNameHandling.All // Ajout du type dans le JSON
            };
            
            string json = JsonConvert.SerializeObject(persistentsFacts, settings);
            string filename = Path.Combine(Application.persistentDataPath, $"{slotName}_save.json");
            
            Debug.Log($"Saving to {filename}");

            try
            {
                File.WriteAllText(filename, json);
                _saves.Add(slotName);
                return true;
            } 
            catch (Exception e)
            {
                return false;
            }
        }

        public bool LoadFacts(string slotName)
        {
            string file = Path.Combine(Application.persistentDataPath, $"{slotName}_save.json");
            if (File.Exists(file))
            {
                // Permet à Newtonsoft de savoir quel type concret désérialiser via le champ $type
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                
                string json = File.ReadAllText(file);
                Dictionary<string, IFact> facts = JsonConvert.DeserializeObject<Dictionary<string, IFact>>(json, settings);
                
                foreach (var fact in facts)
                {
                    _facts[fact.Key] = fact.Value;
                }
                return true;
            }
            
            return false;
        }

        public void DeleteSaveFile(string slotName)
        {
            string file = Path.Combine(Application.persistentDataPath, $"{slotName}_save.json");
            File.Delete(file);
        }
        
        #endregion
        
        #region Utils
        
        private Dictionary<string, IFact> GetPersistentsFacts()
        {
            Dictionary<string, IFact> persistentsFacts = new();

            foreach (var fact in _facts)
            {
                if (fact.Value.IsPersistent)
                {
                    persistentsFacts.Add(fact.Key, fact.Value);
                }
            }
            return persistentsFacts;
        }

        #endregion
        
        #region private
        
        private Dictionary<string, IFact> _facts = new();
        private List<string> _saves = new List<string>();

        #endregion
    }
}