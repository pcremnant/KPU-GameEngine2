using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class SceneManager
    {
        private Stack<GameScene> scenes;

        public void Initialize(GameScene initScene)
        {
            scenes = new Stack<GameScene>();
            scenes.Push(initScene);
            if (!initScene.initialzied)
                initScene.Initialize();
        }

        public void Push(GameScene newScene)
        {
            if (scenes.Count > 0)
                scenes.Peek().Pause();
            scenes.Push(newScene);
            if (!newScene.initialzied)
                newScene.Initialize();
        }

        public bool Pop()
        {
            if (scenes.Count <= 1)
                return false;

            var scene = scenes.Pop();
            scene.DestroyScene();

            scenes.Peek().Resume();
            return true;
        }

        public void SceneUpdate()
        {
            var scene = scenes.Peek();
            if (scene.initialzied)
                scene.SceneUpdate();
        }

        public Scene GetCurrentScene()
        {
            return scenes.Peek().MyScene;
        }
    }
}