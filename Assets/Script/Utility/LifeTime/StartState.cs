using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController stateController) : base("Start", stateController)
    {
        
    }
    
    private Transform _uiRoot;
    private Transform _startPanel;
    private Transform _btnPanel;
    private Transform _rankPanel;

    //
    private int _index = 0;
    
    public override void StateStart()
    {
        base.StateStart();
        
        Init();
    }
    
    public override void StateEnd()
    {
       Debug.Log("ending  StartState");
    }

    
    private void Init()
    {
        //initiate this scene
        _uiRoot = GameObject.Find("Canvas").transform;
        _startPanel = _uiRoot.Find("Player");
        _btnPanel = _uiRoot.Find("BtnPanel");
        _rankPanel = _uiRoot.Find("RankPanel");

        var startPanelChildCount = _startPanel.childCount;

        //register button event
        _btnPanel.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            //select role toward left
            _index = (--_index + startPanelChildCount) % startPanelChildCount;
            ShowPlayer(_index);
        });
          
        _btnPanel.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            //select role toward right
            _index = (++_index + startPanelChildCount) % startPanelChildCount;
            ShowPlayer(_index);
        });
        
        _btnPanel.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            //entry game
            StateController.SetState(new LoadingState(StateController,"")).Forget();
        });
    }
    
    private void ShowPlayer(int value)
    {
        for (int i = 0; i < _startPanel.childCount; i++)
        {
            _startPanel.GetChild(i).gameObject.SetActive(false);
        }
        _startPanel.GetChild(value).gameObject.SetActive(true);
    }

}
