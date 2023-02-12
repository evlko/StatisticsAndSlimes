using TMPro;
using UnityEngine;

namespace Localization
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private LocalizationGroup localizationGroup;
        [SerializeField] private string localizationKey;
        private TextMeshProUGUI _textMeshPro;

        public string LocalizationKey
        {
            set
            {
                localizationKey = value;
                GetTextValue();
            }
        }

        private void Awake()
        {
            _textMeshPro = this.GetComponent<TextMeshProUGUI>();
            Localization.LoadLocalized += GetTextValue;
        }

        private void OnEnable()
        {
            GetTextValue();
        }

        private void GetTextValue()
        {
            if (_textMeshPro == null)
            {
                _textMeshPro = this.GetComponent<TextMeshProUGUI>();
            }
            _textMeshPro.text = Localization.GetValueByKey(localizationGroup, localizationKey);
        }
    }
}
