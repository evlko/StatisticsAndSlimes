using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "PlaygroundLevel", menuName = "ScriptableObjects/PlaygroundLevel", order = 2)]
    public class PlaygroundLevelData : LevelData
    {
        [Header("Data")]
        public List<SlimeData> initialActiveSmiles;
        public List<SlimeData> initialStoredSlimes;
        public List<ConditionData> conditions;

        [Header("Objects")] 
        public List<InteractionObject> interactionObjects;
    }
}