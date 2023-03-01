using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Localization
{
    public static class Localization
    {
        private static Dictionary<string, Dictionary<string, string>> _localizationMap =
            new Dictionary<string, Dictionary<string, string>>();
        private static string _currentLanguage;

        public static Action LoadLocalized;

        static Localization()
        {
            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetString("Language", "ru");
            }
            _currentLanguage = PlayerPrefs.GetString("Language");
            LoadLocalizationMap(_currentLanguage);
        }

        private static void LoadLocalizationMap(string lang)
        {
            var jsonString = Resources.Load<TextAsset>(lang).ToString();
            _localizationMap =
                JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
            LoadLocalized?.Invoke();
        }

        public static string GetValueByKey(LocalizationGroup group, string key)
        {
            return TextTagAggregation.SetTagValue(_localizationMap[group.ToString()][key]);
        }

        public static void SwitchLocalization()
        {
            _currentLanguage = _currentLanguage switch
            {
                "en" => "ru",
                "ru" => "en",
                _ => _currentLanguage
            };
            PlayerPrefs.SetString("Language", _currentLanguage);
            LoadLocalizationMap(_currentLanguage);
        }
    }

    public enum LocalizationGroup
    {
        LevelTask,
        LevelTheory,
        PythonHint,
        TableHint,
        UI,
        ArenaTask,
        LevelCompleted,
    }
}