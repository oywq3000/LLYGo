using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script.UI;
using UnityEngine;

namespace Script.SceneState
{
    public class StartSceneState:ISceneState
    {
        private IUIkit _uIkit;
        
        //Introduction panel 
        private CanvasGroup _introductionPanel;
        public StartSceneState(SceneStateController stateController) : base("Start", stateController)
        {
        }

        public async override void StateStart()
        {
            #region ShowIntroduction

            // //show introduction panel
            // await GameObject.Find("UIRoot/Common/IntroductionPanel")
            //     .GetComponent<CanvasGroup>().DOFade(1, 1).ToUniTask();
            //
            // await UniTask.Delay(TimeSpan.FromSeconds(3));
            //
            // await GameObject.Find("UIRoot/Common/IntroductionPanel")
            //     .GetComponent<CanvasGroup>().DOFade(0, 2).ToUniTask();

                #endregion
                
            _uIkit = GameFacade.Instance.GetInstance<IUIkit>();
            _uIkit.OpenPanel("Login");
            
            //for synchronize scene
            base.StateStart();
        }

        public override void StateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StateController.SetState(new MainState(StateController)).Forget();
            }
        }
    }
}