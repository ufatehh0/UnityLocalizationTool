using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class LocalizationManager : MonoBehaviour
{
    // Singleton instance to access from other scripts
    public static LocalizationManager Instance;

    // Dictionary holding all texts for all languages
    private Dictionary<string, Dictionary<string, string>> localizedTexts;
    
    // Currently selected language
    private string currentLanguage = "en";

    private void Awake()
    {
        // Set this instance as the singleton
        Instance = this;

        // Load previously saved language or default to English
        currentLanguage = PlayerPrefs.GetString("language", "en");

        // Load the language data from JSON
        LoadLanguage(currentLanguage);
    }

    // Load language by language code (e.g., "en", "tr", "es")
    public void LoadLanguage(string langCode)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "localization.json");

        if (File.Exists(filePath))
        {
            // Read JSON file
            string json = File.ReadAllText(filePath);

            // Deserialize into dictionary
            localizedTexts = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

            // Set current language and save to PlayerPrefs
            currentLanguage = langCode;
            PlayerPrefs.SetString("language", currentLanguage);
            PlayerPrefs.Save();

            // Notify all subscribers that the language changed
            OnLanguageChanged?.Invoke();
        }
        else
        {
            Debug.LogError("Localization file not found: " + filePath);
        }
    }

    // Get localized text by key
    public string GetText(string key)
    {
        if (localizedTexts != null && localizedTexts.ContainsKey(key) && localizedTexts[key].ContainsKey(currentLanguage))
        {
            return localizedTexts[key][currentLanguage];
        }
        return key; // Fallback: return the key itself if not found
    }

    // Event triggered when language changes
    public delegate void LanguageChanged();
    public event LanguageChanged OnLanguageChanged;
}
