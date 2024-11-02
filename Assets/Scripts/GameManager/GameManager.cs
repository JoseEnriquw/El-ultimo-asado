using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager gameManager;
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
            scene = 0;
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
    }
}
