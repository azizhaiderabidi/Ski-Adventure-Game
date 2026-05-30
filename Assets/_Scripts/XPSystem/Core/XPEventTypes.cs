public class StageCompleteEvent
{
    public string Message;
    public StageCompleteEvent(string message) => Message = message;
}

public class SceneLoadRequestedEvent
{
    public string SceneName;
    public SceneLoadRequestedEvent(string sceneName) => SceneName = sceneName;
}
