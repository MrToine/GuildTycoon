using System;
using Adventurer.Runtime;
using UnityEngine;

namespace EventSystem.Runtime
{
    public static class AdventurerSignals
    {
        public static event Action<AdventurerClass> OnAdventurerSpawnRequested;
        public static event Action OnRefresh;
        public static event Action<AdventurerClass, Sprite> OnPortraitCaptured;
        public static event Action<AdventurerClass> OnPhotoCaptured;
        public static event Action<AdventurerClass> OnInfoAdventurerPanel;
        public static event Action<AdventurerClass> OnAdventurerSelected; 
        public static event Action<AdventurerClass> OnAdventurerUnselected; 
        
        public static void RaiseSpawnRequested(AdventurerClass adventurerClass)
        {
            OnAdventurerSpawnRequested?.Invoke(adventurerClass);
        }

        public static void RaiseRefreshAdventurers()
        {
            OnRefresh?.Invoke();
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
        
        public static void RaiseAdventurerSelected(AdventurerClass adventurerClass)
        {
            OnAdventurerSelected?.Invoke(adventurerClass);
        }
        
        public static void RaiseAdventurerUnselected(AdventurerClass adventurerClass)
        {
            OnAdventurerUnselected?.Invoke(adventurerClass);
        }
    }
}

