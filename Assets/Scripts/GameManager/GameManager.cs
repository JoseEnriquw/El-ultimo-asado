using Assets.Scripts.Character;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager gameManager;
        public Action<EnemyStatesEnum> OnChangeEnemyState;
        public static GameManager GetGameManager() => gameManager; 
        private int scene;

        private void Awake()
        {
            if (gameManager != null)
            {
                Destroy(gameObject);
                return;
            }
            
            gameManager = this;
            scene = 3;
            DontDestroyOnLoad(this);
        }
        public void NextScene()
        {
            scene++;
            SceneManager.LoadScene(scene);
        }

        public void GoToScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }

        public int GetSceneNumber()
        {
            return scene;
        }

        public void ChangeEnemyState(EnemyStatesEnum newState)
        {
            OnChangeEnemyState?.Invoke(newState);
        }
    }
}
