using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    //scene current state
    private ISceneState _sceneState;

    //await scene loading 
    private bool _canUpdate = false;

    //set scene state
    public async UniTask SetState(ISceneState state, bool isLoadScene = true,bool isfirst = false)
    {
        _canUpdate = false;

        //if not first then release the lasted state
        _sceneState?.StateEnd();

        if (isfirst)
        {
            //if this scene is first of whole game than don't load scene
            _sceneState = state;
            _sceneState.StateStart();

            _canUpdate = true;
            return;
        }
        
        //judge if load scene
        if (isLoadScene)
        {
            //entry loading scene
            await this.SetState(new LoadingState(this, state.SceneName), false);
        }
        else
        {
            //update current scene state
            _sceneState = state;
            //loading directly
            await Addressables.LoadSceneAsync(_sceneState.SceneName);
            _sceneState.StateStart();
        }

        _canUpdate = true;
    }

    /// <summary>
    /// current state real time update
    /// </summary>
    public void StateUpdate()
    {
        if (_canUpdate)
        {
            _sceneState?.StateUpdate();
        }
    }


    void LoadingScene()
    {
        var asyncOperationHandle = Addressables.LoadSceneAsync("Loading");
        var loadSceneAsync = Addressables.LoadSceneAsync(_sceneState.SceneName, LoadSceneMode.Single, false);
        var percentComplete = loadSceneAsync.PercentComplete;
    }
}