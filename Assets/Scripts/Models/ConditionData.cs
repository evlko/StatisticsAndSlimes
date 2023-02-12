using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Condition", menuName = "ScriptableObjects/Condition", order = 3)]
    public class ConditionData : ScriptableObject
    {
        public PlaygroundCharacteristicsTypes playgroundCharacteristicsTypes;
        public SlimesCharacteristics slimesCharacteristics;
        public CharacteristicTypes characteristicType;
        public SampleCharacteristics sampleCharacteristic;
        public FeatureTypes featureType;
        public SlimeQuantitativeFeatures slimeQuantitativeFeature;
        public QuantitativeFeatureCharacteristics quantitativeFeatureCharacteristic;
        public SlimeCategoricalFeatures slimeCategoricalFeature;
        public CategoricalFeatureCharacteristics categoricalFeatureCharacteristic;
        public int value;
        public List<SlimeData> aliveSlimes;
        public List<SlimeData> certainSlimes;
    }

    public enum CharacteristicTypes
    {
        Sample,
        Feature,
    }

    public enum SampleCharacteristics
    {
        Size
    }
    
    public enum SlimesCharacteristics {
        AlivedSlimes,
        CertainData
    }
    
    // TODO this is not checked and used lol
    public enum PlaygroundCharacteristicsTypes
    {
        Numerical,
        Slimes
    }
    
    public enum FeatureTypes
    {
        Quantitative,
        Categorical
    }

    public enum QuantitativeFeatureCharacteristics
    {
        Mean,
        Median,
        Mode,
        Range,
        Variance,
        FirstQuantile,
        ThirdQuantile,
        Min,
        Max,
    }

    public enum CategoricalFeatureCharacteristics
    {
        Cardinality
    }
}