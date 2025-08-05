using UnityEngine;

namespace EventSystem.Runtime
{
    public static class PanelSignals
    {
        public static event System.Action<string> OnRefresh;
        
        public static void RaiseRefresh(string panelName)
        {
            OnRefresh?.Invoke(panelName);
        }
    }
}

