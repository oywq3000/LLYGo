using UnityEngine;

namespace Script.UI
{
    public interface IUIkit
    {
        void OpenPanel(string key);
        void ClosePanel(GameObject obj);
        void ClosePanel(string key);
    }
}