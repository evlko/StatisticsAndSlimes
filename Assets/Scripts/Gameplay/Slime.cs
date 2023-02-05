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
        private Vector2 _savedPosition;

        public SlimeData SlimeData => _slimeData;

        public static Action<SlimeData> ShowSlimeData;
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
            this.gameObject.AddComponent<Dragging>();
        }

        private void OnMouseDown()
        {
            _savedPosition = this.transform.position;
        }

        private void OnMouseUp()
        {
            if (Vector2.Distance(this.transform.position, _savedPosition) < 0.5f)
            {
                ShowData();
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

        private void ShowData()
        {
            ShowSlimeData?.Invoke(_slimeData);
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
