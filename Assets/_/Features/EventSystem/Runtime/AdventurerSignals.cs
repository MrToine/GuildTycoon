using System;
using Adventurer.Runtime;
using UnityEngine;

namespace EventSystem.Runtime
{
    public static class AdventurerSignals
    {
        public static event Action<AdventurerClass> OnAdventurerSpawnRequested;
        public static event Action<AdventurerClass, Sprite> OnPortraitCaptured;
        public static event Action<AdventurerClass> OnPhotoCaptured;
        public static event Action<AdventurerClass> OnInfoAdventurerPanel;
        
        public static void RaiseSpawnRequested(AdventurerClass adventurerClass)
        {
            OnAdventurerSpawnRequested?.Invoke(adventurerClass);
        }

        public static void RaisePortraitCaptured(AdventurerClass adventurerClass, Sprite portrait)
        {
            OnPortraitCaptured?.Invoke(adventurerClass, portrait);       
        }
        
        public static void RaisePhotoCaptured(AdventurerClass adventurerClass)
        {
            OnPhotoCaptured?.Invoke(adventurerClass);
        }

        public static void RaiseInfoAdventurerPanel(AdventurerClass adventurerClass)
        {
            OnInfoAdventurerPanel?.Invoke(adventurerClass);
        }
    }
}

