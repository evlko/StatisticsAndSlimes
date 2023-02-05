using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Chapter", menuName = "ScriptableObjects/Chapter", order = 4)]
    public class ChapterData : ScriptableObject
    {
        [Header("Levels")]
        public List<LevelData> levels;
    }
}