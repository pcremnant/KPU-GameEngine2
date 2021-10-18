using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameState gameState => _gameState;
    private GameState _gameState;

    // �ӽ� Ȯ�ο� �ڵ�
    [SerializeField] StageSelectScene selectScene;

    public GameScene gameScene => _scenes.Peek();
    private Stack<GameScene> _scenes;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        _scenes.Push(selectScene);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
