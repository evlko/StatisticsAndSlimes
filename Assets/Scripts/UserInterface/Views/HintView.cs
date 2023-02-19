using System.Collections;
using Gameplay;
using Localization;
using UnityEngine;

namespace Views
{
    public class HintView : MonoBehaviour
    {
        private LocalizedText _localizedText;

        private void Awake()
        {
            _localizedText = this.GetComponentInChildren<LocalizedText>();
            Hint.HintShowed += ShowHint;
            Hint.HintHidden += HideHint;
            
            this.gameObject.SetActive(false);
        }

        private void ShowHint(string hintKey, float time = 0)
        {
            this.gameObject.SetActive(true);
            _localizedText.LocalizationKey = hintKey;

            if (time > 0)
            {
                StartCoroutine(DelayedHideHint(time));
            }
        }

        private void HideHint()
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
        }

        private IEnumerator DelayedHideHint(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            HideHint();
        }

        private void OnDestroy()
        {
            Hint.HintShowed -= ShowHint;
            Hint.HintHidden -= HideHint;
        }
    }
}
