using System;
using Script.UI;
using TMPro;
using UnityEngine.UI;

namespace _core.Script.UI.Panel
{
    public class WarningPanel:AbstractUIPanel
    {
        public TextMeshProUGUI info;
        public Button cfBtn;


        private void Start()
        {
            cfBtn.onClick.AddListener(CloseSelf);
        }

        public void SetInfo(string str)
        {
            info.text = str;
        }
        
        public override void OnOpen()
        {
            
        }

        protected override void Onclose()
        {
           
        }
    }
}