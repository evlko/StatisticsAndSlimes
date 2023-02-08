using System;
using UnityEngine;
using Models;

namespace Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private SlimePool slimePool;
        [SerializeField] private ChapterData chapterData;
        
        private LevelData _levelData;
        private int _currentLevel;

        public static Action<LevelData> ShowLevelData;
        public static Action LevelCompleted;
        
        private void Awake()
        {
            if (!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level", 0);
            }

            _currentLevel = PlayerPrefs.GetInt("Level");
            
            _levelData = chapterData.levels[_currentLevel];
            
            slimePool.BuildPool(_levelData.initialStoredSlimes, _levelData.initialActiveSmiles);
            
            // TODO: it's better to try one more time to implement kinda ICondition interface
            SlimePool.SlimesPoolChanged += CheckConditions;
            Slime.SlimeFeatureChanged += CheckConditions;
            
            ShowLevelData?.Invoke(_levelData);
        }

        private void CheckConditions()
        {
            Debug.Log("Checkin for level " + _currentLevel.ToString());
            Debug.Log(SlimePool.ActiveSlimes.Count);
            if (ConditionsTracker.AllConditionsAreSatisfied(SlimePool.ActiveSlimes, _levelData.conditions))
            {
                SlimePool.SlimesPoolChanged -= CheckConditions;
                Slime.SlimeFeatureChanged -= CheckConditions;
                _currentLevel += 1;
                PlayerPrefs.SetInt("Level", _currentLevel);
                LevelCompleted?.Invoke();
            };
        }
    }
}
