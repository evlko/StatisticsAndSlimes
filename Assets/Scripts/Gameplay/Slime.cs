using System;
using Interfaces;
using Models;
using UnityEngine;

namespace Gameplay
{
    public class Slime : MonoBehaviour, IColorable, IDestroyable
    {
        private SlimeData _slimeData;
        private SlimePool _slimePool;
        private SpriteRenderer _spriteRenderer;
        private Dragging _dragging;

        public SlimeData SlimeData => _slimeData;

        public static Action<SlimeData> ShowSlimeData;
        public static Action HideSlimeData;
        public static Action SlimeFeatureChanged;

        public void Init(SlimeData slimeData, SlimePool slimePool)
        {
            _slimeData = slimeData;
            _slimePool = slimePool;
            Color(_slimeData.color);
            RegisterStats();
        }

        private void Awake()
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
            _dragging = this.gameObject.AddComponent<Dragging>();
        }

        private void OnMouseEnter()
        {
            ShowSlimeData?.Invoke(_slimeData);
        }

        private void OnMouseExit()
        {
            if (!_dragging.isDragging)
            {
                HideSlimeData?.Invoke();
            }
        }

        public void Color(Color newColor)
        {
            _spriteRenderer.color = newColor;
            UpdateSlimeCategoricalFeature(SlimeCategoricalFeatures.Color, newColor.ToString());
        }

        public void Color(float alpha)
        {
            var currentColor = _spriteRenderer.color;
            currentColor.a = alpha;
            _spriteRenderer.color = currentColor;
        }

        public void Destroy()
        {
            _slimePool.RemoveSlime(this);
        }

        private void RegisterStats()
        {
            _slimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Happiness] = _slimeData.happiness;
            _slimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Slipperiness] = _slimeData.slipperiness;
        }
        
        public void UpdateSlimeQuantitativeFeature(SlimeQuantitativeFeatures slimeQuantitativeFeature, float value)
        {
            _slimeData.QuantitativeFeatures[slimeQuantitativeFeature] += value;
            SlimeFeatureChanged?.Invoke();
        }

        public void UpdateSlimeCategoricalFeature(SlimeCategoricalFeatures slimeCategoricalFeature, string value)
        {
            _slimeData.CategoricalFeatures[slimeCategoricalFeature] = value;
            SlimeFeatureChanged?.Invoke();
        }
    }
}
