using UnityEngine;

public class GameScene : MonoBehaviour
{
    protected float _sceneTimer = 0f;

    public bool endScene => _endScene;
    protected bool _endScene = false;


    // æ¿ √ ±‚»≠
    public virtual void Initialize()
    {
        _endScene = false;
        _sceneTimer = 0;
    }

    // º“∏Í¿⁄
    public virtual void DestroyScene()
    {

    }
}
