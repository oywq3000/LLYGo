using UnityEngine;

namespace Script.SceneState
{
    public class StartSceneState:ISceneState
    {
        public StartSceneState(SceneStateController stateController) : base("Start", stateController)
        {
        }

        public override void StateUpdate()
        {
            Debug.Log("start");
        }
    }
}