# Unity Localization Helper

A simple, modular, and easy-to-use localization system for Unity projects.  
Designed to work with **TextMeshPro** and JSON-based localization files, allowing you to easily manage multiple languages in your game or application.  

This package is ideal for small to medium projects and can be added as a module to any Unity project.

---

## Features

- **LocalizationManager**: Central manager for handling all localized texts.
- **LocalizedText**: Attach to TextMeshPro UI elements to automatically display localized text.
- **LanguageDropdown**: Dropdown component for switching languages at runtime.
- **Inspector-friendly**: Add or modify languages directly in the Unity Inspector, no code changes required.
- **JSON-based**: Store all texts in a readable JSON file.
- **Multi-scene support**: Works across scenes when using `DontDestroyOnLoad`.

---

## Installation

1. **Copy files**  
   - Copy the `Localization` folder into your project `Assets/`.
   - Copy the `localization.json` file into `Assets/StreamingAssets/`.

2. **Setup LocalizationManager**  
   - Add an empty GameObject in your scene and attach `LocalizationManager.cs`.
   - Optionally, you can make this a prefab for easy reuse across multiple scenes.

3. **Setup LocalizedText components**  
   - Attach `LocalizedText.cs` to any TextMeshPro UI element you want to localize.
   - Set the `key` field to match the key in your JSON file.

4. **Setup LanguageDropdown (optional)**  
   - Attach `LanguageDropdown.cs` to a TMP_Dropdown object.
   - In the Inspector, add your languages:
     - `code`: JSON language code (`en`, `tr`, `es`, etc.)
     - `displayName`: The name shown in the dropdown (`English`, `Türkçe`, `Español`, etc.)

---

## Usage

### 1. Adding a new language

1. Open `localization.json` in `Assets/StreamingAssets/`.
2. Add a new language code to each key. Example JSON:

```json
{
  "play_button": {
    "en": "Play",
    "tr": "Oyna",
    "es": "Jugar",
    "fr": "Jouer"
  },
  "settings_button": {
    "en": "Settings",
    "tr": "Ayarlar",
    "es": "Configuración",
    "fr": "Paramètres"
  },
  "exit_button": {
    "en": "Exit",
    "tr": "Çıkış",
    "es": "Salir",
    "fr": "Quitter"
  }
}
```

    Each key corresponds to a UI element.

    Add new languages by adding new language codes under each key.

    The LocalizationManager automatically reads this file and updates all LocalizedText components.

2. Getting localized text in scripts

string playText = LocalizationManager.Instance.GetText("play_button")
// Returns the text for the currently selected language.
// Returns the key itself if the text is missing.

3. Switching language at runtime

LocalizationManager.Instance.LoadLanguage("tr") // Switch to Turkish
// All UI elements with LocalizedText will automatically update.

4. Example

ExampleScene/DemoScene.unity demonstrates:

    A sample menu with Play, Settings, and Exit buttons.

    LanguageDropdown with en, tr, es.

    Adding new languages without modifying code.

Notes

    Singleton: Ensure only one LocalizationManager exists per scene.

    Multi-scene support: Use DontDestroyOnLoad if you want the manager to persist across scenes.

    JSON structure: All keys should have entries for each language. Missing entries fallback to the key itself.

    Inspector-driven: Add new languages and dropdown display names directly from the Unity Inspector without code changes.

Recommended Folder Structure
```
Assets/
├─ Localization/
│  ├─ LocalizationManager.cs
│  ├─ LocalizedText.cs
│  ├─ LanguageDropdown.cs
├─ StreamingAssets/
│  └─ localization.json
├─ ExampleScene/
│  └─ DemoScene.unity
```
