using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Localization : MonoBehaviour
{
    private static Dictionary<string, Dictionary<string, string>> _dics;
    public static string _currentLanguage = "_pt-br";

    private static bool _hasLoaded;
    public static Action updateL;

    private static void Load()
    {
        TextAsset _asset = Resources.Load<TextAsset>("Localization");
        string csv = _asset.text;

        _dics = new Dictionary<string, Dictionary<string, string>>();
        string[] lines = csv.Split('\n');
        string[] header = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(',');
            string key = cells[0].Trim();
            for (int j = 1; j < cells.Length; j++)
            {
                string language = header[j].Trim();
                if (!_dics.ContainsKey(language))
                {
                    _dics.Add(language, new Dictionary<string, string>());
                }
                string value = cells[j].Trim();
                _dics[language].Add(key, value);
            }
        }
        _hasLoaded = true;
    }

    public void ChangeLanguage(string language)
    {
        _currentLanguage = language;
        UpdateLanguage();
    }

    public static string Localize(string key)
    {
        if (!_hasLoaded)
        {
            Load();
        }
        if (!_dics[_currentLanguage].ContainsKey(key))
        {
            Debug.LogWarning(string.Format("Localization.Localize There's no value for key: {0}", key));
            return string.Format("*_{0}_*", key);
        }
        return _dics[_currentLanguage][key];
    }

    public static void UpdateLanguage()
    {
        updateL?.Invoke();
    }
}
