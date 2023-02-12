using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Gameplay
{
    public static class ConditionsTracker
    {
        public static bool AllConditionsAreSatisfied(List<Slime> slimes, List<ConditionData> conditions)
        {
            var slimesData = new List<SlimeData>();
            foreach (var slime in slimes)
            {
                slimesData.Add(slime.SlimeData);
            }
            
            foreach (var condition in conditions)
            {
                if (condition.playgroundCharacteristicsTypes == PlaygroundCharacteristicsTypes.Slimes)
                {
                    switch (condition.slimesCharacteristics)
                    {
                        case SlimesCharacteristics.AlivedSlimes:
                            if (slimesData.Count != condition.aliveSlimes.Count)
                            {
                                return false;
                            }
                            
                            if (slimesData.Any(slime => !condition.aliveSlimes.Contains(slime)))
                            {
                                return false;
                            }
                            break;
                        case SlimesCharacteristics.CertainData:
                            foreach (var expectedSlime in condition.certainSlimes)
                            {
                                if (!slimesData.Any(slime => slime.Equals(expectedSlime)))
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    float result = condition.value - 1;

                    if (condition.characteristicType == CharacteristicTypes.Sample)
                    {
                        switch (condition.sampleCharacteristic)
                        {
                            case SampleCharacteristics.Size:
                                result = Calculator.SampleSize(slimesData);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    
                    else if (condition.characteristicType == CharacteristicTypes.Feature)
                    {
                        if (condition.featureType == FeatureTypes.Quantitative)
                        {
                            switch (condition.quantitativeFeatureCharacteristic)
                            {
                                case QuantitativeFeatureCharacteristics.Range:
                                    result = Calculator.FeatureRange(slimesData, condition.slimeQuantitativeFeature);
                                    break;
                                case QuantitativeFeatureCharacteristics.Mean:
                                    result = Calculator.FeatureMean(slimesData, condition.slimeQuantitativeFeature);
                                    break;
                                case QuantitativeFeatureCharacteristics.Median:
                                    result = Calculator.FeatureMedian(slimesData, condition.slimeQuantitativeFeature);
                                    break;
                                case QuantitativeFeatureCharacteristics.Mode:
                                    break;
                                case QuantitativeFeatureCharacteristics.Variance:
                                    break;
                                case QuantitativeFeatureCharacteristics.Max:
                                    result = Calculator.FeatureMax(slimesData, condition.slimeQuantitativeFeature);
                                    break;
                                case QuantitativeFeatureCharacteristics.Min:
                                    result = Calculator.FeatureMin(slimesData, condition.slimeQuantitativeFeature);
                                    break;
                                case QuantitativeFeatureCharacteristics.FirstQuantile:
                                    result = Calculator.FeatureQuantileFirst(slimesData,
                                        condition.slimeQuantitativeFeature);
                                    break;
                                case QuantitativeFeatureCharacteristics.ThirdQuantile:
                                    result = Calculator.FeatureQuantileThird(slimesData,
                                        condition.slimeQuantitativeFeature);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        else
                        {
                            switch (condition.categoricalFeatureCharacteristic)
                            {
                                case CategoricalFeatureCharacteristics.Cardinality:
                                    result = Calculator.FeatureCardinality(slimes, condition.slimeCategoricalFeature);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }

                    if (result != condition.value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
