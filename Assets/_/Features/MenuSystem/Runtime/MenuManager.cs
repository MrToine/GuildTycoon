using System.Collections.Generic;
using Core.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSystem.Runtime
{
    public class MenuManager : BaseMonobehaviour
    {

        #region Publics

        public static MenuManager Instance { get; private set; }

        #endregion


        #region Unity API

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        #endregion


        #region Main Methods

        public void RegisterMenu(IMenuModule module)
        {
            _registeredMenus.Add(module);
            
            GameObject btn = Instantiate(_buttonPrefab, _menuContainer);
            btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = module.GetMenuName();
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ShowPanel(module.GetMenuPanel());
                var pauseMenu = FindAnyObjectByType<PauseMenu>();
                pauseMenu?.SetMainPanelVisible(false);
                
                GameObject btnReturn = Instantiate(_buttonPrefab, module.GetMenuPanel().transform);
                btnReturn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Return";
                btnReturn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    module.GetMenuPanel().SetActive(false);
                    var pauseMenu = FindAnyObjectByType<PauseMenu>();
                    pauseMenu?.SetMainPanelVisible(true);
                });
            });
            
            module.GetMenuPanel().SetActive(false);
        }

        #endregion


        #region Utils

        private void ShowPanel(GameObject panelToShow)
        {
            foreach (var module in _registeredMenus)
            {
                module.GetMenuPanel().SetActive(false);
            }
            
            panelToShow.SetActive(true);
        }

        #endregion


        #region Privates and Protected

        private readonly List<IMenuModule> _registeredMenus = new();
        
        [SerializeField] private Transform _menuContainer;
        [SerializeField] private GameObject _buttonPrefab;

        #endregion
    }
}

