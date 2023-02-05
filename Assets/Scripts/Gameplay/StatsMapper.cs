using System.Collections.Generic;
using UnityEngine;
using Models;

namespace Gameplay
{
    public class StatsMapper : MonoBehaviour
    {
        [System.Serializable]
        public struct StatColor
        {
            public SlimeQuantitativeFeatures feature;
            public Color color;
        }

        public List<StatColor> ColorsToStats;
    }
}
