using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    public GameObject languageSelectionPanel; // Панель выбора языка
    public TMP_Text[] textElements; // Массив текстовых элементов TMP_Text
    public string[] russianTexts; // Массив текстов на русском
    public string[] englishTexts; // Массив текстов на английском
    
    private const string LanguageKey = "SelectedLanguage";

    private void Start()
    {
        // Проверяем, был ли уже выбран язык
        if (PlayerPrefs.HasKey(LanguageKey))
        {
            // Если язык уже выбран, применяем его
            string selectedLanguage = PlayerPrefs.GetString(LanguageKey);
            ApplyLanguage(selectedLanguage == "Russian" ? russianTexts : englishTexts);
            languageSelectionPanel.SetActive(false); // Скрываем панель выбора языка
        }
        else
        {
            // Если язык не выбран, показываем панель выбора
            languageSelectionPanel.SetActive(true);
        }
    }

    // Метод для выбора русского языка
    public void SelectRussian()
    {
        SaveLanguageChoice("Russian");
        ApplyLanguage(russianTexts);
        languageSelectionPanel.SetActive(false);
    }

    // Метод для выбора английского языка
    public void SelectEnglish()
    {
        SaveLanguageChoice("English");
        ApplyLanguage(englishTexts);
        languageSelectionPanel.SetActive(false);
    }
    
    private void SaveLanguageChoice(string language)
    {
        PlayerPrefs.SetString(LanguageKey, language);
        PlayerPrefs.Save(); // Сохраняем изменения
    }


    // Применяем выбранный язык
    private void ApplyLanguage(string[] texts)
    {
        if (textElements.Length != texts.Length)
        {
            Debug.LogError("Количество текстовых элементов и переводов не совпадает!");
            return;
        }

        for (int i = 0; i < textElements.Length; i++)
        {
            textElements[i].text = texts[i];
        }
    }
}