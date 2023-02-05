using Gameplay;
using TMPro;
using UnityEngine;
using Models;

namespace Views
{
    public class LevelView : View
    {
        [SerializeField] private TextMeshProUGUI mainText;
        [SerializeField] private TextMeshProUGUI pythonHintText;
        [SerializeField] private TextMeshProUGUI tablesHintText;
        [SerializeField] private TextMeshProUGUI theoryText;

        private void Awake()
        {
            LevelManager.ShowLevelData += ShowLevelView;
        }

        private void ShowLevelView(LevelData levelData)
        {
            mainText.text = levelData.levelText;
            pythonHintText.text = levelData.pythonHint;
            tablesHintText.text = levelData.tablesHint;
            theoryText.text = levelData.theoryText;
        }
    }
}