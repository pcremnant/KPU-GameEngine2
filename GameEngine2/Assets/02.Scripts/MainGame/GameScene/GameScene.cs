using UnityEngine;
public enum Scene
{
    Main = 0,
    StageSelect = 1,
    Playing = 2,
    Pause = 3,
    Result = 4
}

public class GameScene : MonoBehaviour
{
    protected float _sceneTimer = 0f;

    public bool endScene => _endScene;
    protected bool _endScene = false;

    public bool paused => _paused;
    protected bool _paused = false;

    public bool initialzied => _initialized;
    protected bool _initialized = false;

    public Scene MyScene => _myScene;
    protected Scene _myScene;
    
    public virtual void CreateScene()
    {

    }

    // æ¿ √ ±‚»≠
    public virtual void Initialize()
    {
        _paused = false;
        _endScene = false;
        _initialized = true;
        _sceneTimer = 0;
    }

    public virtual void SceneUpdate()
    {
        _sceneTimer += Time.deltaTime;
    }

    // º“∏Í¿⁄
    public virtual void DestroyScene()
    {
        _endScene = true;
        _initialized = false;
    }

    public virtual void Pause()
    {
        _paused = true;
    }

    public virtual void Resume()
    {
        _paused = true;
    }
}