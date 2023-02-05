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

        private void Awake()
        {
            Slime.ShowSlimeData += ShowSlimeView;
        }

        private void ShowSlimeView(SlimeData slimeData)
        {
            ActivateView(true);
            slimeNameText.text = slimeData.slimeName;
            statHappinessText.text = slimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Happiness].ToString();
            statSlipperinessText.text = slimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Slipperiness].ToString();
        }
    }
}
