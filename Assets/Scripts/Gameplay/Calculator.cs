using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Gameplay
{
    public static class Calculator
    {
        public static int SampleSize(List<SlimeData> slimes)
        {
            return slimes.Count;
        }
        
        public static float FeatureMin(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return slimes.Min(slime => slime.QuantitativeFeatures[quantitativeFeature]);
        }

        public static float FeatureMax(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return slimes.Max(slime => slime.QuantitativeFeatures[quantitativeFeature]);
        }

        public static float FeatureRange(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return FeatureMax(slimes, quantitativeFeature) - FeatureMin(slimes, quantitativeFeature);
        }

        public static float FeatureMean(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return slimes.Average(slime => slime.QuantitativeFeatures[quantitativeFeature]);
        }

        public static float FeatureMedian(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var size = SampleSize(slimes);
            slimes = slimes.OrderBy(slime=>slime.QuantitativeFeatures[quantitativeFeature]).ToList();

            if (size % 2 == 0)
            {
                return (slimes[size / 2].QuantitativeFeatures[quantitativeFeature] +
                        slimes[(size / 2) - 1].QuantitativeFeatures[quantitativeFeature]) / 2;
            }

            return slimes[(size - 1) / 2].QuantitativeFeatures[quantitativeFeature];
        }

        public static float FeatureMode(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return slimes
                .GroupBy(x => x.QuantitativeFeatures[quantitativeFeature])
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }

        public static int FeatureModeNumber(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var groups = slimes
                .GroupBy(x => x.QuantitativeFeatures[quantitativeFeature])
                .Select(g => new { Value = g.Key, Count = g.Count() })
                .ToList();
            var maxCount = groups.Max(g => g.Count);
            var modes = groups
                .Where(g => g.Count == maxCount);
            return modes.Count();
        }

        public static float FeatureQuantileFirst(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var median = FeatureMedian(slimes, quantitativeFeature);
            slimes = slimes.Where(slime => slime.QuantitativeFeatures[quantitativeFeature] < median).ToList();
            return FeatureMedian(slimes, quantitativeFeature);
        }
        
        public static float FeatureQuantileThird(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var median = FeatureMedian(slimes, quantitativeFeature);
            slimes = slimes.Where(slime => slime.QuantitativeFeatures[quantitativeFeature] > median).ToList();
            return FeatureMedian(slimes, quantitativeFeature);
        }

        public static float FeatureVariance(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var mean = FeatureMean(slimes, quantitativeFeature);
            var size = SampleSize(slimes) - 1;
            var var = 0f;    
            
            foreach (var slime in slimes)
            {
                var += (float) Math.Pow(slime.QuantitativeFeatures[quantitativeFeature] - mean, 2);
            }

            return var / size;
        }

        public static float FeatureStDev(List<SlimeData> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return (float)Math.Sqrt(FeatureVariance(slimes, quantitativeFeature));
        }

        public static int FeatureCardinality(List<Slime> slimes, SlimeCategoricalFeatures categoricalFeature)
        {
            var features = new HashSet<string>();
            foreach (var slime in slimes)
            {
                features.Add(slime.SlimeData.CategoricalFeatures[categoricalFeature]);
            }

            return features.Count;
        }
    }
}
