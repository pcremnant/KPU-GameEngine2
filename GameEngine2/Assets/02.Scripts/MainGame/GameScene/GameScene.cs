using UnityEngine;

public class GameScene : MonoBehaviour
{
    protected float _sceneTimer = 0f;
    // Start is called before the first frame update

    public virtual void Initialize()
    {
        _sceneTimer = 0;
    }

    public virtual void DestroyScene()
    {

    }

    public virtual void SceneUpdate()
    {

    }

}
