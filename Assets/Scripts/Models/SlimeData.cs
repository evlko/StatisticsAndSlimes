using System;
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
        public ColorData color;
        [Header("Quantitative Features")]
        public List<QuantitativeFeature> quantitativeFeature;
        [Header("Visualization")] 
        public Sprite body;
        public Sprite eyes;
        public Sprite horns;
        public Sprite mouth;
        
        [Serializable] public struct QuantitativeFeature
        {
            public SlimeQuantitativeFeatures slimeQuantitativeFeature;
            public float value;
        }

        public readonly Dictionary<SlimeQuantitativeFeatures, float> QuantitativeFeatures = new Dictionary<SlimeQuantitativeFeatures, float>();
        public readonly Dictionary<SlimeCategoricalFeatures, string> CategoricalFeatures = new Dictionary<SlimeCategoricalFeatures, string>();

        private void OnEnable()
        {
            foreach (var feature in quantitativeFeature)
            {
                QuantitativeFeatures[feature.slimeQuantitativeFeature] = feature.value;
            }

            CategoricalFeatures[SlimeCategoricalFeatures.Color] = color.color.ToString();
        }

        public bool Equals(SlimeData otherData)
        {
            if ((object)otherData == null)
            {
                return false;
            }

            if (this.slimeName != otherData.slimeName)
            {
                return false;
            }

            foreach(var key in QuantitativeFeatures.Keys)
            {
                if (this.QuantitativeFeatures[key] != otherData.QuantitativeFeatures[key])
                {
                    return false;
                }
            }
            
            foreach(var key in CategoricalFeatures.Keys)
            {
                if (this.CategoricalFeatures[key] != otherData.CategoricalFeatures[key])
                {
                    Debug.Log(key);
                    Debug.Log(this.CategoricalFeatures[key]);
                    Debug.Log(otherData.CategoricalFeatures[key]);
                    return false;
                }
            }

            return true;
        }
    }

    public enum SlimeQuantitativeFeatures
    {
        Sweetness,
        Slipperiness
    }

    public enum SlimeCategoricalFeatures
    {
        Color
    }
}