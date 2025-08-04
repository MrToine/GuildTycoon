using System;
using Adventurer.Runtime;
using UnityEngine;

namespace EventSystem.Runtime
{
    public class LegacyRecruitementEvents : MonoBehaviour
    {
        public static event Action<AdventurerClass> OnHeroRecruited;
        public static event Action<AdventurerClass> OnSpawnAdventurerModel;

        private void OnEnable()
        {
            AdventurerSignals.OnAdventurerSpawnRequested += RelaySpawn;
        }

        private void OnDisable()
        {
            AdventurerSignals.OnAdventurerSpawnRequested -= RelaySpawn;
        }

        private void RelaySpawn(AdventurerClass adventurer)
        {
            OnHeroRecruited?.Invoke(adventurer);
            OnSpawnAdventurerModel?.Invoke(adventurer);
        }
    }
}

