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
        private Animator _animator;
        private Dragging _dragging;
        private Transform _transform;
        private SpriteRenderer _spriteRenderer;
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

            _spriteRenderer.sprite = _slimeData.body;
            _currentColor = _slimeData.color.color;

            Color(_currentColor);
            UpdateQuantitativeFeatureView(SlimeQuantitativeFeatures.Sweetness);
            UpdateQuantitativeFeatureView(SlimeQuantitativeFeatures.Slipperiness);
        }

        private void Awake()
        {
            _spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
            _transform = this.GetComponent<Transform>();
            _animator = this.GetComponentInChildren<Animator>();
            _dragging = this.gameObject.AddComponent<Dragging>();

            LevelManager.LevelCompleted += Jump;
        }

        private void OnDestroy()
        {
            LevelManager.LevelCompleted -= Jump;
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
            _spriteRenderer.color = color;
            _currentColor = color;
            UpdateSlimeCategoricalFeature(SlimeCategoricalFeatures.Color, color.ToString());
        }

        public void Color(float alpha)
        {
            _currentColor.a = alpha;
            _spriteRenderer.color = _currentColor;
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
                    var sweetnessParam =
                        Math.Min(
                            GameplayConsts.SlimeMinScale + Math.Max(0,
                                (value - GameplayConsts.SlimeFeaturesDefaultValue) /
                                GameplayConsts.SlimeFeaturesDivider), GameplayConsts.SlimeMaxScale);
                    _transform.localScale = new Vector2(sweetnessParam, sweetnessParam);
                    break;
                case SlimeQuantitativeFeatures.Slipperiness:
                    var slipperinessParam =
                        Math.Min(
                            Math.Max(GameplayConsts.SlimeMinAnimationSpeed,
                                1 - (value - GameplayConsts.SlimeFeaturesDefaultValue) /
                                GameplayConsts.SlimeFeaturesDivider), GameplayConsts.SlimeMaxAnimationSpeed);
                    _animator.SetFloat(Slipperiness, slipperinessParam);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(slimeQuantitativeFeature), slimeQuantitativeFeature,
                        null);
            }
        }
    }
}