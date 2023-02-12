using UnityEngine;

namespace UserInterface
{
    public class LanguageButton : MonoBehaviour
    {
        public void ChangeLanguage()
        {
            Localization.Localization.SwitchLocalization();
        }
    }
}
