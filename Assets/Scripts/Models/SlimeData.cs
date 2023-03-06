using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Slime", menuName = "ScriptableObjects/Slime", order = 1)]
    public class SlimeData : ScriptableObject
    {
        [SerializeField] private string slimeName;
        [Header("Categorical Features")]
        [SerializeField] private ColorData color;
        [SerializeField] private SlimeType slimeType;
        [Header("Quantitative Features")]
        [SerializeField] private List<QuantitativeFeature> quantitativeFeature;
        [Header("Visualization")] 
        [SerializeField] private Sprite body;

        public string SlimeName => slimeName;
        public ColorData Color => color;
        public Sprite Body => body;

        [Serializable] public struct QuantitativeFeature
        {
            [SerializeField] private SlimeQuantitativeFeatures slimeQuantitativeFeature;
            [SerializeField] private float value;

            public SlimeQuantitativeFeatures SlimeCategoricalFeatures => slimeQuantitativeFeature;
            public float Value => value;
        }

        public readonly Dictionary<SlimeQuantitativeFeatures, float> QuantitativeFeatures = new Dictionary<SlimeQuantitativeFeatures, float>();
        public readonly Dictionary<SlimeCategoricalFeatures, string> CategoricalFeatures = new Dictionary<SlimeCategoricalFeatures, string>();

        private void OnEnable()
        {
            foreach (var feature in quantitativeFeature)
            {
                QuantitativeFeatures[feature.SlimeCategoricalFeatures] = feature.Value;
            }

            CategoricalFeatures[SlimeCategoricalFeatures.Color] = color.color.ToString();
            CategoricalFeatures[SlimeCategoricalFeatures.Type] = slimeType.ToString();
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
        Color,
        Type
    }

    public enum SlimeType
    {
        Domestic,
        Wild
    }
}