using System;
using System.Collections.Generic;
using Models;

namespace Gameplay
{
    public static class ConditionsTracker
    {
        public static bool AllConditionsAreSatisfied(List<Slime> slimes, List<ConditionData> conditions)
        { 
            foreach (var condition in conditions)
            {
                float result = condition.value - 1;
                    
                if (condition.characteristicType == CharacteristicTypes.Sample)
                {
                    switch (condition.sampleCharacteristic)
                    {
                        case SampleCharacteristics.Size:
                            result = Calculator.SampleSize(slimes);
                            break;
                        case SampleCharacteristics.AlivedSlimes:
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
                                result = Calculator.FeatureRange(slimes, condition.slimeQuantitativeFeature);
                                break;
                            case QuantitativeFeatureCharacteristics.Mean:
                                result = Calculator.FeatureMean(slimes, condition.slimeQuantitativeFeature);
                                break;
                            case QuantitativeFeatureCharacteristics.Median:
                                result = Calculator.FeatureMedian(slimes, condition.slimeQuantitativeFeature);
                                break;
                            case QuantitativeFeatureCharacteristics.Mode:
                                break;
                            case QuantitativeFeatureCharacteristics.Variance:
                                break;
                            case QuantitativeFeatureCharacteristics.Max:
                                result = Calculator.FeatureMax(slimes, condition.slimeQuantitativeFeature);
                                break;
                            case QuantitativeFeatureCharacteristics.Min:
                                result = Calculator.FeatureMin(slimes, condition.slimeQuantitativeFeature);
                                break;
                            case QuantitativeFeatureCharacteristics.FirstQuantile:
                                result = Calculator.FeatureQuantileFirst(slimes, condition.slimeQuantitativeFeature);
                                break;
                            case QuantitativeFeatureCharacteristics.ThirdQuantile:
                                result = Calculator.FeatureQuantileThird(slimes, condition.slimeQuantitativeFeature);
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

            return true;
        }
    }
}
