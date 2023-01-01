using System;
using _core.Script.UI.UIVariable;
using Script.UI;
using TMPro;
using UnityEngine.UI;

namespace _core.Script.UI.Panel
{
    public class ConfirmPanel:AbstractUIPanel,IOperationPanel
    {

        public Button cancelBtn;
        public Button confirmBtn;

        public TextMeshProUGUI contentText;
        
        public Action<IVariable> Result { get; set; }

        private void Start()
        {
            cancelBtn.onClick.AddListener(() =>
            {
                CloseSelf();
            });
            
            confirmBtn.onClick.AddListener(() =>
            {
                CloseSelf();
                Result.Invoke(new ConfirmBool(){IsConfirm = true});
            });
        }

        public override void OnOpen()
        {
           
        }

        protected override void Onclose()
        {
           
        }

        public void SetContentText(string text)
        {
            contentText.text = text;
        }
    }
}