using Core.Runtime;
using Player.Runtime;
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
    }
}

