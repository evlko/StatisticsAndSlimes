using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Views
{
    public class SceneSelector : MonoBehaviour
    {
        private void Awake()
        {
            LevelManager.LevelCompleted += ShowButton;
            this.gameObject.SetActive(false);
        }

        private void ShowButton()
        {
            this.gameObject.SetActive(true);
        }
        
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
