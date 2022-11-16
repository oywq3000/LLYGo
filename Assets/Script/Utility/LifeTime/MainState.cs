using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainState : ISceneState
{
    public MainState(SceneStateController stateController) : base("Main", stateController)
    {
    }

    private Transform _uiRoot;
    private Transform _timePanel;
    private Transform _endPanel;
    
    
    public override void StateStart()
    {
        base.StateStart();
        Init();
    }
    
    public override void StateEnd()
    {
        base.StateEnd();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }


    void Init()
    {
        _uiRoot = GameObject.Find("Canvas").transform;
        _timePanel = _uiRoot.Find("PlayTime");
        _endPanel = _uiRoot.Find("EndPanel");
        
        _endPanel.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            StateController.SetState(new StartState(StateController)).Forget();
        });
      
        _endPanel.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        });
    }
}
