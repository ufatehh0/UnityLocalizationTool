using TMPro;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LanguageOption
{
    public string code;      // e.g., "en", "tr", "es"
    public string displayName; // e.g., "English", "Türkçe", "Español"
}

public class LanguageDropdown : MonoBehaviour
{
    [Tooltip("Dropdown reference")]
    public TMP_Dropdown dropdown;

    [Tooltip("Languages with code and display name")]
    public List<LanguageOption> languages = new List<LanguageOption>();

    private void Start()
    {
        // Clear previous options
        dropdown.ClearOptions();

        // Add dropdown options using displayName
        List<string> options = new List<string>();
        foreach (var lang in languages)
        {
            options.Add(lang.displayName);
        }
        dropdown.AddOptions(options);

        // Select previously saved language
        string savedLang = PlayerPrefs.GetString("language", "en");
        int index = languages.FindIndex(l => l.code == savedLang);
        if (index >= 0) dropdown.value = index;
        dropdown.RefreshShownValue();

        // Subscribe to dropdown value change
        dropdown.onValueChanged.AddListener(OnLanguageSelected);
    }

    // Called when dropdown selection changes
    private void OnLanguageSelected(int index)
    {
        if (index >= 0 && index < languages.Count)
        {
            LocalizationManager.Instance.LoadLanguage(languages[index].code);
        }
    }
}
