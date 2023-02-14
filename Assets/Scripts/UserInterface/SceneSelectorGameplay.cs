using Gameplay;

namespace UserInterface
{
    public class SceneSelectorGameplay : SceneSelector
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

        private void OnDestroy()
        {
            LevelManager.LevelCompleted -= ShowButton;
        }
    }
}
