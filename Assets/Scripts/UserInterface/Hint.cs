using System.Collections;
using Gameplay;
using Localization;
using UnityEngine;

namespace UserInterface
{
    public class Hint : MonoBehaviour
    {
        private LocalizedText _localizedText;

        private void Awake()
        {
            _localizedText = this.GetComponentInChildren<LocalizedText>();
            SlimePool.ShowPoolHint += ShowHint;
            this.gameObject.SetActive(false);
        }

        private void ShowHint(string hintKey)
        {
            this.gameObject.SetActive(true);
            _localizedText.LocalizationKey = hintKey;
            StartCoroutine(HideHint());
        }

        private IEnumerator HideHint()
        {
            yield return new WaitForSecondsRealtime(3);
            this.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            SlimePool.ShowPoolHint -= ShowHint;
        }
    }
}
