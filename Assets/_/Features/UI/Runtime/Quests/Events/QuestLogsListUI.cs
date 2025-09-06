using System;
using System.Collections.Generic;
using System.Linq;
using Core.Runtime;
using Quests.Runtime;
using UnityEngine;

namespace GameUI.Runtime.Events
{
    public class QuestLogsListUI : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        public void Refresh(Guid questId)
        {
            for (int i = _logPanel.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(_logPanel.transform.GetChild(i).gameObject);
            }

            var logs = GetQuestEventsLogList(questId);
            foreach (var log in logs.OrderBy(l => l.Time))
            {
                QuestEvent questEvent = QuestManager.Instance.GetEventById(log.EventId);
                GenerateLog(log.Time, questEvent);
            }
        }

        public void ShowFor(Guid questId)
        {
            gameObject.SetActive(true);
            Refresh(questId);
        }

        public void GenerateList(Guid questId)
        {
            Refresh(questId);
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        List<QuestEventLog> GetQuestEventsLogList(Guid questId)
        {
            Dictionary<Guid, List<QuestEventLog>> eventsDict = GetFact<Dictionary<Guid, List<QuestEventLog>>>("events_quests_history");

            if (eventsDict != null && eventsDict.TryGetValue(questId, out var logs))
                return logs;

            return new List<QuestEventLog>();
        }

        void GenerateLog(int time, QuestEvent questEvent)
        {
            GameObject logGO = Instantiate(_questLogPrefab, _logPanel.transform);
            QuestLogUI logUI = logGO.GetComponent<QuestLogUI>();
            logUI.setDatas(time, questEvent.DescriptionKey);
        }

        #endregion


        #region Privates and Protected

        // Variables privées
        [SerializeField] GameObject _questLogPrefab;
        [SerializeField] GameObject _logPanel;
        
        #endregion
    }
}
