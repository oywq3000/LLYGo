using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.SceneState;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private SceneStateController _controller;

    public static GameLoop Instance;

    private bool _senecStateInit = false;

    private void Awake()
    {
        //avoid the generate the same object
        if (GameObject.Find("GameLoop") != this.gameObject)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _controller = new SceneStateController();

        //entry a start scene without loading
        _controller.SetState(new StartSceneState(_controller), false, true).Forget();
        
        
    }

    private void FixedUpdate()
    {
        //different state need different data
        _controller?.StateUpdate();
    }


    //provide tool for GamaObject for current scene state

    public void Set(bool senecStateInit)
    {
        _senecStateInit = senecStateInit;
    }

    public async UniTask Setup()
    {
        if (_senecStateInit) return;

        //await for the completion of current scene state
        await UniTask.WaitUntil(() => _senecStateInit);
    }
}