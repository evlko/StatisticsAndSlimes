using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Gameplay
{
    public static class Calculator
    {
        public static int SampleSize(List<Slime> slimes)
        {
            return slimes.Count;
        }
        
        public static float FeatureMin(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return slimes.Min(slime => slime.SlimeData.QuantitativeFeatures[quantitativeFeature]);
        }

        public static float FeatureMax(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return slimes.Max(slime => slime.SlimeData.QuantitativeFeatures[quantitativeFeature]);
        }

        public static float FeatureRange(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return FeatureMax(slimes, quantitativeFeature) - FeatureMin(slimes, quantitativeFeature);
        }

        public static float FeatureMean(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return slimes.Average(slime => slime.SlimeData.QuantitativeFeatures[quantitativeFeature]);
        }

        public static float FeatureMedian(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var size = SampleSize(slimes);
            slimes = slimes.OrderBy(slime=>slime.SlimeData.QuantitativeFeatures[quantitativeFeature]).ToList();

            if (size % 2 == 0)
            {
                return (slimes[size / 2].SlimeData.QuantitativeFeatures[quantitativeFeature] +
                        slimes[(size / 2) - 1].SlimeData.QuantitativeFeatures[quantitativeFeature]) / 2;
            }

            return slimes[(size - 1) / 2].SlimeData.QuantitativeFeatures[quantitativeFeature];
        }

        public static float FeatureMode(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            return slimes
                .GroupBy(x => x.SlimeData.QuantitativeFeatures[quantitativeFeature])
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }

        public static int FeatureModeNumber(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var groups = slimes
                .GroupBy(x => x.SlimeData.QuantitativeFeatures[quantitativeFeature])
                .Select(g => new { Value = g.Key, Count = g.Count() })
                .ToList();
            var maxCount = groups.Max(g => g.Count);
            var modes = groups
                .Where(g => g.Count == maxCount);
            return modes.Count();
        }

        public static float FeatureQuantileFirst(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var median = FeatureMedian(slimes, quantitativeFeature);
            slimes = slimes.Where(slime => slime.SlimeData.QuantitativeFeatures[quantitativeFeature] < median).ToList();
            return FeatureMedian(slimes, quantitativeFeature);
        }
        
        public static float FeatureQuantileThird(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var median = FeatureMedian(slimes, quantitativeFeature);
            slimes = slimes.Where(slime => slime.SlimeData.QuantitativeFeatures[quantitativeFeature] > median).ToList();
            return FeatureMedian(slimes, quantitativeFeature);
        }

        public static float FeatureVariance(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
        {
            var mean = FeatureMean(slimes, quantitativeFeature);
            var size = SampleSize(slimes) - 1;
            var var = 0f;    
            
            foreach (var slime in slimes)
            {
                var += (float) Math.Pow(slime.SlimeData.QuantitativeFeatures[quantitativeFeature] - mean, 2);
            }

            return var / size;
        }

        public static float FeatureStDev(List<Slime> slimes, SlimeQuantitativeFeatures quantitativeFeature)
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
