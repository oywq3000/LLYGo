using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SceneStateRegion;
using UnityEngine;

public class GameBoot : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup _canvasGroup;
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Boot();
    }


   async void Boot()
    {
        #region ShowIntroduction

         //show introduction panel
        await _canvasGroup.DOFade(1, 1);
        
         await UniTask.Delay(TimeSpan.FromSeconds(3));
        
        await _canvasGroup.DOFade(0, 2);
        
        GameLoop.Instance.Controller.SetState(new StartState(GameLoop.Instance.Controller),false);
         #endregion
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
