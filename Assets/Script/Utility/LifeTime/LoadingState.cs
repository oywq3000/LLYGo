using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingState : ISceneState
{
    private string _targetScene;
    private AsyncOperationHandle<SceneInstance> _asyncOperationHandle;
    
    public LoadingState(SceneStateController stateController,string targetScene) : base("Loading", stateController)
    {
        _targetScene = targetScene;
    }

    private Image _loadBar;
    private Text _processText;
    
    private float _waitTime = 0;
    private float _totallTime = 2;

    public override void StateStart()
    {
        base.StateStart();
        _loadBar = GameObject.Find("Canvas/LoadingPanel/Slider").GetComponent<Image>();
        _processText = GameObject.Find("Canvas/LoadingPanel/ProcessText").GetComponent<Text>();
        
        
        //background loading 
        _asyncOperationHandle = Addressables.LoadSceneAsync(_targetScene, LoadSceneMode.Single, false);
    }

    
    
    
    
    public override void StateUpdate()
    {
        base.StateUpdate();
        
        Debug.Log("loading");
        
        
        _loadBar.fillAmount = _asyncOperationHandle.PercentComplete;

        _processText.text = _asyncOperationHandle.PercentComplete + "%";
        
        if ( _asyncOperationHandle.IsDone)
        {
            //entry game scene
            _asyncOperationHandle.Result.ActivateAsync();
        }
    }
}
