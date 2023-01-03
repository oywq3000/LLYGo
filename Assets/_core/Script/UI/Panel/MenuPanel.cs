using System;
using System.Collections;
using System.Collections.Generic;
using _core.Script.UI.Panel;
using Cysharp.Threading.Tasks;
using SceneStateRegion;
using Script.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class MenuPanel : AbstractUIPanel
{
    [SerializeField] private Button startGameBtn;
    [SerializeField] private Button accountBtn;
    [SerializeField] private Button endGameBtn;
    [SerializeField] private Button helpBtn;


    private void Start()
    {
        startGameBtn.onClick.AddListener(() =>
        {
            if (GetComponentInChildren<SelectHeros>().IsSelected())
            {
                GameLoop.Instance.Controller.SetState(new MainState(GameLoop.Instance.Controller));
            }
            else
            {
                GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("WarningPanel").GetComponent<WarningPanel>()
                    .SetInfo("请添加英雄！");
            }
        });

        accountBtn.onClick.AddListener(() =>
        {
            GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("AccountInfoPanel");
        });


        endGameBtn.onClick.AddListener(() =>
        {
            GameLoop.Instance.Controller.SetState(new StartState(GameLoop.Instance.Controller),false);
        });
        
        helpBtn.onClick.AddListener(() =>
        {
            GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("HelpPanel");
        });
    }

    public override void OnOpen()
    {
    }

    protected override void Onclose()
    {
        Debug.Log("Onclose");
    }

    private void OnDestroy()
    {
    }
}