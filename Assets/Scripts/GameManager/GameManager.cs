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
        public Action<bool> OnChangePlayerInput;

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
            var sceneNumber= SceneManager.GetActiveScene().buildIndex + 1;
            
            SceneManager.LoadScene(sceneNumber);

            switch (sceneNumber)
            {
                case 5:
                    UIManager.GetUIManager().HideTaskPanel();
                    break;
            }
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

        public void SetEnablePlayerInput(bool enable)
        {
            OnChangePlayerInput?.Invoke(enable);
            if (enable) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }
    }
}
