using System;
using Core.Runtime;
using Player.Runtime;
using TMPro;

namespace GameUI.Runtime
{
    public class InfoPanel : BaseMonobehaviour
    {

        #region Publics

        public TextMeshProUGUI m_guildName;
        public TextMeshProUGUI m_level;
        public TextMeshProUGUI m_golds;
        public TextMeshProUGUI m_adventurerCount;

        #endregion


        #region Unity API

        void Start()
        {
            _infoPlayerClass = GetFact<PlayerClass>(GameManager.Instance.Profile);
            LocalizationSystem.Instance.LocalizeAllTextsIn(gameObject.transform);
        }
        
        void Update()
        {
            m_guildName.text = _infoPlayerClass.GuildName;
            m_level.text = $"{_infoPlayerClass.GuildLevel.ToString()}";
            m_golds.text = _infoPlayerClass.Money.ToString();
            m_adventurerCount.text = $"{_infoPlayerClass.AdventurersCount.ToString()}/{_infoPlayerClass.AdventurersMax.ToString()}";
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        PlayerClass _infoPlayerClass;

        #endregion
    }
}

