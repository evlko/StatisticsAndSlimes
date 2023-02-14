using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "ArenaLevel", menuName = "ScriptableObjects/ArenaLevel", order = 7)]
    public class ArenaLevelData : LevelData
    {
        [Header("Data")]
        public List<SlimeData> slimes;
        public int result;
    }
}
