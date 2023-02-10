using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Localization
{
    public class Localization : MonoBehaviour
    {
        private static Dictionary<string, Dictionary<string, string>> _localizationMap =
            new Dictionary<string, Dictionary<string, string>>();

        private string currentLanguage;

        public static Action LoadLocalized;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetString("Language", "en");
            }

            currentLanguage = PlayerPrefs.GetString("Language");
            LoadLocalizationMap(currentLanguage);
        }

        private void LoadLocalizationMap(string lang)
        {
            var jsonString = Resources.Load<TextAsset>(lang).ToString();
            _localizationMap =
                JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
            LoadLocalized?.Invoke();
        }

        public static string GetValueByKey(LocalizationGroup group, string key)
        {
            return _localizationMap[group.ToString()][key];
        }

        public void SwitchLocalization()
        {
            currentLanguage = currentLanguage switch
            {
                "en" => "ru",
                "ru" => "en",
                _ => currentLanguage
            };
            PlayerPrefs.SetString("Language", currentLanguage);
            LoadLocalizationMap(currentLanguage);
        }
    }

    public enum LocalizationGroup
    {
        LevelTask,
        LevelTheory,
        PythonHint,
        TableHint
    }
}