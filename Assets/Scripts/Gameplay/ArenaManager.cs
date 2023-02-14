using Models;
using UnityEngine;
using UserInterface;

namespace Gameplay
{
    public class ArenaManager : LevelManager
    {
        [SerializeField] private int maxHealth;
        private ArenaLevelData _arenaLevelData;
        private int _currentHealth;

        protected override void InitLevel()
        {
            if (!PlayerPrefs.HasKey("ArenaHealth"))
            {
                PlayerPrefs.SetInt("ArenaHealth", maxHealth);
                _currentHealth = maxHealth;
            }
            else
            {
                _currentHealth = PlayerPrefs.GetInt("ArenaHealth");
            }
            
            _arenaLevelData = Instantiate((ArenaLevelData) chapterData.levels[CurrentLevel]);
            
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
                _currentHealth -= 1;
            }
        }
    }
}
