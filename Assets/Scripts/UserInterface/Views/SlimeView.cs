using System;
using Gameplay;
using Localization;
using Models;
using TMPro;
using UnityEngine;

namespace Views
{
    public class SlimeView : View
    {
        [SerializeField] private TextMeshProUGUI slimeNameText;
        [SerializeField] private TextMeshProUGUI statSweetnessText;
        [SerializeField] private TextMeshProUGUI statSlipperinessText;
        [SerializeField] private LocalizedText statTypeText;

        private SlimeData _slimeData;

        private void Awake()
        {
            Slime.ShowSlimeData += ShowSlimeView;
            Slime.SlimeFeatureChanged += UpdateSlimeView;
            Slime.HideSlimeData += HideSlimeView;
            
            ActivateView(false);
        }

        private void ShowSlimeView(SlimeData slimeData)
        {
            ActivateView(true);
            _slimeData = slimeData;
            UpdateSlimeView();
        }

        private void UpdateSlimeView()
        {
            if (!_slimeData) return;
            slimeNameText.text = _slimeData.SlimeName;
            statSlipperinessText.text = _slimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Slipperiness].ToString();
            statSweetnessText.text = _slimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Sweetness].ToString();
            statTypeText.LocalizationKey = "slime_type_" +
                                           _slimeData.CategoricalFeatures[SlimeCategoricalFeatures.Type].ToString()
                                               .ToLower();
        }

        private void HideSlimeView()
        {
            _slimeData = null;
            ActivateView(false);
        }

        private void OnDestroy()
        {
            Slime.ShowSlimeData -= ShowSlimeView;
            Slime.SlimeFeatureChanged -= UpdateSlimeView;
            Slime.HideSlimeData -= HideSlimeView;
        }
    }
}
