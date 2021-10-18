using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameState gameState => _gameState;
    private GameState _gameState;

    // �ӽ� Ȯ�ο� �ڵ�
    [SerializeField] StageSelectScene selectScene;

    public GameScene curScene => _curScene;
    private GameScene _curScene;

    private void Awake()
    {
        // �������� ���� �׽�Ʈ
        // _curScene = new StageSelectScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        selectScene.Initialize();
        // _curScene.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // _curScene.
    }
}
