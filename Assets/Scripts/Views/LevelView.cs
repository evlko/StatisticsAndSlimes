using System.Collections.Generic;
using Gameplay;
using Localization;
using TMPro;
using UnityEngine;
using Models;

namespace Views
{
    public class LevelView : View
    {
        [SerializeField] private List<LocalizedText> levelTexts;

        private void Awake()
        {
            LevelManager.ShowLevelData += ShowLevelView;
        }

        private void ShowLevelView(LevelData levelData)
        {
            foreach (var levelText in levelTexts)
            {
                levelText.LocalizationKey = levelData.levelKey;
            }
        }
    }
}