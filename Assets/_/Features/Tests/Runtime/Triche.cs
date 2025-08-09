using System;
using System.Collections.Generic;
using Core.Runtime;
using Player.Runtime;
using Quests.Runtime;
using UnityEngine;

namespace TestFacts.Runtime
{
    public class Triche : BaseMonobehaviour
    {
        [ContextMenu("Add 99999 Gold")]
        void AddInfiniteGold()
        {
            PlayerClass profile = GetFact<PlayerClass>(GameManager.Instance.Profile);
            profile.Money = 99999;
            Info("➕99999 Gold added");
            SaveFacts();
        }
        
        [ContextMenu("MAJ Facts = Créer l'historique des events ")]
        void CreateHistory()
        {
            SetFact<Dictionary<Guid, List<QuestEvent>>>("events_quests_history", new Dictionary<Guid, List<QuestEvent>>(), FactPersistence.Persistent);
            SaveFacts();
        }
    }
}

