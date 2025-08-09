using Core.Runtime;
using UnityEngine;

namespace TestFacts.Runtime
{
    public class testLocalization : BaseMonobehaviour
    {
        private LocalizationSystem _localization = LocalizationSystem.Instance;

        [ContextMenu("Current Language ?")]
        public void GetCurrentLanguage()
        {
            _localization.LoadLanguage();
            // Affichage de la langue courante
        }
        
        [ContextMenu("Mettre en anglais")]
        public void SetEnglish()
        {
            GameManager.Instance.CurrentLanguage = "en";
            _localization.SaveLanguage(GameManager.Instance.CurrentLanguage);
            SaveFacts("GeneralSettings");
        }
        
        [ContextMenu("Mettre en français")]
        public void SetFrench()
        {
            GameManager.Instance.CurrentLanguage = "fr";
            _localization.SaveLanguage(GameManager.Instance.CurrentLanguage);
            SaveFacts("GeneralSettings");
        }
    }
}

