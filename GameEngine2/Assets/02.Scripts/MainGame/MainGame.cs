using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameState gameState => _gameState;
    private GameState _gameState;

    [SerializeField] public SceneManager SceneManager => SceneManager;
    private SceneManager sceneManager;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = new SceneManager();
        sceneManager.Initialize(new StageSelectScene());
    }

    // Update is called once per frame
    void Update()
    {
        sceneManager.SceneUpdate();
    }
}
