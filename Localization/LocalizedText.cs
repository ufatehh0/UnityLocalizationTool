using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LocalizedText : MonoBehaviour
{
    // Key to look up in the JSON file
    public string key;

    private TMP_Text textComponent;

    void Awake()
    {
        // Get TextMeshPro component
        textComponent = GetComponent<TMP_Text>();

        // Subscribe to language change event if manager exists
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged += UpdateText;

            // Update text initially
            UpdateText();
        }
        else
        {
            Debug.LogWarning("LocalizationManager not found in the scene.");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
    }

    // Update the text component with localized string
    private void UpdateText()
    {
        if (LocalizationManager.Instance != null)
            textComponent.text = LocalizationManager.Instance.GetText(key);
        else
            textComponent.text = key; // Fallback to key
    }
}
