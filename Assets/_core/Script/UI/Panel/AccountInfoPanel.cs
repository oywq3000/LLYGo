using System;
using System.Collections;
using System.Collections.Generic;
using _core.AcountInfo;
using _core.Script.UI;
using _core.Script.UI.Panel;
using Cysharp.Threading.Tasks;
using MysqlUtility;
using SceneStateRegion;
using Script.Event;
using Script.Facade;
using Script.UI;
using UnityEngine;
using UnityEngine.UI;
using CharacterInfo = _core.AcountInfo.CharacterInfo;


public class AccountInfoPanel : AbstractUIPanel
{
    [SerializeField] private Text name;
    [SerializeField] private Text sex;
    [SerializeField] private Text age;
    [SerializeField] private Button modifyPlayerInfoBtn;
    [SerializeField] private Button modifyPasswordBtn;
    [SerializeField] private Button cancellationBtn;
    [SerializeField] private Button returnBtn;


    private void Start()
    {
        //register button event
        modifyPlayerInfoBtn.onClick.AddListener(() =>
        {
            GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("ModifyPanel");
        });
        modifyPasswordBtn.onClick.AddListener(() =>
        {
            GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("ModifyPasswordPanel");
        });
        returnBtn.onClick.AddListener(this.CloseSelf);
        
        cancellationBtn.onClick.AddListener(() =>
        {
            var operationPanel = GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("ConfirmPanel");

            var operation = operationPanel.GetComponent<IOperationPanel>();
            operationPanel.GetComponent<ConfirmPanel>().SetContentText("确定要删除改账号");

            operation.Result = variable =>
            {
                AccountCancellation();
            };
        });
        
        
        RefreshPanel();
        
        //register event
        GameFacade.Instance.RegisterEvent<OnInfoCHanged>(e =>
        {
            RefreshPanel();
        }).UnRegisterOnDestroy(gameObject);
    }

    public override void OnOpen()
    {
       
    }


    private async void AccountCancellation()
    {
        MysqlTool.DeleteAccount(GameFacade.Instance.GetAccount());

        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        GameLoop.Instance.Controller.SetState(new StartState(GameLoop.Instance.Controller),false);
    }
    
    private void RefreshPanel()
    {
        var accountInfo = MysqlTool.GetInfoByKey<AccountInfo>(GameFacade.Instance.GetAccount());
        //display the info
        name.text = $"姓名：{accountInfo.Account}";
        sex.text = $"性别：{ accountInfo.Sex}";
        age.text = $"年龄：{DateTime.Now.Year- Int32.Parse(accountInfo.BornYear)}";
    }
    protected override void Onclose()
    {
        
    }
}
