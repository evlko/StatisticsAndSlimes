using Gameplay;
using Models;
using TMPro;
using UnityEngine;

namespace Views
{
    public class SlimeView : View
    {
        [SerializeField] private TextMeshProUGUI slimeNameText;
        [SerializeField] private TextMeshProUGUI statHappinessText;
        [SerializeField] private TextMeshProUGUI statSlipperinessText;

        private SlimeData _slimeData;

        private void Awake()
        {
            Slime.ShowSlimeData += ShowSlimeView;
            Slime.SlimeFeatureChanged += UpdateSlimeView;
            Slime.HideSlimeData += HideSlimeView;
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
            slimeNameText.text = _slimeData.slimeName;
            statHappinessText.text = _slimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Happiness].ToString();
            statSlipperinessText.text = _slimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Slipperiness].ToString();
        }

        private void HideSlimeView()
        {
            _slimeData = null;
            ActivateView(false);
        }
    }
}
