using System.IO;
using TMPro;
using UnityEngine;

namespace Core.Runtime
{
    public class LocalizationSystem : BaseMonobehaviour
    {

        #region Publics

        public static LocalizationSystem Instance { get; private set; }

        #endregion


        #region Unity API

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion


        #region Main Methods
        
        public void LocalizeAllTextsIn(Transform root)
        {
            foreach (var txt in root.GetComponentsInChildren<TMP_Text>(true))
            {
                txt.text = GetLocalizedText(txt.text);
            }
        }
        
        public string GetLocalizedText(string key)
        {
            if (GameManager.Instance.GetLocalTexts.TryGetValue(key, out var value))
                return value;
    
            return $"{key}";
        }

        public void SaveLanguage(string newLanguage)
        {
            if (FactExists<string>("language", out var language))
            {
                RemoveFact<string>("language");
                SetFact<string>("language", newLanguage, FactPersistence.Persistent);
            }
            SetFact<string>("language", newLanguage, FactPersistence.Persistent);
        }
        
        public void LoadLanguage()
        {
            /*if (!FactExists<string>("language", out var language))
            {
                SetFact("language", language, FactPersistence.Persistent);
            }*/
            
            string lang = GetLanguage();
            GameManager.Instance.CurrentLanguage = lang;
            
            string langFile = GetLanguageFile();
            if (!string.IsNullOrEmpty(langFile))
            {
                GameManager.Instance.GetLocalTexts = XmlLoader.LoadDictionary(GetLanguageFile());
            }
            else
            {
            }
        }
        
        #endregion
        
        #region Utils

        private string GetLanguage()
        {
            string file = GetLanguageFile();
            
            return file.Split('/')[file.Split('/').Length - 1].Split('.')[0];
        }
        
        private string GetLanguageFile()
        {
            string filename = Application.dataPath + "/_/Database/Localization/" + GetFact<string>("language") + ".xml";

            if (File.Exists(filename))
            {
                return filename;
            }
            return "";
        }

        #endregion
    }
}

