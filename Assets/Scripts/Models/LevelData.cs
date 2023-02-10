using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 2)]
    public class LevelData : ScriptableObject
    {
        [Header("Level Key")]
        public string levelKey;
        
        [Header("Data")]
        public List<SlimeData> initialActiveSmiles;
        public List<SlimeData> initialStoredSlimes;
        public List<ConditionData> conditions;

        [Header("Objects")] 
        public List<InteractionObject> interactionObjects;
    }
}