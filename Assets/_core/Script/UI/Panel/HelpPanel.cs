using System;
using System.Collections;
using System.Collections.Generic;
using Script.UI;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : AbstractUIPanel
{

    [SerializeField] private Button closeBtn;

    private void Start()
    {
        closeBtn.onClick.AddListener(CloseSelf);
    }


    public override void OnOpen()
    {
        
    }

    protected override void Onclose()
    {
       
    }
}
