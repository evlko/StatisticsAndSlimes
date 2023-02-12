using System;
using System.Collections.Generic;
using Interfaces;
using Models;
using UnityEngine;

namespace Gameplay
{
    public class Slime : MonoBehaviour, IColorable, IDestroyable
    {
        private SlimeData _slimeData;
        private SlimePool _slimePool;
        private Animator _animator;
        private Dragging _dragging;
        private Transform _transform;
        
        [Header("Visual")]
        [SerializeField] private SpriteRenderer bodySpriteRenderer;
        [SerializeField] private SpriteRenderer eyesSpriteRenderer;
        [SerializeField] private SpriteRenderer mouthSpriteRenderer;
        [SerializeField] private SpriteRenderer hornsSpriteRenderer;
        private List<SpriteRenderer> _spriteRenderers = new List<SpriteRenderer>();
        private Color _currentColor;

        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int Slipperiness = Animator.StringToHash("slipperiness");
        
        public SlimeData SlimeData => _slimeData;

        public static Action<SlimeData> ShowSlimeData;
        public static Action HideSlimeData;
        public static Action SlimeFeatureChanged;

        public void Init(SlimeData slimeData, SlimePool slimePool)
        {
            _slimeData = slimeData;
            _slimePool = slimePool;

            bodySpriteRenderer.sprite = _slimeData.body;
            eyesSpriteRenderer.sprite = _slimeData.eyes;
            mouthSpriteRenderer.sprite = _slimeData.mouth;
            hornsSpriteRenderer.sprite = _slimeData.horns;

            _spriteRenderers = new List<SpriteRenderer>()
            {
                bodySpriteRenderer,
                eyesSpriteRenderer,
                mouthSpriteRenderer,
                hornsSpriteRenderer
            };

            _currentColor = _slimeData.color.color;
            
            Color(_currentColor);
            UpdateQuantitativeFeatureView(SlimeQuantitativeFeatures.Sweetness);
            UpdateQuantitativeFeatureView(SlimeQuantitativeFeatures.Slipperiness);
        }

        private void Awake()
        {
            _transform = this.GetComponent<Transform>();
            _animator = this.GetComponentInChildren<Animator>();
            _dragging = this.gameObject.AddComponent<Dragging>();

            LevelManager.LevelCompleted += Jump;
        }

        private void Jump()
        {
            if (_animator != null)
            {
                _animator.SetBool(IsJumping, true);
            }
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

        public void Color(Color color)
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = color;
            }
            UpdateSlimeCategoricalFeature(SlimeCategoricalFeatures.Color, color.ToString());
        }

        public void Color(float alpha)
        {
            _currentColor.a = alpha;
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = _currentColor;
            }
        }

        public void Destroy()
        {
            _slimePool.RemoveSlime(this);
        }

        public void UpdateSlimeQuantitativeFeature(SlimeQuantitativeFeatures slimeQuantitativeFeature, float value)
        {
            _slimeData.QuantitativeFeatures[slimeQuantitativeFeature] += value;
            UpdateQuantitativeFeatureView(slimeQuantitativeFeature);
            SlimeFeatureChanged?.Invoke();
        }

        public void UpdateSlimeCategoricalFeature(SlimeCategoricalFeatures slimeCategoricalFeature, string value)
        {
            _slimeData.CategoricalFeatures[slimeCategoricalFeature] = value;
            SlimeFeatureChanged?.Invoke();
        }

        private void UpdateQuantitativeFeatureView(SlimeQuantitativeFeatures slimeQuantitativeFeature)
        {
            var value = _slimeData.QuantitativeFeatures[slimeQuantitativeFeature];
            
            switch (slimeQuantitativeFeature)
            {
                case SlimeQuantitativeFeatures.Sweetness:
                    var sweetnessParam = Math.Min(1 + Math.Max(0, (value - 10) / 100), 2);
                    _transform.localScale = new Vector2(sweetnessParam, sweetnessParam);
                    break;
                case SlimeQuantitativeFeatures.Slipperiness:
                    var slipperinessParam = Math.Min(Math.Max(0.1f, 1 - (value - 10) / 100), 1);
                    _animator.SetFloat(Slipperiness, slipperinessParam);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(slimeQuantitativeFeature), slimeQuantitativeFeature, null);
            }
        }
    }
}
