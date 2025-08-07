using UnityEngine;

namespace Item.Runtime
{
    [CreateAssetMenu(fileName = "New Item Definition", menuName = "Guild Tycoon/Items/Generation", order = 0)]
    public class ItemDefinitionSO : ScriptableObject
    {
        [SerializeField] Item _data;

        public string DisplayNameKey;
    }
}
