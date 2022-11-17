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
    public virtual void StateStart()
    {
        //if you want inherit it, you must preserve it at the last line of override method
        GameLoop.Instance.Set(true);
    }
    
    //called when update info for every frame
    public virtual void StateUpdate() { }
    
    //called when leaves this state
    public virtual void StateEnd()
    {
        GameLoop.Instance.Set(false);
    }
}