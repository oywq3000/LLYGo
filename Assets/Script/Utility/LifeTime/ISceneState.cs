/// <summary>
/// abstract state for scene
/// </summary>
public class ISceneState
{
    //
    public string SceneName { get; }
    protected SceneStateController StateController;

    public ISceneState(string sceneName, SceneStateController stateController)
    {
        SceneName = sceneName;
        StateController = stateController;
    }

    //called when ever entry this state
    public virtual void StateStart() { }
    
    //called when update info for every frame
    public virtual void StateUpdate() { }
    
    //called when leaves this state
    public virtual void StateEnd() { }
}