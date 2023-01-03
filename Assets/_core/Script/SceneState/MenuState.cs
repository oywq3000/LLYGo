using _core.Script.Music;
using Script.UI;
using UnityEngine;

namespace SceneStateRegion
{
    public class MenuState:AbstractState
    {
        private IUIkit _uiKit;
        public MenuState(SceneStateController stateController) : base("Menu", stateController)
        {
        }

        public override void StateStart()
        {
            _uiKit =GameFacade.Instance.GetInstance<IUIkit>();
            
            _uiKit.OpenPanel("MenuPanel");
            
            base.StateStart();
            
            //play bgm for menu
            AudioManager.Instance.PlayBGM("MenuBgm");
        }

        public override void StateUpdate()
        {
         
            
            base.StateUpdate();
        }

        public override void StateEnd()
        {
           
            AudioManager.Instance.StopBGM();
            base.StateEnd();
        }
    }
}