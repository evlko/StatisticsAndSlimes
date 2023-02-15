using System;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UserInterface;

namespace Gameplay
{
    public class ArenaManager : LevelManager
    {
        [SerializeField] private int maxHealth;
        private ArenaLevelData _arenaLevelData;
        private int _currentHealth;
        private string _arenaHealthPlayerPref = "ArenaHealth";

        public static Action<int> ShowHealth;

        private int CurrenHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                PlayerPrefs.SetInt(_arenaHealthPlayerPref, _currentHealth);
                ShowHealth?.Invoke(maxHealth - _currentHealth);
                if (_currentHealth == 0)
                {
                    Defeat();
                }
            }
        }

        protected override void InitLevel()
        {
            CurrenHealth = !PlayerPrefs.HasKey(_arenaHealthPlayerPref)
                ? maxHealth
                : PlayerPrefs.GetInt(_arenaHealthPlayerPref);

            _arenaLevelData = Instantiate((ArenaLevelData)chapterData.levels[CurrentLevel]);

            slimePool.BuildPool(_arenaLevelData.slimes);

            // TODO: it's better to try one more time to implement kinda ICondition interface
            ArenaResultInput.ConditionChecked += CheckConditions;

            ShowLevelData?.Invoke(_arenaLevelData);
        }

        private void CheckConditions(float result)
        {
            if (result == _arenaLevelData.result)
            {
                ArenaResultInput.ConditionChecked -= CheckConditions;
                CompleteLevel();
            }
            else
            {
                CurrenHealth -= 1;
            }
        }

        private void Defeat()
        {
            ChapterCleared();
            PlayerPrefs.SetInt(_arenaHealthPlayerPref, maxHealth);
            SceneManager.LoadScene("MainMenu");
        }

        private void OnDestroy()
        {
            ArenaResultInput.ConditionChecked -= CheckConditions;
        }
    }
}