using Gameplay;

namespace UserInterface
{
    public class SceneSelectorGameplay : SceneSelector
    {
        private void Awake()
        {
            PlaygroundManager.LevelCompleted += ShowButton;
            this.gameObject.SetActive(false);
        }
        
        private void ShowButton()
        {
            this.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            PlaygroundManager.LevelCompleted -= ShowButton;
        }
    }
}
