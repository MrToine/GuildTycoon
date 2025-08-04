using System;
using System.Collections.Generic;
using Core.Runtime;
using Quests.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Runtime
{
    public class InfoQuestPanel : BaseMonobehaviour
    {

        #region Publics

        public TextMeshProUGUI m_title;
        public TextMeshProUGUI m_description;
        public TextMeshProUGUI m_objective;
        public TextMeshProUGUI m_reward;

        #endregion


        #region Unity API

        void Start()
        {
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }
        }

        #endregion


        #region Main Methods

        public void ShowInfo(QuestClass quest)
        {
            m_title.text = LocalizationSystem.Instance.GetLocalizedText(quest.Name);
            m_description.text = LocalizationSystem.Instance.GetLocalizedText(quest.Description);
            m_objective.text = LocalizationSystem.Instance.GetLocalizedText(quest.Objective);
            m_reward.text = RewardsListJoined(quest.Rewards);

            if (quest.State == QuestStateEnum.Active)
            {
                _buttonActivation.gameObject.SetActive(true);
            }
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        string RewardsListJoined(List<string> rewards)
        {
            return string.Join(", ", rewards);
        }

        #endregion


        #region Privates and Protected

        // Variables privées
        [SerializeField] GameObject _buttonActivation;

        #endregion
    }
}

