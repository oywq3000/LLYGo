using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.SceneState;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private SceneStateController _controller;
    
    private void Awake()
    {
        //avoid the generate the same object
        if (GameObject.Find("GameLoop") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _controller = new SceneStateController();
        
        //entry a start scene without loading
        _controller.SetState(new StartSceneState(_controller), false,true).Forget();
    }

    
    
    private void FixedUpdate()
    {
        //different state need different data
        _controller?.StateUpdate();
    }
}
