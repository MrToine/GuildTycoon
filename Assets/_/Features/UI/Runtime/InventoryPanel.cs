using Core.Runtime;
using TMPro;
using UnityEngine;

namespace GameUI.Runtime
{
    public class InventoryPanel : BaseMonobehaviour
    {
        #region Unity API

        private void Start()
        {
            foreach (var txt in GetComponentsInChildren<TMP_Text>())
            {
                txt.text = LocalizationSystem.Instance.GetLocalizedText(txt.text);
            }
        }

        #endregion
    }
}

