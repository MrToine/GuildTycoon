using System;
using UnityEngine;

namespace Quests.Runtime
{
    [CreateAssetMenu(fileName = "QuestEventSO", menuName = "Guild Tycoon/Quests/Event", order = 0)]
    public class QuestEventSO : ScriptableObject
    {
        [SerializeField] string m_assetGuid;
        public QuestEvent m_data;
        public Guid AssetGuid => Guid.Parse(m_assetGuid);

        public QuestEvent ToQuestEventClass(int time, Guid? id = null)
        {
            QuestEvent eventQuest = new QuestEvent(AssetGuid, this, time);
            
            return eventQuest;
        }
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            Guid tempGuid;
            if (string.IsNullOrEmpty(m_assetGuid) || !Guid.TryParse(m_assetGuid, out tempGuid))
            {
                m_assetGuid = Guid.NewGuid().ToString();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
        #endif
    }
}
