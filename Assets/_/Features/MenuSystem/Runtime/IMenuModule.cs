using UnityEngine;

namespace MenuSystem.Runtime
{
    public interface IMenuModule
    {
        string GetMenuName();
        GameObject GetMenuPanel();
    }
}
