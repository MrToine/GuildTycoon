using Core.Runtime;
using DG.Tweening;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace MenuSystem.Runtime
{
        public class PauseMenu : BaseMonobehaviour
        {
                #region Publics

                public bool IsOpen;
            
                #endregion


                #region Unity API

                void Update()
                {
                        if (GameManager.Instance.CanPause)
                        {
                                if (IsOpen)
                                {
                                        SlideIn();
                                }
                                else
                                {
                                        SlideOut();
                                }
                                
                                if (Input.GetKeyDown(KeyCode.Escape))
                                {
                                        TogglePause();
                                }
                        }
                }

                void TogglePause()
                {
                        IsOpen = !IsOpen;
                        Time.timeScale = IsOpen ? 0 : 1;
                        Cursor.visible = IsOpen;
                        Cursor.lockState = IsOpen ? CursorLockMode.None : CursorLockMode.Locked;
                        GameManager.Instance.IsOnPause = IsOpen;
                        
                        _panel.gameObject.SetActive(IsOpen);
                }


                #endregion
                

                #region Main Methods

                public void Resume()
                {
                        TogglePause();
                }

                public void Restart()
                {
                        GameManager.Instance.ReloadScene();
                }
                
                public void Quit()
                {
                        Application.Quit();
                }

                public void SetMainPanelVisible(bool visible)
                {
                        _defaultFocusPanel.SetActive(visible);
                }
            
                #endregion

            
                #region Utils

                private void SlideIn()
                {
                        Info("On slide IN");
                        //_panel.position = new Vector2(200, _panel.position.y);
                        _panel.transform.DOMove(new Vector3(99, _panel.position.y, 0), 0.3f).SetEase(Ease.Linear).SetUpdate(true);

                }
                
                private void SlideOut()
                {
                        Info("On slide OUT");
                        //_panel.position = new Vector2(-200, _panel.position.y);
                        _panel.transform.DOMove(new Vector3(-100, _panel.position.y, 0), 0.3f).SetEase(Ease.Linear);
                }
            
                #endregion
            
            
                #region Privates and Protected

                [Header("Referencement")]
                [SerializeField] private RectTransform _panel;
                [SerializeField] private GameObject _defaultFocusPanel;

                #endregion
        }
}
