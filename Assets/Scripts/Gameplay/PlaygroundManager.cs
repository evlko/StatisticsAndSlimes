using UnityEngine;
using Models;

namespace Gameplay
{
    public class PlaygroundManager : LevelManager
    {
        [SerializeField] private LevelBuilder levelBuilder;
        
        private PlaygroundLevelData _playgroundLevelData;

        protected override void InitLevel()
        {
            _playgroundLevelData = Instantiate((PlaygroundLevelData) chapterData.levels[CurrentLevel]);
            
            slimePool.BuildPool(_playgroundLevelData.initialStoredSlimes, _playgroundLevelData.initialActiveSmiles);
            levelBuilder.BuildLevel(_playgroundLevelData.interactionObjects);
            
            // TODO: it's better to try one more time to implement kinda ICondition interface
            SlimePool.SlimesPoolChanged += CheckConditions;
            Slime.SlimeFeatureChanged += CheckConditions;
            
            ShowLevelData?.Invoke(_playgroundLevelData);
        }

        private void OnDestroy()
        {
            SlimePool.SlimesPoolChanged -= CheckConditions;
            Slime.SlimeFeatureChanged -= CheckConditions;
        }

        private void CheckConditions()
        {
            if (ConditionsTracker.AllConditionsAreSatisfied(SlimePool.ActiveSlimes, _playgroundLevelData.conditions))
            {
                SlimePool.SlimesPoolChanged -= CheckConditions;
                Slime.SlimeFeatureChanged -= CheckConditions;
                CompleteLevel();
            };
        }
    }
}
