using UnityEngine;

public class GameScene : MonoBehaviour
{
    protected float _sceneTimer = 0f;

    public bool endScene => _endScene;
    protected bool _endScene = false;


    // �� �ʱ�ȭ
    public virtual void Initialize()
    {
        _endScene = false;
        _sceneTimer = 0;
    }

    // �Ҹ���
    public virtual void DestroyScene()
    {

    }
}
