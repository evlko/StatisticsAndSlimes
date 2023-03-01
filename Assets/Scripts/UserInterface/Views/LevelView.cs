using System.Collections.Generic;
using Gameplay;
using Localization;
using UnityEngine;
using Models;
using UnityEngine.UI;

namespace Views
{
    public class LevelView : View
    {
        [SerializeField] private List<LocalizedText> levelTexts;
        [SerializeField] private Image completedLevelPanel;

        private void Awake()
        {
            LevelManager.ShowLevelData += ShowLevelView;
            LevelManager.LevelCompleted += ShowCompletedLevelView;
        }

        private void ShowLevelView(LevelData levelData)
        {
            foreach (var levelText in levelTexts)
            {
                levelText.LocalizationKey = levelData.levelKey;
            }
        }

        private void ShowCompletedLevelView()
        {
            if (completedLevelPanel != null)
            {
                completedLevelPanel.gameObject.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            LevelManager.ShowLevelData -= ShowLevelView;
            LevelManager.LevelCompleted -= ShowCompletedLevelView;
        }
    }
}