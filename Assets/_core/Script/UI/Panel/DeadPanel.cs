using System;
using System.Collections;
using System.Collections.Generic;
using SceneStateRegion;
using Script.Event;
using Script.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadPanel : AbstractUIPanel
{
    [SerializeField]
    private TextMeshProUGUI killCountText;
    [SerializeField]
    private TextMeshProUGUI surviveTimeText;
    [SerializeField]
    private Button returnMenu;
    
    private void Start()
    {
        returnMenu.onClick.AddListener(() =>
        {
            GameLoop.Instance.Controller.SetState(new MenuState(GameLoop.Instance.Controller),false);
        });
    }


    public override void OnOpen()
    {
        Cursor.visible = true;
        
        GameFacade.Instance.SendEvent(new GetRecordPanelData()
        {
            Result = e =>
            {
                killCountText.text = "击杀数 "+e.KillCount;
                surviveTimeText.text = "存活时间 "+e.SurviveTime+"s";
            }
        });
    }

    protected override void Onclose()
    {
    }
}