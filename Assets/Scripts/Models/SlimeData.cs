using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Slime", menuName = "ScriptableObjects/Slime", order = 1)]
    public class SlimeData : ScriptableObject
    {
        [Header("Categorical Features")]
        public string slimeName;
        public ColorsStorage.ColorNames color;
        [Header("Quantitative Features")]
        public float happiness;
        public float slipperiness;

        public Dictionary<SlimeQuantitativeFeatures, float> QuantitativeFeatures = new Dictionary<SlimeQuantitativeFeatures, float>();
        public Dictionary<SlimeCategoricalFeatures, string> CategoricalFeatures = new Dictionary<SlimeCategoricalFeatures, string>();
    }

    public enum SlimeQuantitativeFeatures
    {
        Happiness,
        Slipperiness
    }

    public enum SlimeCategoricalFeatures
    {
        Color
    }
}