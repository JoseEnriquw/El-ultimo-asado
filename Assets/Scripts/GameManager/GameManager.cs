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
        public Action<GameObject> OnPickUpObject;
        public Action OnPlayEnemyScream;
        public static GameManager GetGameManager() => gameManager; 

        private void Awake()
        {
            if (gameManager != null)
            {
                Destroy(gameObject);
                return;
            }
            
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void GoToScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }

        public int GetSceneNumber()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public void ChangeEnemyState(EnemyStatesEnum newState)
        {
            OnChangeEnemyState?.Invoke(newState);
        }  
        public void PickUpObject(GameObject gameObject)
        {
            OnPickUpObject?.Invoke(gameObject);
        }    
        public void PlayEnemyScream()
        {
            OnPlayEnemyScream?.Invoke();
        }
    }
}
